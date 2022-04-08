namespace AutofacApplication.Service
{
    public class UserService : IUserService
    {
        public string GetUserName()
        {
            return "张三";
        }
    }
    public class User2Service : IUserService
    {
        public string GetUserName()
        {
            return "张三2号";
        }
    }
}
