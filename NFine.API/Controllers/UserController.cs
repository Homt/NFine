using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using NFine.Application;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;
using NFine.API.Attribute;
using NFine.API.Models;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Entity.SystemSecurity;

namespace NFine.API.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码</param>
        /// <returns></returns>
        public ApiResult<dynamic> Login()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string userName = Common.GetString("userName");
                string userPwd = Common.GetString("userPwd");
                userPwd = Md5.md5(userPwd, 32).ToLower();
                UserEntity userEntity = new UserApp().CheckLogin(userName, userPwd);
                if (userEntity != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.F_Id;
                    operatorModel.UserCode = userEntity.F_Account;
                    operatorModel.UserName = userEntity.F_RealName;
                    operatorModel.CompanyId = userEntity.F_OrganizeId;
                    operatorModel.DepartmentId = userEntity.F_DepartmentId;
                    operatorModel.RoleId = userEntity.F_RoleId;
                    operatorModel.LoginIPAddress = Net.Ip;
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    if (userEntity.F_Account == "admin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                    api.Message = "登录成功";
                    api.Status = true;
                    api.Result = operatorModel;
                    return api;
                }
                else
                {
                    api.Message = "用户名或密码有误";
                    return api;
                }
            }
            catch (Exception e)
            {
                api.Message = e.Message;
                return api;
            }
        }
       /// <summary>
       /// 修改密码
       /// </summary>
       /// <param name="userId">用户id</param>
       /// <param name="password">原密码</param>
       /// <param name="newpassword">新密码</param>
       /// <param name="repassword">确认新密码</param>
       /// <returns></returns>
        public ApiResult<dynamic> RevisePassword()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string userid =  Common.GetString("userId");
                string pwd = Common.GetString("password");
                string newpwd = Common.GetString("newpassword");
                string repwd =  Common.GetString("repassword");
                UserLogOnApp userLogOnApp = new UserLogOnApp();
                var userLogOn = userLogOnApp.GetForm(userid);
                var oldpswd = Md5.md5(DESEncrypt.Encrypt(Md5.md5(pwd, 32).ToLower(), userLogOn.F_UserSecretkey).ToLower(), 32).ToLower();
                if (userLogOn.F_UserPassword != oldpswd)
                {
                    api.Message = "原密码有误";
                    return api;
                }
                if (newpwd != repwd)
                {
                    api.Message = "确认密码和新密码不一致";
                    return api;
                }
                userLogOnApp.ModifyPassword(newpwd, userLogOn.F_Id);
                api.Message = "修改成功";
                api.Status = true;
                return api;
            }
            catch (Exception e)
            {
                api.Message = e.Message;
                return api;
            }
        }

    }
}
