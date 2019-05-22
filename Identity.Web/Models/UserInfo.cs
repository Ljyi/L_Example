using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identity.Web.Models
{
    public class UserInfo : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime ? UpdateTime { get; set; }
    }
}