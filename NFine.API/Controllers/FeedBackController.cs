using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NFine.Application.FeedBackManage;
using NFine.API.Models;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Code;

namespace NFine.API.Controllers
{
    public class FeedBackController : ApiController
    {
        /// <summary>
        /// 提交反馈
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public ApiResult<dynamic> PostFeedBackData()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string userId = Common.GetString("userId");
                string title = Common.GetString("title");
                string content = Common.GetString("content");
                FeedBackEntity feedBackEntity = new FeedBackEntity();
                feedBackEntity.F_UserId = userId;
                feedBackEntity.F_Content = content;
                feedBackEntity.F_Title = title;
                FeedBackApp feedBackApp = new FeedBackApp();
                feedBackApp.SubmitForm(feedBackEntity, string.Empty);
                api.Message = "提交成功";
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