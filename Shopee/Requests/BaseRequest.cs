using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopee.Requests
{
    public class BaseRequest
    {
        public int partner_id { get; set; }
        public long shopid { get; set; }
        public long timestamp { get; set; }

    }
}
