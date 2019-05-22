using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderPreviewResponse : BaseResponse
    {
        /// <summary>
        /// 订单预览结果，过自动拆单会返回多个记录
        /// </summary>
        public List<OrderPreviewResuslt> orderPreviewResuslt { get; set; }
        /// <summary>
        /// 运费说明的商品列表
        /// </summary>
        public long[] postFeeByDescOfferList { get; set; }
        /// <summary>
        /// 代销商品列表
        /// </summary>
        public long[] consignOfferList { get; set; }
    }

    public class OrderPreviewResuslt
    {
        public List<string> tradeModeNameList { get; set; }
        public long discountFee { get; set; }
        public bool status { get; set; }
        public bool taoSampleSinglePromotion { get; set; }
        public long sumPayment { get; set; }
        public string message { get; set; }
        public string resultCode { get; set; }
        public long sumPaymentNoCarriage { get; set; }
        public long additionalFee { get; set; }
        public string flowFlag { get; set; }
        public long sumCarriage { get; set; }
        public List<CargoList> cargoList { get; set; }
        public List<ShopPromotionList> shopPromotionList { get; set; }

    }
    public class CargoList
    {
        public double amount { get; set; }
        public string message { get; set; }
        public double finalUnitPrice { get; set; }
        public string specId { get; set; }
        public double skuId { get; set; }
        public string resultCode { get; set; }
        public long offerId { get; set; }

        public List<CargoPromotionList> cargoPromotionList { get; set; }
    }
    public class CargoPromotionList
    {
        public string promotionId { get; set; }
        public bool selected { get; set; }
        public string text { get; set; }
        public string desc { get; set; }
        public bool freePostage { get; set; }
        public long discountFee { get; set; }
    }
    public class ShopPromotionList
    {
        public string promotionId { get; set; }
        public bool selected { get; set; }
        public string text { get; set; }
        public string desc { get; set; }
        public bool freePostage { get; set; }
        public long discountFee { get; set; }
    }
}
