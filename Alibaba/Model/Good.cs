using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Model
{
    #region
    /// <summary>
    /// 产品信息
    /// </summary>
    public class Good
    {
        /// <summary>
        /// 淘宝cartId(必填)
        /// </summary>
        public int cartId { get; set; }

        /// <summary>
        /// ID（必填）
        /// </summary>
        public string ext { get; set; }
        /// <summary>
        /// 所属流程(必填)
        /// </summary>
        public string flow { get; set; }
        /// <summary>
        /// id,Cargo 标识(必填)
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 商品ID，offerId(必填)
        /// </summary>
        public int offerId { get; set; }
        /// <summary>
        /// 数量(必填)
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 商品的规格特征值(必填)
        /// </summary>
        public string specId { get; set; }
        /// <summary>
        /// (必填)
        /// </summary>
        public string tradeMode { get; set; }
        /// <summary>
        /// (必填)
        /// </summary>
        public string tradeWay { get; set; }
    }
    /// <summary>
    /// 收货地址
    /// </summary>
    public class ReceiveAddress
    {
        /// <summary>
        /// 街道地址，不包括省市编码(必填)
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 地址区域编码(必填)
        /// </summary>
        public string addressCode { get; set; }
        /// <summary>
        /// * 地址区域编码对应的文本（包括国家，省，城市）(必填)
        /// </summary>
        public string addressCodeText { get; set; }
        /// <summary>
        /// 地址ID(必填)
        /// </summary>
        public int addressId { get; set; }
        /// <summary>
        /// 业务类型(必填)
        /// </summary>
        public string bizType { get; set; }
        /// <summary>
        /// 是否为默认地址(必填)
        /// </summary>
        public string isDefault { get; set; }
        /// <summary>
        /// 收货人姓名(必填)
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// 是否最近使用的地址(必填)
        /// </summary>
        public string latest { get; set; }
        /// <summary>
        /// 手机(必填)
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 电话(必填)
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 邮编(必填)
        /// </summary>
        public string postCode { get; set; }
    }

    public class extension
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    #endregion
}
