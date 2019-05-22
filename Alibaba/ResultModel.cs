using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba
{
    public class ResultModel<T>
    {
        public ResultModel()
        {
            Success = true;
            Message = "成功";
        }
        public T Result { get; set; }

        public List<T> ResultList { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 异常code
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
