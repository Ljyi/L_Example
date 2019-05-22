using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identity.Web.Models
{
    public class RoleInfo : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}