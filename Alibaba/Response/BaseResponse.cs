using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }

        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
