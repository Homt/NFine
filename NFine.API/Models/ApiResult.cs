using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NFine.API.Models
{
    /// <summary>
    /// APP接口统一标准模型
    /// </summary>
    [DataContract(Name = "result")]
    public class ApiResult<T> where T : class, new()
    {
        /// <summary>
        /// 接口状态: 1.成功 0.失败
        /// </summary>
        [DataMember(Name = "isSuccess")]
        public bool Status { get; set; }

        /// <summary>
        /// 错误提示消息
        /// </summary>
        [DataMember(Name = "msg")]
        public string Message { get; set; }

        /// <summary>
        /// 实体模型
        /// </summary>
        [DataMember(Name = "list")]
        public T Result { get; set; }

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public ApiResult()
        {
            Status = false;
            Message = "";
            Result = new T();
        }
    }
}