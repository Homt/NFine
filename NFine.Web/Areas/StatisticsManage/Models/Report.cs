using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace NFine.Web.Areas.StatisticsManage.Models
{
    public class Series
    {
        public string id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public int count { get; set; }
        public string schedule { get; set; }
    }

    public class Report
    {
        public string name { get; set; }
        public List<int> data { get; set; }
    }
}