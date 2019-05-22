using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopee.Requests
{
    public class GetOrderDetails : BaseRequest
    {
        public List<string> ordersn_list { get; set; }
    }
}
