using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.Domain._04_IRepository.MessageManage
{
    public interface IMessageRepository : IRepositoryBase<MessageEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(MessageEntity userEntity, string keyValue);
    }
}
