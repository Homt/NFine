using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;

namespace NFine.Application
{
    public static class uni
    {
        /// <summary>
        /// 获取我的下级员工id集合
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMyUser()
        {
            //根据权限获取我的下级
            var companyId = NFine.Code.OperatorProvider.Provider.GetCurrent().CompanyId;//获取到当前的组织结构id
            OrganizeApp organizeApp = new OrganizeApp();
            var organizeData = organizeApp.GetList();
            List<string> organizeIds = new List<string>();
            organizeIds = GetChildList(organizeIds, organizeData.Where(d => d.F_Id == companyId).ToList());
            var expression = ExtLinq.True<UserEntity>();
            expression = expression.And(d => organizeIds.Contains(d.F_OrganizeId));
            IUserRepository service = new UserRepository();
            return service.FindList(expression).Select(x => x.F_Id).ToList();
        }
        /// <summary>
        /// 获取我的下级
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        static List<string> GetChildList(List<string> organizeIds,
                List<OrganizeEntity> organizeEntities)
        {
            OrganizeApp organizeApp = new OrganizeApp();
            foreach (var item in organizeEntities)
            {
                organizeIds.Add(item.F_Id);
                var tempdata = organizeApp.GetList().Where(d => d.F_ParentId == item.F_Id).ToList();
                GetChildList(organizeIds, tempdata);
            }
            return organizeIds;
        }
    }
}
