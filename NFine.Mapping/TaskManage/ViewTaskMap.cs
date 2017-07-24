using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Mapping.TaskManage
{
    public class ViewTaskMap : EntityTypeConfiguration<ViewTaskEntity>
    {
        public ViewTaskMap()
        {
            this.ToTable("View_Task");
            this.HasKey(t => t.F_Id);
        }
    }
}
