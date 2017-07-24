/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 锡润管理平台
 * Website：http://www.softdog.cc
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class UserController : ControllerBase
    {
        private UserApp userApp = new UserApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取我的员工
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetMyGridJson()
        {
            var companyId = NFine.Code.OperatorProvider.Provider.GetCurrent().CompanyId;//获取到当前的组织结构id
            OrganizeApp organizeApp = new OrganizeApp();
            var organizeData = organizeApp.GetList();
            List<string> organizeIds = new List<string>();
            organizeIds = GetChildList(organizeIds, organizeData.Where(d => d.F_Id == companyId).ToList());
            var userData = new UserApp().GetList(organizeIds);
            return Content(userData.ToJson());
        }
        List<string> GetChildList(List<string> organizeIds,
           List<OrganizeEntity> organizeEntities)
        {
            OrganizeApp organizeApp = new OrganizeApp();
            foreach (var item in organizeEntities)
            {
                organizeIds.Add(item.F_Id);
                var tempdata = organizeApp.GetList().Where(d => d.F_ParentId == item.F_Id).ToList();
                GetChildList(organizeIds, tempdata);
            }
            return organizeIds;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            userApp.SubmitForm(userEntity, userLogOnEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            userApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = false;
            userApp.UpdateForm(userEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = true;
            userApp.UpdateForm(userEntity);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyPassword()
        {
            string pswd = Common.GetString("pswd");
            string newpswd = Common.GetString("newpswd");
            string repswd = Common.GetString("repswd");
            var userLogOn = userLogOnApp.GetForm();
            var oldpswd = Md5.md5(DESEncrypt.Encrypt(Md5.md5(pswd, 32).ToLower(), userLogOn.F_UserSecretkey).ToLower(), 32).ToLower();
            if (userLogOn.F_UserPassword != oldpswd)
            {
                return Error("原密码有误!");
            }
            if (newpswd != repswd)
            {
                return Error("确认密码和新密码不一致");
            }
            userLogOnApp.ModifyPassword(newpswd, userLogOn.F_Id);
            return Success("修改成功!");
        }
    }
}
