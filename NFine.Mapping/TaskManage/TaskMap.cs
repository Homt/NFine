using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Mapping.TaskManage
{
    public class TaskMap : EntityTypeConfiguration<TaskEntity>
    {
        public TaskMap()
        {
            this.ToTable("Sys_Task");
            this.HasKey(t => t.F_Id);
        }
    }
}
