using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class SyncProductPushedResponse
    { 
      public SyncProductPushedResult result { get; set; }
    }
    public class SyncProductPushedResult
    { 
        /// <summary>
       /// 是否成功
       /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string errorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorMsg { get; set; }
    }
}
