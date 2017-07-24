using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.TaskManage;
using NFine.Domain._04_IRepository.CustomerManage;
using NFine.Domain._04_IRepository.TaskManage;

namespace NFine.Repository.TaskManage
{
    public class TaskRepository : RepositoryBase<TaskEntity>, ITaskRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<TaskEntity>(t => t.F_Id == keyValue);
                //db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }

        public void SubmitForm(TaskEntity userEntity, string keyValue)
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
        public void SubmitForm(TaskEntity userEntity)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(userEntity.F_Id))
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
