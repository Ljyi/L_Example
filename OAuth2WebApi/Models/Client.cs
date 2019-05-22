namespace OAuth2WebApi.Models
{
    public class Client
    {
        public string Id { get; set; }

        public string Secret { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RedirectUrl { get; set; }
    }
}