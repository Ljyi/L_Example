using System;
using ERP.Model;
using ERP.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ERP.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SysMenuService sysMenuService = new SysMenuService();
            UserService userService = new UserService();
            userService.isSave = false;
            User user = userService.GetUser();
            User user1=  new User()
            {
                Email = "979671716@qq.com",
                LogingName = "LJY",
                Password = "123",
                UserName = "李军毅",
                CreateUser = "李军毅",
                CredateTime = DateTime.Now
            };
            userService.Add(user1);
            userService.Update(user);
            SysMenu sysMenu = new SysMenu()
            {
                CreateUser = "ljy12",
                CredateTime = DateTime.Now,
                Icon = "123",
                LinkUrl = "123",
                MenuCode = "T1",
                MenuLevel = 1,
                MenuName = "Test1",
                ParentID = 0,
                SortNumber = 1
            };
            sysMenuService.AddSysMenu(sysMenu);
            sysMenuService.GeSysMenu();
        }
    }

    public class Test
    {
        static string s = null;
        public static string Get()
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "123";
            }
            return s;
        }
    }
}
