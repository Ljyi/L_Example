using ERP.DAL;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Service
{
    public class SysMenuService
    {
        private IRepository<SysMenu> sysMenuRepository = null;
        public SysMenuService()
        {
            sysMenuRepository = new EFRepositoryBase<SysMenu>();
        }
        public List<SysMenu> GeSysMenu()
        {
            return sysMenuRepository.Entities.ToList();
        }
        public int AddSysMenu(SysMenu sysMenu)
        {
            return sysMenuRepository.Insert(sysMenu);
        }
        public int UpdateSysMenu(SysMenu sysMenu)
        {
            return sysMenuRepository.Update(sysMenu);
        }
    }
}
