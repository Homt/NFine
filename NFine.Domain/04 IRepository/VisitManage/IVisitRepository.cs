using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.VisitManage;

namespace NFine.Domain._04_IRepository.VisitManage
{
    public interface IVisitRepository : IRepositoryBase<VisitEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(VisitEntity userEntity, string keyValue);
    }
}
