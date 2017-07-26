using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFine.Web.Areas.VisitManage.Models
{
    public class uni
    {
        public static string ConvertResult(bool? result)
        {
            return Convert.ToBoolean(result) ? "是" : "否";
        }

        public static string ConvertIcon(bool? result)
        {
            return Convert.ToBoolean(result) ? "是☑  否□" : "是□  否☑";
        }
        public static string ConverType(string type)
        {
            switch (type)
            {
                case "新商户":
                    return "新商户☑  重点商户□  高风险商户□  其他商户□";
                    break;
                case "重点商户":
                    return "新商户□  重点商户☑  高风险商户□  其他商户□";
                    break;
                case "高风险商户":
                    return "新商户□  重点商户□  高风险商户☑  其他商户□";
                    break;
                case "其他商户":
                    return "新商户□  重点商户□  高风险商户□  其他商户☑";
                    break;
            }
            return string.Empty;
        }
    }
}