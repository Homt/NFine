using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.Mapping.FeedBackManage
{
    public class FeedBackMap : EntityTypeConfiguration<FeedBackEntity>
    {
        public FeedBackMap()
        {
            this.ToTable("Sys_FeedBack");
            this.HasKey(t => t.F_Id);
        }
    }
}
