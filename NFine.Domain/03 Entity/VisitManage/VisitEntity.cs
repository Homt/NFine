using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.VisitManage
{
    public class VisitEntity : IEntity<VisitEntity>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string F_Id { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public bool? F_DeleteMark { get; set; }
        public string F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_Code { get; set; }

        public string F_Name { get; set; }
        public string F_Address { get; set; }
        public string F_Tel { get; set; }
        public string F_Mobile { get; set; }
        public string F_Branch { get; set; }
        public string F_Manager_Mobile { get; set; }
        public string F_Manager_Name { get; set; }

        public string F_Number { get; set; }
        public int? F_Count { get; set; }
        public string F_Type { get; set; }
        public string F_Cashier_Name { get; set; }
        public string F_Cashier_Code { get; set; }
        public string F_Contacts { get; set; }
        public string F_Machine_Type { get; set; }
        public string F_Machine_Code { get; set; }
        public string F_Machine_Count { get; set; }
        public string F_Category { get; set; }

        public string F_PSAM { get; set; }
        public string F_Temp1 { get; set; }
        public string F_Temp2 { get; set; }

        public string F_Temp3 { get; set; }

        public string F_Temp4 { get; set; }

        public string F_Temp5 { get; set; }

        public string F_TaskId { get; set; }
        public bool? F_Qu1 { get; set; }
        public bool? F_Qu2 { get; set; }
        public bool? F_Qu3 { get; set; }
        public bool? F_Qu4 { get; set; }
        public bool? F_Qu5 { get; set; }
        public bool? F_Qu6 { get; set; }
        public bool? F_Qu7 { get; set; }
        public bool? F_Qu8 { get; set; }
        public bool? F_Qu9 { get; set; }
        public bool? F_Qu10 { get; set; }
        public bool? F_Qu11 { get; set; }
        public bool? F_Qu12 { get; set; }
        public bool? F_Qu13 { get; set; }
        public bool? F_Qu14 { get; set; }
        public bool? F_Qu15 { get; set; }

    }
}
