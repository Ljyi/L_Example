using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class GetOrdersResponse : BaseResponse
    {
        public OrdersResult result { get; set; }
    }
    public class OrdersResult
    {
        public OrderBaseInfo baseInfo { get; set; }
        public List<ProductItems> productItems { get; set; }
    }
    public class OrderBaseInfo
    {
        public long id { get; set; }
        public string idOfStr { get; set; }

        public string status { get; set; }
        public string flowTemplateCode { get; set; }

        public string remark { get; set; }
    }
    public class ProductItems
    {
        public string cargoNumber { get; set; }
        public string description { get; set; }
        public decimal itemAmount { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public long productID { get; set; }

        public string[] productImgUrl { get; set; }
        public string productSnapshotUrl { get; set; }
        public decimal quantity { get; set; }
        public decimal refund { get; set; }
        public long skuID { get; set; }
        public int sort { get; set; }
        public string status { get; set; }

        public long subItemID { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public string weight { get; set; }
        public string weightUnit { get; set; }

        public List<GuaranteesTerms> guaranteesTerms { get; set; }
        public string productCargoNumber { get; set; }


    }
    public class GuaranteesTerms
    {
        public string assuranceInfo { get; set; }
        public string assuranceType { get; set; }
        public string qualityAssuranceType { get; set; }

    }

    public class TradeTerms
    {
        public string payStatus { get; set; }
        public DateTime payTime { get; set; }
        public string payWay { get; set; }

        public string phasAmount { get; set; }

        public long phase { get; set; }
        public string phaseCondition { get; set; }
        public string phaseDate { get; set; }

        public bool cardPay { get; set; }
        public bool expressPay { get; set; }
    }
    public class NativeLogistics
    {
        public string address { get; set; }
        public string area { get; set; }
        public string areaCode { get; set; }
        public string city { get; set; }
        public string contactPerson { get; set; }
        public string fax { get; set; }
        public string mobile { get; set; }
        public string province { get; set; }
        public string telephone { get; set; }
        public string zip { get; set; }
        public List<LogisticsItems> logisticsItems { get; set; }
        public string townCode { get; set; }
        public string town { get; set; }
    }
    public class LogisticsItems
    {
        public DateTime deliveredTime { get; set; }
        public string logisticsCode { get; set; }
        public string type { get; set; }
        public long id { get; set; }
        public string status { get; set; }
        public DateTime gmtModified { get; set; }
        public DateTime gmtCreate { get; set; }
        public decimal carriage { get; set; }
        public string fromProvince { get; set; }
        public string fromCity { get; set; }
        public string fromArea { get; set; }
        public string fromAddress { get; set; }
        public string fromPhone { get; set; }
        public string fromMobile { get; set; }
        public string fromPost { get; set; }
        public long logisticsCompanyId { get; set; }
        public string logisticsCompanyNo { get; set; }
        public string logisticsCompanyName { get; set; }
        public string logisticsBillNo { get; set; }
        public string subItemIds { get; set; }
        public string toProvince { get; set; }
        public string toCity { get; set; }
        public string toArea { get; set; }
        public string toAddress { get; set; }
        public string toPhone { get; set; }
        public string toMobile { get; set; }
        public string toPost { get; set; }
    }
}
