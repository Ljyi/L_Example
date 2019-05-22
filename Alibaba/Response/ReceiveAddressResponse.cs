using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    /// <summary>
    /// 收货地址
    /// </summary>
    public class ReceiveAddressResponse : BaseResponse
    {
        public ReceiveAddressItems result { get; set; }
    }

    public class ReceiveAddressItems
    {
        public List<ReceiveAddress> receiveAddressItems { get; set; }
    }
    /// <summary>
    /// 收货地址
    /// </summary>
    public class ReceiveAddress
    {
        /// <summary>
        /// addressId
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// 街道地址，不包括省市编码
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string post { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilePhone { get; set; }
        /// <summary>
        /// 地址区域编码
        /// </summary>
        public string addressCode { get; set; }
        /// <summary>
        /// 地址区域编码对应的文本（包括国家，省，城市）
        /// </summary>
        public string addressCodeText { get; set; }
        /// <summary>
        /// 是否为默认
        /// </summary>
        public bool isDefault { get; set; }
        /// <summary>
        /// 镇编码
        /// </summary>
        public string townCode { get; set; }
        /// <summary>
        /// 镇地址
        /// </summary>
        public string townName { get; set; }
    }
}
