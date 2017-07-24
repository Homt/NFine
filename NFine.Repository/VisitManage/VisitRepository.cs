using System;
using System.Collections.Generic;
using System.Linq;
using NFine.Data;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.VisitManage;
using NFine.Domain._04_IRepository.VisitManage;


namespace NFine.Repository.VisitManage
{
    public class VisitRepository : RepositoryBase<VisitEntity>, IVisitRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<CustomerEntity>(t => t.F_Id == keyValue);
                //db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }


        public void SubmitForm(VisitEntity userEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    db.Insert(userEntity);
                }
                db.Commit();
            }
        }
    }
}
