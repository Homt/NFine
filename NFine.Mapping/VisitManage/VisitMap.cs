using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain._03_Entity.VisitManage;

namespace NFine.Mapping.VisitManage
{
    public class VisitMap : EntityTypeConfiguration<VisitEntity>
    {
        public VisitMap()
        {
            this.ToTable("Sys_Visit");
            this.HasKey(t => t.F_Id);
        }
    }
}
