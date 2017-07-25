using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT.Common;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.TaskManage;
using NFine.Domain._04_IRepository.CustomerManage;
using NFine.Domain._04_IRepository.TaskManage;
using NFine.Repository.SystemManage;
using NFine.Repository.TaskManage;

namespace NFine.Application.TaskManage
{
    public class TaskApp
    {
        private ITaskRepository service = new TaskRepository();
        //private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<TaskEntity> GetList(Pagination pagination, string keyword, string schedule = "", string start = "", string end = "", string user = "", string bank = "")
        {
            var expression = ExtLinq.True<TaskEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_UserId.Contains(keyword));
                //expression = expression.Or(t => t..Contains(keyword));
                //expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(schedule))
            {
                expression = expression.And(d => d.F_Status == schedule);
            }
            if (!string.IsNullOrEmpty(start))
            {
                var startdt = Utils.ObjectToDateTime(start);
                expression = expression.And(d => d.F_CreatorTime > startdt);
            }
            if (!string.IsNullOrEmpty(end))
            {
                var enddt = Utils.ObjectToDateTime(end);
                expression = expression.And(d => d.F_CreatorTime <= enddt);
            }
            if (!string.IsNullOrEmpty(user))
            {
                expression = expression.And(d => d.F_UserId == user);
            }
            else
            {
                var userId = uni.GetMyUser();
                expression = expression.And(d => userId.Contains(d.F_UserId));
            }
            if (!string.IsNullOrEmpty(bank))
            {
                CustomerApp customer = new CustomerApp();
                var usersid = customer.GetList(string.Empty, bank).Select(x => x.F_Id);
                expression = expression.And(d => usersid.Contains(d.F_CustomerId));
            }
            //expression = expression.And(t => t.F_Account != "admin");

            return service.FindList(expression, pagination);
        }
        /// <summary>
        /// 根据id获取任务集合
        /// </summary>
        /// <param name="UsersId">员工id集合</param>
        /// <returns></returns>
        public List<TaskEntity> GetList(List<string> UsersId)
        {
            var expression = ExtLinq.True<TaskEntity>();
            expression = expression.And(d => UsersId.Contains(d.F_UserId));
            var startMonth = DateTime.Now.AddMonths(-6);
            expression =
                expression.And(d => d.F_CreatorTime > startMonth && d.F_CreatorTime <= DateTime.Now);//做数据限制 防止查询量过大
            return service.FindList(expression);
        }

        public TaskEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(TaskEntity userEntity, string keyValue)
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
        public void SubmitFormAPI(TaskEntity userEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.ModifySimple(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, keyValue);
        }
        public void SubmitForm(TaskEntity userEntity)
        {
            if (!string.IsNullOrEmpty(userEntity.F_Id))
            {
                userEntity.Modify(userEntity.F_Id);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity);
        }
        public void UpdateForm(TaskEntity userEntity)
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
