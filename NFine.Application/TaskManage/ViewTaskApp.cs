using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Domain._03_Entity.TaskManage;
using NFine.Domain._04_IRepository.TaskManage;
using NFine.Repository.TaskManage;

namespace NFine.Application.TaskManage
{
    public class ViewTaskApp
    {
        private IViewTaskRepository service = new ViewTaskRepository();
        //private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<ViewTaskEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ViewTaskEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_UserId.Contains(keyword));
                //expression = expression.Or(t => t..Contains(keyword));
                //expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Account != "admin");

            return service.FindList(expression, pagination);
        }
        public ViewTaskEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(ViewTaskEntity userEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, keyValue);
        }
        public void UpdateForm(ViewTaskEntity userEntity)
        {
            service.Update(userEntity);
        }
        //public UserEntity CheckLogin(string username, string password)
        //{
        //    UserEntity userEntity = service.FindEntity(t => t.F_Account == username);
        //    if (userEntity != null)
        //    {
        //        if (userEntity.F_EnabledMark == true)
        //        {
        //            UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
        //            string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
        //            if (dbPassword == userLogOnEntity.F_UserPassword)
        //            {
        //                DateTime lastVisitTime = DateTime.Now;
        //                int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
        //                if (userLogOnEntity.F_LastVisitTime != null)
        //                {
        //                    userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
        //                }
        //                userLogOnEntity.F_LastVisitTime = lastVisitTime;
        //                userLogOnEntity.F_LogOnCount = LogOnCount;
        //                userLogOnApp.UpdateForm(userLogOnEntity);
        //                return userEntity;
        //            }
        //            else
        //            {
        //                throw new Exception("密码不正确，请重新输入");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("账户被系统锁定,请联系管理员");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("账户不存在，请重新输入");
        //    }
        //}
    }
}
