﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._04_IRepository.CustomerManage;
using NFine.Domain._04_IRepository.FeedBackManage;

namespace NFine.Repository.SystemManage
{
    public class FeedBackRepository : RepositoryBase<FeedBackEntity>, IFeedBackRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<FeedBackEntity>(t => t.F_Id == keyValue);
                //db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }


        public void SubmitForm(FeedBackEntity userEntity, string keyValue)
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
                //else
                //{
                //    userLogOnEntity.F_Id = userEntity.F_Id;
                //    userLogOnEntity.F_UserId = userEntity.F_Id;
                //    userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                //    userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                   
                //    db.Insert(userLogOnEntity);
                //}
                db.Commit();
            }
        }
    }
}
