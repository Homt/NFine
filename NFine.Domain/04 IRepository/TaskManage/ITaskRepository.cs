using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Domain._04_IRepository.TaskManage
{
    public interface ITaskRepository : IRepositoryBase<TaskEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(TaskEntity userEntity, string keyValue);
        void SubmitForm(TaskEntity taskEntity);
    }
}
