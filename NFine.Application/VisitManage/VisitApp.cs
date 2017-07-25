using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.VisitManage;
using NFine.Domain._04_IRepository.VisitManage;
using NFine.Repository.VisitManage;

namespace NFine.Application.VisitManage
{
    public class VisitApp
    {
        private IVisitRepository service = new VisitRepository();
        //private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<VisitEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<VisitEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name.Contains(keyword));
                //expression = expression.Or(t => t.F_RealName.Contains(keyword));
                //expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Account != "admin");

            return service.FindList(expression, pagination);
        }

        public List<VisitEntity> GetList(string taskId)
        {
            var expression = ExtLinq.True<VisitEntity>();
            if (!string.IsNullOrEmpty(taskId))
            {
                expression = expression.And(d => d.F_TaskId == taskId);
            }
            return service.FindList(expression);
        }
        public VisitEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(VisitEntity userEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, keyValue);
        }
        public void SubmitFormAPI(VisitEntity userEntity)
        {
            userEntity.CreateSimple();
            service.SubmitForm(userEntity, string.Empty);
        }
        public void UpdateForm(VisitEntity userEntity)
        {
            service.Update(userEntity);
        }

    }
}
