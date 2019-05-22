using ERP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL
{
    /// <summary>
    /// DropCreateDatabaseIfModelChanges
    /// 如果模型更改就重建数据表
    /// CreateDatabaseIfNotExists
    /// 如果不存在就创建数据库
    /// </summary>
    public class ManagerInitializer : DropCreateDatabaseIfModelChanges<BaseDbContext>
    {
        protected override void Seed(BaseDbContext context)
        {
            try
            {
                //初始化用户
                InitializerUser(context);
                //初始化菜单
                InitializerSysMenu(context);
                //初始化角色
                InitializerRole(context);
                //初始化按钮
                InitializerOperationButton(context);
                //初始化用户角色
                InitializerUserRole(context);
                //初始化用户权限
                InitializerUserRights(context);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// 初始用户
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUser(BaseDbContext context)
        {
            List<User> userList = new List<User>() {
                new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "ljy",
                    Password = "123",
                    UserName = "李军毅",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                },
                new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "Admin",
                    Password = "123456",
                    UserName = "管理员",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                },
            };
            userList.ForEach(user =>
            {
                context.User.Add(user);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysMenu(BaseDbContext context)
        {
            List<SysMenu> sysMenuList = new List<SysMenu>() {
                 new SysMenu()
                 {
                    MenuName = "基本设置",
                    MenuCode = "BaseSet",
                    LinkUrl = "Home",
                    ParentID = 0,
                    MenuLevel = 0,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "系统管理",
                    MenuCode = "Home",
                    LinkUrl = "SystemManager",
                    ParentID = 0,
                    MenuLevel = 0,
                    SortNumber = 1,
                    Status = 1,
                    Icon="icon wb-settings",
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "菜单管理",
                    MenuCode = "Menu",
                    LinkUrl = "MenuManager",
                    ParentID = 2,
                    MenuLevel = 1,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "用户管理",
                    MenuCode = "User",
                    LinkUrl = "UserManager",
                    ParentID = 2,
                    MenuLevel = 2,
                    SortNumber = 2,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "日志信息",
                    MenuCode = "Log",
                    LinkUrl = "Log",
                    ParentID = 2,
                    MenuLevel = 2,
                    SortNumber = 3,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "系统设置",
                    MenuCode = "SystemSet",
                    LinkUrl = "SystemSetup",
                    ParentID = 2,
                    MenuLevel = 2,
                    SortNumber = 4,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "显示设置",
                    MenuCode = "DisplaySettings",
                    LinkUrl = "Display",
                    ParentID = 6,
                    MenuLevel = 3,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "日志设置",
                    MenuCode = "LogSettings",
                    LinkUrl = "Log",
                    ParentID = 6,
                    MenuLevel = 3,
                    SortNumber = 2,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
            };
            sysMenuList.ForEach(p =>
            {
                context.SysMenu.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="context"></param>
        public void InitializerRole(BaseDbContext context)
        {
            List<Role> roleList = new List<Role>() {
                 new Role()
                 {
                    RoleName ="员工",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            roleList.ForEach(p =>
            {
                context.Role.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化按钮
        /// </summary>
        /// <param name="context"></param>
        public void InitializerOperationButton(BaseDbContext context)
        {
            List<OperationButton> operationButtonList = new List<OperationButton>() {
                 new OperationButton()
                 {
                    ButtonCode ="Add",
                    ButtonName="新增",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Edit",
                    ButtonName="编辑",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Browse",
                    ButtonName="浏览",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Delete",
                    ButtonName="删除",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Submit ",
                    ButtonName="提交",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Confirm ",
                    ButtonName="确认",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Approval ",
                    ButtonName="审核",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new OperationButton()
                 {
                    ButtonCode ="Reject ",
                    ButtonName="拒绝",
                    InputType="Input",
                    Style="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            operationButtonList.ForEach(p =>
            {
                context.OperationButton.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUserRole(BaseDbContext context)
        {
            List<UserRole> userRoleList = new List<UserRole>() {
                 new UserRole()
                 {
                    RoleId =1,
                    UserId=1,
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            userRoleList.ForEach(p =>
            {
                context.UserRole.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 用户权限
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUserRights(BaseDbContext context)
        {
            List<UserRights> userRightsList = new List<UserRights>() {
                 new UserRights()
                 {
                     SysMenuId =1,
                     OperationButtonId=1,
                     UserRoleId=1,
                     CreateUser="李军毅",
                     CredateTime = DateTime.Now
                }
            };
            userRightsList.ForEach(p =>
            {
                context.UserRights.Add(p);
            });
            context.SaveChanges();
        }
    }
}
