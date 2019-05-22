using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using Utils.Model;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PurchaseContract purchaseContract = new PurchaseContract()
            {
                PartyA = "傲基国际有限公司",
                PartyB = "深圳市长知科技有限公司",
                ContractNo = "HT18071000024",
                AddressA = "深圳市龙岗区平湖街道华南大道一号华南国际印刷纸品包装物流区（一期）P09栋102号",
                AddressB = "深圳市宝安区沙井街道蚝三工业区1栋工厂厂房三层",
                ContractedAddress = "深圳",
                ContractedTime = "2018-07-10",
                PartyABankAccount = "817-646268-838",
                PartyABankName = "HSBC Hong Kong",
                PartyAPhone = "0755-33622851",
                PartyBPhone = "-",
                PartyBBankAccount = "4000022509200619286",
                PartyBBankName = "中国工商银行深圳沙井支行",
            };
            List<PurchaseContractDetaile> list = new List<PurchaseContractDetaile>() {
                new PurchaseContractDetaile(){
                     采购单号="CG15092100901",
                     品名="充电器",
                     SKU="GET46762",
                     产品描述="AUKEY PA-T2 2+1快充充电器 总功率：42W 黑色",
                     单位="个",
                     数量=60,
                     单价=39.77M,
                     采购成本=2386.20M,
                     税金=0.00M,
                     价税合计=2386.20M
                },
                 new PurchaseContractDetaile(){
                     采购单号="CG15092100901",
                     品名="充电器",
                     SKU="GET46762",
                     产品描述="AUKEY PA-T2 2+1快充充电器 总功率：42W 黑色",
                     单位="个",
                     数量=60,
                     单价=39.77M,
                     采购成本=2386.20M,
                     税金=0.00M,
                     价税合计=2386.20M
                },
            };

            SpireWord.GetDocument();
            SpireWord.ReplaseTemplateWord(purchaseContract);
            SpireWord.AddTable(list);
            SpireWord.CreateNewWord();
        }
    }
}
