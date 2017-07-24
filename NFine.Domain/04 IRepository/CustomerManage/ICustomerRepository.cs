using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.Domain._04_IRepository.CustomerManage
{
    public interface ICustomerRepository : IRepositoryBase<CustomerEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(CustomerEntity userEntity, string keyValue);
    }
}
