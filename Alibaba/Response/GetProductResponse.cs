using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class GetProductResponse : BaseResponse
    {
        public ProductInfo productInfo { get; set; }
    }
    public class ProductInfo
    {
        public long productID
        {
            get; set;
        }
        public string productType
        {
            get; set;
        }
        public long categoryID
        {
            get; set;
        }
        public List<ProductAttributes> attributes
        {
            get; set;
        }
        public long[] groupID
        {
            get; set;
        }
        public string status
        {
            get; set;
        }
        public string subject
        {
            get; set;
        }
        public string description
        {
            get; set;
        }
        public string language
        {
            get; set;
        }
        public int periodOfValidity
        {
            get; set;
        }
        public int bizType
        {
            get; set;
        }
        public bool pictureAuth
        {
            get; set;
        }

        public Image image
        {
            get; set;
        }
        public List<SkuInfos> skuInfos
        {
            get; set;
        }
        public SaleInfo saleInfo
        {
            get; set;
        }
        public ShippingInfo shippingInfo
        {
            get; set;
        }
        public List<ExtendInfos> extendInfos
        {
            get; set;
        }
        public string supplierUserId
        {
            get; set;
        }
        public int qualityLevel
        {
            get; set;
        }
        public string supplierLoginId
        {
            get; set;
        }
        public string categoryName
        {
            get; set;
        }
        public string mainVedio
        {
            get; set;
        }
        public string productCargoNumber
        {
            get; set;
        }
        public bool crossBorderOffer
        {
            get; set;
        }
        public string referencePrice
        {
            get; set;
        }
    }
    public class ProductAttributes
    {
        public long attributeID
        {
            get; set;
        }
        public string attributeValue
        {
            get; set;
        }
        public string attributeDisplayName
        {
            get; set;
        }
    }
    public class Image
    {
        public string[] images
        {
            get; set;
        }
        public bool isWatermark
        {
            get; set;
        }
        public bool isWatermarkFrame
        {
            get; set;
        }
        public string watermarkPosition
        {
            get; set;
        }
    }
    public class SkuInfos
    {
        public List<ProductAttributes> attributes
        {
            get; set;
        }
        public string cargoNumber
        {
            get; set;
        }
        public int amountOnSale
        {
            get; set;
        }
        public double retailPrice
        {
            get; set;
        }
        public double price
        {
            get; set;
        }
        public List<PriceRange> priceRange
        {
            get; set;
        }
        public string skuCode
        {
            get; set;
        }
        public string skuId
        {
            get; set;
        }
        public string specId
        {
            get; set;
        }
    }
    public class PriceRange
    {
        public int startQuantity
        {
            get; set;
        }
        public double price
        {
            get; set;
        }
    }
    public class SaleInfo
    {
        public bool supportOnlineTrade
        {
            get; set;
        }
        public bool mixWholeSale
        {
            get; set;
        }
        public string saleType
        {
            get; set;
        }
        public bool priceAuth
        {
            get; set;
        }
        public List<PriceRange> priceRanges
        {
            get; set;
        }
        public bool amountOnSale
        {
            get; set;
        }
        public string unit
        {
            get; set;
        }
        public int minOrderQuantity
        {
            get; set;
        }
        public int batchNumber
        {
            get; set;
        }
        public double retailprice
        {
            get; set;
        }
        public string tax
        {
            get; set;
        }
        public string sellunit
        {
            get; set;
        }
        public int quoteType
        {
            get; set;
        }
    }
    public class ShippingInfo
    {
        public long freightTemplateID
        {
            get; set;
        }
        public double unitWeight
        {
            get; set;
        }
        public string packageSize
        {
            get; set;
        }
        public int volume
        {
            get; set;
        }
        public int handlingTime
        {
            get; set;
        }
        public long sendGoodsAddressId
        {
            get; set;
        }
        public string sendGoodsAddressText
        {
            get; set;
        }
    }
    public class ExtendInfos
    {
        public string key
        {
            get; set;
        }
        public string value
        {
            get; set;
        }
        public string supplierUserId
        {
            get; set;
        }
        public int qualityLevel
        {
            get; set;
        }
        public string supplierLoginId
        {
            get; set;
        }
        public string categoryName
        {
            get; set;
        }
        public string mainVedio
        {
            get; set;
        }
        public string productCargoNumber
        {
            get; set;
        }
        public bool crossBorderOffer
        {
            get; set;
        }
        public string referencePrice
        {
            get; set;
        }
    }
}
