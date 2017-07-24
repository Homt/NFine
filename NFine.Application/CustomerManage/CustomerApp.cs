using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._04_IRepository.CustomerManage;
using NFine.Repository.SystemManage;

namespace NFine.Application
{
    public class CustomerApp
    {
        private ICustomerRepository service = new CustomerRepository();
        //private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<CustomerEntity> GetList(Pagination pagination, string keyword,int area)
        {
            var expression = ExtLinq.True<CustomerEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name.Contains(keyword));
                expression = expression.Or(d => d.F_Tel.Contains(keyword));
                //expression = expression.Or(t => t.F_RealName.Contains(keyword));
                //expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            if (area!=0)
            {
                expression = expression.And(d => d.F_Area == area);
            }
            //expression = expression.And(t => t.F_Account != "admin");
            
            return service.FindList(expression, pagination);
        }

        public List<CustomerEntity> GetList(string keyword,string bank)
        {
            var expression = ExtLinq.True<CustomerEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(d => d.F_Name.Contains(keyword));
                expression = expression.Or(d => d.F_Address.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(bank))
            {
                expression = expression.And(d => d.F_Bank == bank);
            }
            return service.FindList(expression);
        }
        public CustomerEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(CustomerEntity userEntity, string keyValue)
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
        public void SubmitFormAPI(CustomerEntity userEntity, string keyValue)
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
        public void UpdateForm(CustomerEntity userEntity)
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
