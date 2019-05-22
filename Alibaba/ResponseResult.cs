using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba
{
    public class ResponseResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回结果(Josn)
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
