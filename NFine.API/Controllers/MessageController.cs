using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NFine.Application.MessageManage;
using NFine.API.Models;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.API.Controllers
{
    public class MessageController : ApiController
    {
        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">数量</param>
        /// <returns></returns>
        public ApiResult<dynamic> PostMessageList()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {

                //string userId = jsondata.userId ?? string.Empty;
                int pageindex =  Common.GetInt("pageIndex", 0);// jsondata.pageIndex ?? 0;
                int pagesize = Common.GetInt("pageSize", 0);// jsondata.pageSize ?? 0;

                MessageApp messageApp = new MessageApp();
                Pagination pagination = new Pagination();
                pagination.page = pageindex;
                pagination.rows = pagesize;
                pagination.sidx = "F_CreatorTime";
                pagination.sord = "desc";
                var expression = ExtLinq.True<MessageEntity>();
                api.Result = messageApp.GetList(pagination, expression);
                api.Status = true;
                api.Message = "获取成功";
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