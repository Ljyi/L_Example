using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    /// <summary>
    /// 订单创建只允许购买同一个供应商的商品。本接口返回创建订单相关的优惠等信息。 
    /// 1、校验商品数据是否允许订购。 2、校验代销关系 3、校验库存、起批量、是否满足混批条件
    /// </summary>
    public class CreateOrderPreviewParam : BaseRequest
    {
        public CreateOrderPreviewParam()
        {
            NamespaceValue = "com.alibaba.trade";
            Name = "alibaba.createOrder.preview";
            RequestType = "CreateOrderPreview";
        }
        public string invoiceParam;
        public string addressParam;
        public string cargoParamList;
        public string flow= "general";
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            dictParameters.Add("invoiceParam", invoiceParam);
            dictParameters.Add("addressParam", addressParam);
            dictParameters.Add("cargoParamList", cargoParamList);
            dictParameters.Add("flow", flow);
        }
    }
    /// <summary>
    /// 收货地址信息
    /// </summary>
    public class AddressParam
    {
        /// <summary>
        /// 收货地址id
        /// </summary>
        public long addressId { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string fullName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postCode { get; set; }
        /// <summary>
        /// 市文本
        /// </summary>
        public string cityText { get; set; }
        /// <summary>
        /// 省份文本
        /// </summary>
        public string provinceText { get; set; }
        /// <summary>
        /// 区文本
        /// </summary>
        public string areaText { get; set; }
        /// <summary>
        /// 镇文本
        /// </summary>
        public string townText { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 地址编码
        /// </summary>
        public string districtCode { get; set; }
    }

    public class CargoParamList
    {
        /// <summary>
        /// 商品对应的offer id
        /// </summary>
        public long offerId { get; set; }
        /// <summary>
        /// 商品规格id
        /// </summary>
        public string specId { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public double quantity { get; set; }

    }

    public class InvoiceParam
    {
        public int invoiceType { get; set; }
        public string provinceText { get; set; }
        public string cityText { get; set; }
        public string areaText { get; set; }
        public string townText { get; set; }
        public string postCode { get; set; }
        public string address { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string companyName { get; set; }
        public string taxpayerIdentifier { get; set; }
        public string bankAndAccount { get; set; }
        public string localInvoiceId { get; set; }
    }
}
