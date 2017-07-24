using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain._03_Entity.CustomerManage
{
    public class CustomerEntity : IEntity<CustomerEntity>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string F_Id { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public bool? F_DeleteMark { get; set; }
        public string F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_Name { get; set; }
        public string F_Tel { get; set; }
        public string F_Number { get; set; }
        public string F_Gender { get; set; }
      
        public string F_Address { get; set; }
        public string F_Bank { get; set; }
        public int? F_Area { get; set; }
        public string F_Shop_Name { get; set; }
        public string F_Shop_Address { get; set; }
        public string F_Range { get; set; }
        public string F_Machines { get; set; }
        public string F_Machines_Number { get; set; }
        public string F_Cashier_Name { get; set; }
        public string F_Cashier_Number { get; set; }
        public string F_PSAM_Number { get; set; }
        public string F_Manage_Name { get; set; }
        public string F_Manage_Tel { get; set; }
        public double F_Lng { get; set; }
        public double F_Lat { get; set; }
        public string F_Remark { get; set; }
    }
}
