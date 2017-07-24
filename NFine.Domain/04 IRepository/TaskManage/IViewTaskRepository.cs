using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Domain._04_IRepository.TaskManage
{
    public interface IViewTaskRepository : IRepositoryBase<ViewTaskEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(ViewTaskEntity userEntity, string keyValue);
    }
}
