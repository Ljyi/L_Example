using ERP.Model;
using ERP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERP.Consoles
{
    class Program
    {
        static void Main(string[] args)
        {
            SysMenuService sysMenuService = new SysMenuService();
            UserService userService = new UserService();
            userService.isSave = false;
            User user = new User()
            {
                Email = "979671716@qq.com",
                LogingName = "LJY",
                Password = "123",
                UserName = "李军毅",
                CreateUser = "李军毅",
                CredateTime = DateTime.Now
            };
            //for (int i = 0; i < 1000000; i++)
            //{
            //    Thread.Sleep(100);
            //    user.CredateTime = DateTime.Now;
            //    int id = userService.Add(user);
            //    Console.WriteLine("添加成功，不提交");
            //}
            for (int i = 0; i < 10000; i++)
            {
                SysMenu sysMenu = new SysMenu()
                {
                    CreateUser = "ljy",
                    CredateTime = DateTime.Now,
                    Icon = "",
                    LinkUrl = "123",
                    MenuCode = "T",
                    MenuLevel = 1,
                    MenuName = "Test",
                    ParentID = 0,
                    SortNumber = 1
                };
                int id = sysMenuService.AddSysMenu(sysMenu);
                sysMenuService.GeSysMenu();
                if (id > 0)
                {
                    Console.WriteLine("添加成功，提交");
                }
            }
        }
    }
}
