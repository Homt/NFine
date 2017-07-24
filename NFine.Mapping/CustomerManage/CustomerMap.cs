

using System.Data.Entity.ModelConfiguration;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.Mapping.CustomerManage
{

    public class CustomerMap : EntityTypeConfiguration<CustomerEntity>
    {
        public CustomerMap()
        {
            this.ToTable("Sys_Customer");
            this.HasKey(t => t.F_Id);
        }
    }
}
