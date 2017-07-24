/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.softdog.cc
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class OrganizeApp
    {
        private IOrganizeRepository service = new OrganizeRepository();

        public List<OrganizeEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        /// <summary>
        /// 获取下级
        /// </summary>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public List<OrganizeEntity> GetList(string organizeid)
        {
            return service.IQueryable().Where(d => d.F_ParentId == organizeid).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<string> GetAllList(string organizeid)
        {
            var data = service.IQueryable().Where(d => d.F_Id == organizeid).ToList();
            List<string> list = new List<string>();
            return GetNodeOrganize(data, list);
        }
        private List<string> GetNodeOrganize(List<OrganizeEntity> listOrganizeEntities, List<string> listOrganize)
        {
            foreach (var item in listOrganizeEntities)
            {
                listOrganize.Add(item.F_Id);
                var data = service.IQueryable().Where(d => d.F_ParentId == item.F_Id).ToList();
                if (data.Count > 0)
                    GetNodeOrganize(data, listOrganize);
            }
            return listOrganize; ;
        }
        public OrganizeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                service.Update(organizeEntity);
            }
            else
            {
                organizeEntity.Create();
                service.Insert(organizeEntity);
            }
        }
    }
}
