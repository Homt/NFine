using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.Domain._04_IRepository.FeedBackManage
{
    public interface IFeedBackRepository : IRepositoryBase<FeedBackEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(FeedBackEntity userEntity, string keyValue);
    }
}
