using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.Mapping.MessageManage
{
    public class MessageMap : EntityTypeConfiguration<MessageEntity>
    {
        public MessageMap()
        {
            this.ToTable("Sys_Message");
            this.HasKey(t => t.F_Id);
        }
    }
}
