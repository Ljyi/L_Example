﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identity.Web.Models
{
    public class RefreshTokenInfo
    {
        public string Subject { get; set; }
        public string AppClientId { get; set; }
    }
}