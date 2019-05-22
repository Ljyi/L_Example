using OAuth2WebApi.Models;
using System.Collections.Generic;

namespace OAuth2WebApi.OAuth
{
    public class ClientRepository
    {
        public static List<Client> Clients = new List<Client>() {
            new Client{
                 Id = "test1",
                 RedirectUrl = "http://localhost:49279/",
                 UserName="ljy",
                 Password="123",
                 Secret = "123456789"
            }
        };
    }
}