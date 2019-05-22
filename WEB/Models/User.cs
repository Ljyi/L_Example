using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class User
    {
        public string userName { get; set; }
        public string userPassword { get; set; }
    }
    public class Person
    {

        public string Name { get; set; }
        public string Roles { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
    }
}