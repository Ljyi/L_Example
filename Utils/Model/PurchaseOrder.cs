using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Model
{
    public class PurchaseContract : ExportBase
    {
        /// <summary>
        /// 甲方
        /// </summary>
        public string PartyA { get; set; }

        /// <summary>
        /// 乙方
        /// </summary>
        public string PartyB { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 签订地点
        /// </summary>
        public string ContractedAddress { get; set; }
        /// <summary>
        /// 签订时间
        /// </summary>
        public string ContractedTime { get; set; }
        /// <summary>
        /// 甲方地址
        /// </summary>
        public string AddressA { get; set; }
        /// <summary>
        /// 乙方地址
        /// </summary>
        public string AddressB { get; set; }
        /// <summary>
        /// 甲方联系电话
        /// </summary>
        public string PartyAPhone { get; set; }
        /// <summary>
        /// 乙方联系电话
        /// </summary>
        public string PartyBPhone { get; set; }
        /// <summary>
        /// 甲方开户行
        /// </summary>
        public string PartyABankName { get; set; }

        /// <summary>
        /// 乙方开户行
        /// </summary>
        public string PartyBBankName { get; set; }

        /// <summary>
        /// 甲方开户行账户
        /// </summary>
        public string PartyABankAccount { get; set; }

        /// <summary>
        /// 乙方开户行账户
        /// </summary>
        public string PartyBBankAccount { get; set; }
    }

    public class PurchaseContractDetaile : ExportBase
    {
        /// <summary>
        /// 采购单号
        /// </summary>
        public string 采购单号 { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string 品名 { get; set; }
        /// <summary>
        /// SKU
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string 产品描述 { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string 单位 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int 数量 { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal? 单价 { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? 采购成本 { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? 金额 { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal? 税金 { get; set; }
        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal? 价税合计 { get; set; }

        public void SetTaxValue(bool IsTax)
        {
            try
            {
                if (单价.HasValue)
                {
                    单价 = Math.Round(单价.Value, 2);
                }
                if (金额.HasValue)
                {
                    金额 = Math.Round(金额.Value, 2);
                }
            }
            catch (Exception)
            {

            }
        }
    }
    /// <summary>
    /// 采购订单导出类
    /// </summary>
    public class PurchaseOrderExport : ExportBase
    {
        /// <summary>
        /// 所属公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 单据性质(是否含税)
        /// </summary>
        public bool IsTax { get; set; }
        /// <summary>
        /// 单据性质
        /// </summary>
        public string TaxType { get; set; }

        /// <summary>
        /// 币别
        /// </summary>
        public string CurrencyType { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string PurchaseOrderNo { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }
        /// <summary>
        /// 入库仓
        /// </summary>
        public string StoreHouse { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalNumber { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// 大写总金额
        /// </summary>
        public string UpperTotalAmount { get; set; }
        /// <summary>
        /// 总税额
        /// </summary>
        public decimal? TotalTaxAmount { get; set; }
        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal? TotalTaxAndPriceAmount { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        public string PurchaseUser { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 采购订单明细
        /// </summary>
        public List<PurchaseOrderDetaile> OrderDetaileItem { get; set; }

        public void SetValue()
        {
            try
            {
                TotalNumber = OrderDetaileItem.Select(p => p.数量).Sum();
                TotalAmount = OrderDetaileItem.Select(p => p.金额).Sum();
                if (TotalAmount.HasValue)
                {
                    TotalAmount = Math.Round(TotalAmount.Value, 2);
                }
                TaxType = "不含税";
                if (IsTax)
                {
                    TaxType = "含税";
                    TotalTaxAmount = OrderDetaileItem.Select(p => p.税额).Sum();
                    TotalTaxAndPriceAmount = OrderDetaileItem.Select(p => p.价税合计).Sum();
                    if (TotalTaxAmount.HasValue)
                    {
                        TotalTaxAmount = Math.Round(TotalTaxAmount.Value, 2);
                    }
                    if (TotalTaxAndPriceAmount.HasValue)
                    {
                        TotalTaxAndPriceAmount = Math.Round(TotalTaxAndPriceAmount.Value, 2);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
    /// <summary>
    /// 采购订单导出明细类
    /// </summary>
    public class PurchaseOrderDetaile : ExportBase
    {
        /// <summary>
        /// SKU
        /// </summary>
        public string SKU { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string 名称 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int 数量 { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? 单价 { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? 金额 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string 图片 { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? 交付日期 { get; set; }
        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal? 含税单价 { get; set; }
        /// <summary>
        /// 交易税率
        /// </summary>
        public double? 交易税率 { get; set; }
        /// <summary>
        /// 增值税率
        /// </summary>
        public decimal? 增值税率 { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal? 税额 { get; set; }
        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal? 价税合计 { get; set; }

        public void SetTaxValue(bool IsTax)
        {
            try
            {

                //价税合计 = 含税单价 * 数量；
                //金额 = 价税合计 / (1 + 增值税率)；
                //税额 = 价税合计 - 金额
                if (IsTax)
                {
                    if (含税单价.HasValue)
                    {
                        价税合计 = 含税单价 * 数量;
                        含税单价 = Math.Round(含税单价.Value, 2);
                    }
                    if (价税合计.HasValue)
                    {
                        金额 = 价税合计 / (1 + 增值税率);
                        价税合计 = Math.Round(价税合计.Value, 2);
                    }
                    if (金额.HasValue)
                    {
                        税额 = (价税合计 - 金额);
                        金额 = Math.Round(金额.Value, 2);
                    }
                    if (税额.HasValue)
                    {
                        税额 = Math.Round(税额.Value, 2);
                        //decimal.Parse(税额.Value.ToString("#0.00"));
                    }
                }
                if (单价.HasValue)
                {
                    单价 = Math.Round(单价.Value, 2);
                }
                if (金额.HasValue)
                {
                    金额 = Math.Round(金额.Value, 2);
                }
                if (增值税率.HasValue)
                {
                    增值税率 = Math.Round(增值税率.Value, 2);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
