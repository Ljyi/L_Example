using Alibaba.Console.Model;
using Alibaba.Requests;
using Alibaba.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OtherCommon;
using Shopee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Alibaba.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "{\"changedRows\":1,\"success\":[{\"ffid\":5081368743,\"item_id\":7449585824,\"message\":\"Successful\"}],\"error\":[]}";
            BulkmarkShippedResponse bulkmarkShippedResponse = JsonConvert.DeserializeObject<BulkmarkShippedResponse>(result);
            bulkmarkShippedResponse.success.ForEach(p =>
            {
                if (p.message == "Successful")
                {
                    string msg = p.message;
                }
                else
                {
                    string msg = p.message;
                }
            });

            DataTable dataTable = DataTableHelper.InitBatchData();
            string dataTableJson = JsonConvert.SerializeObject(dataTable);

            Test test = new Test();
            List<string> list = new List<string>();
            string str1 = "0";
            test.ListTest(list, str1);

            string zp = nameof(result);


            //ShopeeTest.ConvertJsonToModel(ShopeeTest.jsonResult);
            //ShopeeTest.GetEscrowDetails();
            ShopeeTest.GetOrderDetails();
            //  ParameterToJson();
            // PaytmShipped();
            //   PaytmShipped1();
            //CreateOrderPreview();
            //GetSubaccountAuthInfo();
            //  SubaccountAuthCancel(new List<string>() { "深圳傲基2018:fz006" });
            // LoadProduct();
            //   GetSubaccountAuthInfo();
            //   GetSubaccountAuth();
            //GetMessage();
            // ConvertStringToDateTime();
            // GetTime("21000101000000000+0800");
            // GetSubaccountAuth();
            //GetSubAccounts();
            // GetProductId();
            //ResultModel<SyncProductPushedResponse> resultModel = SyncProductsPushed();
            //UpLoadOrder();
            //  LoadProduct();
            // CreateOrderPreview();
            //   CreateOrder();
            //CancelOrder();
            // RefeshToken();
            //UpLoadOrder();
            // GetReceiveAddress();
            // ResultModel<SubAccountResponse> resultModel = GetSubAccounts();
            //   ResultModel<ReceiveAddressItems> result = GetReceiveAddress();
        }



        public static ResultModel<SubAccountResponse> GetSubAccounts()
        {
            ResultModel<SubAccountResponse> resultModel = new ResultModel<SubAccountResponse>();
            GetSubAccountsParam subAccountsParam = new GetSubAccountsParam("ljy");
            subAccountsParam.GetRequestUrl();
            string url = subAccountsParam.URL;
            string postData = string.Join("&", subAccountsParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {
                        //进行序列化
                        if (!string.IsNullOrEmpty(responseResult.Result))
                        {
                            try
                            {
                                SubAccountResponse subAccountResponse = JsonConvert.DeserializeObject<SubAccountResponse>(responseResult.Result);
                                if (subAccountResponse != null)
                                {
                                    resultModel.Success = subAccountResponse.success;
                                    resultModel.Result = subAccountResponse;
                                }
                            }
                            catch (Exception ex)
                            {
                                string meg = ex.Message;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }
            }
            return resultModel;
        }

        /// <summary>
        /// 获取收货地址
        /// </summary>
        public static ResultModel<ReceiveAddressItems> GetReceiveAddress()
        {
            ResultModel<ReceiveAddressItems> resultModel = new ResultModel<ReceiveAddressItems>();
            ReceiveAddressParam receiveAddressParam = new ReceiveAddressParam();
            receiveAddressParam.GetRequestUrl();
            string url = receiveAddressParam.URL;
            string postData = string.Join("&", receiveAddressParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {
                        //进行序列化
                        if (!string.IsNullOrEmpty(responseResult.Result))
                        {
                            try
                            {
                                ReceiveAddressResponse productResponse = JsonConvert.DeserializeObject<ReceiveAddressResponse>(responseResult.Result);
                                resultModel.Code = productResponse.code;
                                resultModel.ErrorCode = productResponse.errorCode;
                                resultModel.ErrorMessage = productResponse.errorMessage;
                                if (!string.IsNullOrEmpty(productResponse.message))
                                {
                                    resultModel.Message = productResponse.message;
                                }
                                resultModel.Success = productResponse.success;
                                resultModel.Result = productResponse.result;
                            }
                            catch (Exception ex)
                            {
                                string meg = ex.Message;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }
            }
            return resultModel;
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        public static void RefeshToken()
        {
            RefreshTokenParam refreshTokenParam = new RefreshTokenParam();
            refreshTokenParam.GetRequestUrl();
            string url = refreshTokenParam.URL;
            string postData = string.Join("&", refreshTokenParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }

            }
        }

        public static ResultModel<SyncProductPushedResponse> SyncProductsPushed()
        {
            ResultModel<SyncProductPushedResponse> resultModel = new ResultModel<SyncProductPushedResponse>();
            SyncProductsPushedParam syncProductsPushed = new SyncProductsPushedParam();
            syncProductsPushed.ProductIdList = new List<string>() { "571417124858" };//531166632724 ,45516189923,45420491343,554833936110,565461451556，561577651929,569211240067,524889799249
            syncProductsPushed.GetRequestUrl();
            string url = syncProductsPushed.URL;
            string postData = string.Join("&", syncProductsPushed.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                SyncProductPushedResponse productResponse = JsonConvert.DeserializeObject<SyncProductPushedResponse>(responseResult.Result);
                resultModel.Result = productResponse;
            }
            return resultModel;
        }
        /// <summary>
        /// 下载产品
        /// </summary>
        public static void LoadProduct()
        {
            GetProductParam getProductParam = new GetProductParam();
            getProductParam.productId = 547363119296;// 576620941437 565461451556  561577651929 554833936110
            getProductParam.GetRequestUrl();
            string url = getProductParam.URL;
            string postData = string.Join("&", getProductParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {
                        GetProductResponse productResponse = JsonConvert.DeserializeObject<GetProductResponse>(responseResult.Result);
                        string productId = productResponse.productInfo.productID.ToString();
                        string staus = productResponse.productInfo.status;
                        string subject = productResponse.productInfo.subject;
                        string categoryName = productResponse.productInfo.categoryName;
                        List<SkuInfos> skuInfosList = new List<SkuInfos>();
                        List<SkuInfos> list = productResponse.productInfo.skuInfos;
                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }

            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public static void CancelOrder()
        {
            CancelOrderParam cancelOrderParam = new CancelOrderParam();
            cancelOrderParam.tradeID = 214498786581086919;//213485383312086919 214228696119086919
            cancelOrderParam.remark = "测试";
            cancelOrderParam.cancelReason = "buyerCancel";
            cancelOrderParam.RequestType = "CancelOrder";
            cancelOrderParam.GetRequestUrl();
            string url = cancelOrderParam.URL;
            string postData = string.Join("&", cancelOrderParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }

            }
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        public static void CreateOrder()
        {
            CreateOrderParam createOrderParam = new CreateOrderParam("ljy");
            // createOrderParam.invoiceParam = GetInvoiceParam();
            createOrderParam.addressParam = GetAddressParam();
            createOrderParam.cargoParamList = GetCargoParamList();
            createOrderParam.tradeType = "fxassure";
            createOrderParam.message = "请帮我打印(手写也可）以下信息两份，一份放货里面（每箱装的实物清单），一份贴外箱上（如果两款产品以上的务必做区分，拒收混装)  1.外箱上面写上实际重量；  2.采购员：朱锦兰  电话：18123759489  3.产品编号：98528 （中性）私密比基尼剃刀模版+ 剃毛刀 数量：20；  4.订单编号：PO18100400429  5.快递单地址后面请填下:PO18100400429";
            createOrderParam.GetRequestUrl();
            string url = createOrderParam.URL;
            string postData = CreateParameterStr(createOrderParam.m_DictParameters);


            //     string.Join("&", createOrderParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));


            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {
                        GetOrdersResponse ordersResponse = JsonConvert.DeserializeObject<GetOrdersResponse>(responseResult.Result);
                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }

            }
        }

        /// <summary>
        /// 下载订单
        /// </summary>
        public static void UpLoadOrder()
        {
            GetOrderParam getOrderParam = new GetOrderParam();
            getOrderParam.orderId = 229560749877086919;//209934924621086919,213485383312086919
            getOrderParam.includeFields = "GuaranteesTerms,NativeLogistics,RateDetail,OrderInvoice,OrderBizInfo,baseInfo,productItems";//
            getOrderParam.GetRequestUrl();
            HttpClientRequest httpClient = new HttpClientRequest();
            string postData = string.Join("&", getOrderParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            ResponseResult responseResult = httpClient.RequesResult(getOrderParam.URL, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {
                        GetOrdersResponse ordersResponse = JsonConvert.DeserializeObject<GetOrdersResponse>(responseResult.Result);
                        if (ordersResponse != null)
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }

            }
        }

        /// <summary>
        /// 创建订单预览
        /// </summary>
        public static void CreateOrderPreview()
        {
            CreateOrderPreviewParam createOrderPreviewParam = new CreateOrderPreviewParam();
            //  createOrderPreviewParam.invoiceParam = GetInvoiceParam();
            createOrderPreviewParam.addressParam = GetAddressParam();
            createOrderPreviewParam.cargoParamList = GetCargoParamList();
            createOrderPreviewParam.GetRequestUrl();
            string url = "https://gw.open.1688.com/openapi/param2/1/com.alibaba.trade/alibaba.createOrder.preview/6368408";// createOrderPreviewParam.URL;
            string postData = string.Join("&", createOrderPreviewParam.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                //进行序列化
                if (!string.IsNullOrEmpty(responseResult.Result))
                {
                    try
                    {
                        CreateOrderPreviewResponse productResponse = JsonConvert.DeserializeObject<CreateOrderPreviewResponse>(responseResult.Result);
                    }
                    catch (Exception ex)
                    {
                        string meg = ex.Message;
                    }
                }
            }
        }
        public static string GetSign()
        {
            string s = "param2/1/system/currentTime/1000000a1b2";
            s = "param2/1/com.alibaba.trade/alibaba.trade.get.buyerView/1460267/";
            byte[] signatureKey = Encoding.UTF8.GetBytes("2W5jNtJ6o7Bw");//test123
            HMACSHA1 hmacsha1 = new HMACSHA1(signatureKey);
            hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(s));
            byte[] hash = hmacsha1.Hash;
            string Sign = BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();
            return Sign;
        }
        private string sign(string urlPath, Dictionary<string, string> paramDic)
        {
            byte[] signatureKey = Encoding.UTF8.GetBytes("2W5jNtJ6o7Bw");//此处用自己的签名密钥
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> kv in paramDic)
            {
                list.Add(kv.Key + kv.Value);
            }
            list.Sort();
            string tmp = urlPath;
            foreach (string kvstr in list)
            {
                tmp = tmp + kvstr;
            }
            HMACSHA1 hmacsha1 = new HMACSHA1(signatureKey);
            hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(tmp));
            byte[] hash = hmacsha1.Hash;
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();
        }

        public static string GetAddressParam()
        {
            try
            {
                AddressParam addressParam = new AddressParam()
                {
                    fullName = "朱锦兰",
                    mobile = "18123759489",
                    //  phone = "0517-88990077",
                    //   postCode = "000000",
                    provinceText = "广东省",
                    address = "华南工业园金富二路一号兆峰轴承有限公司院内傲基仓库",
                    //    areaText = "p7",
                    townText = "寮步镇",
                    districtCode = "",
                    cityText = "东莞市",
                };
                return JsonConvert.SerializeObject(addressParam);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return msg;
            }
        }

        public static string GetInvoiceParam()
        {
            try
            {
                InvoiceParam invoiceParam = new InvoiceParam()
                {
                    provinceText = "浙江省",
                    cityText = "杭州市",
                    areaText = "滨江区",
                    townText = "长河镇",
                    postCode = "333333",
                    address = "网商路699号",
                    fullName = "张三",
                    phone = "0517-88990077",
                    mobile = "15251667788",
                    companyName = "测试公司",
                    taxpayerIdentifier = "12345",
                    bankAndAccount = "网商银行",
                    localInvoiceId = "123123123",
                };
                return JsonConvert.SerializeObject(invoiceParam);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetCargoParamList()
        {
            List<CargoParamList> list = new List<CargoParamList>();
            try
            {
                CargoParamList cargoParamList = new CargoParamList()
                {
                    offerId = 537073987625,
                    quantity = 20,
                    specId = "48402f2f33f53e9a7cf7d5de5f55dcf2",
                };
                list.Add(cargoParamList);
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static List<string> GetProductId()
        {
            List<string> productList = new List<string>() {
                "https://detail.1688.com/offer/1176110637.html",
                "https://detail.1688.com/offer/535651012524.html?spm=a312h.7841636.1998813769.d_pic_7.meMoWK&tracelog=p4p",
                "https://detail.1688.com/offer/41514323279.html?spm=b26110380.sw1688.0.0.JIf6QN",
                "http://detail.1688.com/offer/1176110637.html"
            };

            List<string> ids = new List<string>();

            foreach (var item in productList)
            {
                int endLeng = item.IndexOf(".html") - 6;
                int straleng = item.IndexOf("offer/");
                ids.Add(item.Substring(straleng + 6, endLeng - straleng));
            }
            return ids;
        }

        #region 取消子账号授权 
        /// <summary>
        /// 取消子账号授权
        /// </summary>
        /// <param name="appAccount"></param>
        /// <param name="accountList"></param>
        /// <returns></returns>
        public static ResultModel<SubaccountAuthCancelResponse> SubaccountAuthCancel(List<string> accountList)
        {
            ResultModel<SubaccountAuthCancelResponse> resultModel = new ResultModel<SubaccountAuthCancelResponse>() { Success = true };
            SubaccountAuthCancelParam subaccountAuthCancel = new SubaccountAuthCancelParam();
            //获取子账号列表
            subaccountAuthCancel.SubUserIdentityList = accountList;
            subaccountAuthCancel.GetRequestUrl();
            string url = subaccountAuthCancel.URL;
            string postData = string.Join("&", subaccountAuthCancel.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData, subaccountAuthCancel.RequestType);
            if (responseResult.Success)
            {
                SubaccountAuthCancelResponse subaccountAuthResult = JsonConvert.DeserializeObject<SubaccountAuthCancelResponse>(responseResult.Result);
                resultModel.Result = subaccountAuthResult;
            }
            else
            {
                resultModel.Success = false;
            }
            return resultModel;
        }
        #endregion

        public static ResultModel<SubaccountAuthResult> GetSubaccountAuthInfo()
        {

            //string encodedUrl = "%5B%22%E6%B7%B1%E5%9C%B3%E5%82%B2%E5%9F%BA2018%3Axs057%22%5D";
            //string decodedUrl = HttpUtility.UrlDecode(encodedUrl);
            //encodedUrl = HttpUtility.UrlEncode(decodedUrl);
            ResultModel<SubaccountAuthResult> resultModel = new ResultModel<SubaccountAuthResult>() { Success = true };
            GetSubaccountAuthInfoParam subaccountAuth = new GetSubaccountAuthInfoParam();
            subaccountAuth.SubUserIdentityList = new List<string>() { "深圳傲基2018:ao504" };
            subaccountAuth.GetRequestUrl();
            string url = subaccountAuth.URL;
            // string postData =  CreateParameterStr(subaccountAuth.m_DictParameters);
            string postData = string.Join("&", subaccountAuth.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                SubaccountAuthResult subaccountAuthResult = JsonConvert.DeserializeObject<SubaccountAuthResult>(responseResult.Result);
                resultModel.Result = subaccountAuthResult;
            }
            else
            {
                resultModel.Success = false;
            }
            return resultModel;
        }

        public static ResultModel<SubaccountAuthResponse> GetSubaccountAuth()
        {
            ResultModel<SubaccountAuthResponse> resultModel = new ResultModel<SubaccountAuthResponse>();
            List<SubaccountAuthResponse> subaccountAuthResponses = new List<SubaccountAuthResponse>();
            SubaccountAuthParam subaccountAuth = new SubaccountAuthParam();
            subaccountAuth.SubUserIdentityList = new List<string>() { "深圳傲基2018:b0204", "深圳傲基2018:xs058", "深圳傲基2018:xs0581" };
            subaccountAuth.GetRequestUrl();
            string url = subaccountAuth.URL;
            string postData = string.Join("&", subaccountAuth.m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            HttpClientRequest httpClient = new HttpClientRequest();
            ResponseResult responseResult = httpClient.RequesResult(url, postData);
            if (responseResult.Success)
            {
                JObject resultObject = JObject.Parse(responseResult.Result);
                JToken result = resultObject as JToken;
                if (result["returnValue"].ToString() != "[]")
                {
                    JObject jObj = JObject.Parse(result["returnValue"].ToString());
                    foreach (var item in subaccountAuth.SubUserIdentityList)
                    {
                        if (jObj.SelectToken(item) != null)
                        {
                            SubaccountAuthResponse subaccountAuthResponse = new SubaccountAuthResponse();
                            subaccountAuthResponse.accessToken = jObj.SelectToken(item)["accessToken"].ToString();
                            subaccountAuthResponse.aliId = jObj.SelectToken(item)["aliId"].ToString();
                            subaccountAuthResponse.memberId = jObj.SelectToken(item)["memberId"].ToString();
                            subaccountAuthResponse.resourceOwnerId = jObj.SelectToken(item)["resourceOwnerId"].ToString();
                            subaccountAuthResponse.accessTokenTimeout = jObj.SelectToken(item)["accessTokenTimeout"].ToString();
                            subaccountAuthResponses.Add(subaccountAuthResponse);
                        }

                    }
                }
                resultModel.ResultList = subaccountAuthResponses;
            }
            else
            {
                resultModel.Success = false;
            }
            return resultModel;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string GetTime(string timeStamp)
        {
            //21000101000000000+0800
            //处理字符串,截取括号内的数字
            var strStamp = timeStamp;// Regex.Matches(timeStamp, @"(?<=\()((?<gp>\()|(?<-gp>\))|[^()]+)*(?(gp)(?!))").Cast<Match>().Select(t => t.Value).ToArray()[0].ToString();
            //处理字符串获取+号前面的数字
            var str = Convert.ToInt64(strStamp.Substring(0, strStamp.IndexOf("+")));
            long timeTricks = new DateTime(1970, 1, 1).Ticks + str + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks).ToString("yyyy-MM-dd HH:mm:ss");

        }
        private DateTime GetTimeq(string timeStamp)
        {

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }
        /// <summary>       
        /// 时间戳转为C#格式时间    timeStamp=146471041000   
        /// </summary>       
        /// <param name=”timeStamp”></param>       
        /// <returns></returns>       
        private static DateTime ConvertStringToDateTime(string timeStamp = "21000101000000000")
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp);
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        public static void GetMessage()
        {
            HttpClientRequest httpClientRequest = new HttpClientRequest();
            string url = "http://localhost:59004/AlibabaMessage/Messages";// "http://www.bestmine.net/AlibabaMessage/Messages";
            string postDataStr = @"message={ 'data':{'sellerMemberId':'b2b-1676547900b7bb3','orderId':167539019420540000,'buyerMemberId':'b2b-665170100','currentStatus': 'waitbuyerpay'},'gmtBorn':1527556000,'msgId':70299002,'type':'PRODUCT_PRODUCT_EXPIRE','userInfo':'CBU_MemberId'}&_aop_signature=65F2AA13AE9C9C9DFE8682EFB90B32F37CB01BE1";
            httpClientRequest.RequesResult(url, postDataStr);
        }
        private static string CreateParameterStr(SortedDictionary<string, string> parameters)
        {
            StringBuilder paramBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                String encodedValue = null;
                if (kvp.Value != null)
                {
                    String tempValue = kvp.Value.ToString();
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tempValue);
                    encodedValue = HttpUtility.UrlEncode(byteArray, 0, byteArray.Length);
                }
                paramBuilder.Append(kvp.Key).Append("=").Append(encodedValue);
                paramBuilder.Append("&");
            }
            return paramBuilder.ToString().TrimEnd('&');
        }

        public static void SchemalevelTraverse(JToken json, ref Dictionary<string, string> jsonDic, string kye = "深圳傲基2018:xs058")
        {
            //如果没有属性节点了，说明已经是叶子节点了
            if (json.SelectToken("returnValue") == null)
            {
                if (json.SelectToken("items") == null)
                {
                    if (json.SelectToken("description") != null)  //这里是我用于填充规则的
                    {
                        string rule = json.Value<string>("description");//从Json里取出规则值
                        if (!jsonDic.ContainsKey(json.Path))
                        {
                            jsonDic.Add(json.Path, rule);
                        }
                    }
                }
                else
                {
                    var itemProperties = json.SelectToken("items").SelectToken("properties");
                    if (itemProperties != null)
                    {
                        foreach (var item in itemProperties)
                        {
                            if (item.First != null)
                            {
                                SchemalevelTraverse(item.First, ref jsonDic);
                            }
                        }
                    }
                }
                return;
            }

            foreach (var item in json.SelectToken(kye))    //循环所有子节点
            {
                if (item.First != null)
                {
                    SchemalevelTraverse(item.First, ref jsonDic);   //递归调用
                }
            }
        }

        public static void PaytmShipped()
        {
            string paytmShipmentUrl = "https://fulfillment.paytm.com/v1/merchant/{0}/fulfillment/create/{1}?authtoken={2}";
            string paymDownloadPackingLabelUrl = "https://fulfillment.paytm.com/v2/invoice/merchant/{0}/fulfillment/bulkfetch?fulfillment_ids={1}&template=shared&authtoken={2}&ffUpdate=true";
            string paytmShippedUrl = "https://fulfillment.paytm.com/v1/merchant/{0}/fulfillment/bulkmarkshipped?fulfillment_ids={1}&authtoken={2}";
            string weigth = "0.030000";
            string token = "4162fcb7-0dfd-40f9-822e-18223735b8ca";
            string merchantID = "572305";
            string orderId = "6962500622";
            string[] itemIdList = new string[] { "7449585824" };
            string trackingNum = "682390067";
            paytmShipmentUrl = String.Format(paytmShipmentUrl, merchantID, orderId, token);
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                try
                {
                    CreateShipmentRequstParameter parameter = new CreateShipmentRequstParameter()
                    {
                        order_item_ids = itemIdList,
                        tracking_url = "https://www.17track.net",
                        shipper_id = "159",
                        shipping_description = "shipping_description",
                        tracking_number = trackingNum,
                        weight = weigth
                    };
                    string json = JsonConvert.SerializeObject(parameter);
                    HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    HttpResponseMessage response = client.PostAsync(paytmShipmentUrl, contentPost).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(result))
                    {
                        CreateShipmentRequstResponse shipmentRequstResponse = JsonConvert.DeserializeObject<CreateShipmentRequstResponse>(result);
                        if (shipmentRequstResponse.res == "success")
                        {
                            if (shipmentRequstResponse.fulfillment_details.status == "15")
                            {

                            }
                            //下载标签
                            paymDownloadPackingLabelUrl = String.Format(paymDownloadPackingLabelUrl, merchantID, shipmentRequstResponse.fulfillment_id, token);
                            contentPost = new StringContent("", Encoding.UTF8, "application/json");
                            response = client.GetAsync(paymDownloadPackingLabelUrl).Result;
                            result = response.Content.ReadAsStringAsync().Result;

                            //标识发货
                            paytmShippedUrl = String.Format(paytmShippedUrl, merchantID, shipmentRequstResponse.fulfillment_id, token);
                            contentPost = new StringContent("", Encoding.UTF8, "application/json");
                            response = client.PostAsync(paytmShippedUrl, contentPost).Result;
                            result = response.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(result))
                            {
                                BulkmarkShippedResponse bulkmarkShippedResponse = JsonConvert.DeserializeObject<BulkmarkShippedResponse>(result);
                                if (bulkmarkShippedResponse.changedRows > 0)
                                {

                                    //bulkmarkShippedResponse.success.ForEach(p =>
                                    //{
                                    //    if (p.message == "Successful")
                                    //    {
                                    //        string msg = p.message;
                                    //    }
                                    //    else
                                    //    {
                                    //        string msg = p.message;
                                    //    }
                                    //});
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                }
            }
        }

        public static void PaytmShipped1()
        {
            try
            {

                string token1 = "7a9aef55-7723-426f-a078-86cf78d5282c";
                string merchantID1 = "572305";
                string orderId1 = "6912017838";
                string[] itemIdList1 = new string[] { "7397083852" };
                string trackingNum1 = "682156311";
                string paytmShipmentUrl1 = "https://fulfillment.paytm.com/v1/merchant/{0}/fulfillment/create/{1}?Authtoken={2}";
                paytmShipmentUrl1 = String.Format(paytmShipmentUrl1, merchantID1, orderId1, token1);
                string userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31";
                CreateShipmentRequstParameter parameter = new CreateShipmentRequstParameter()
                {
                    order_item_ids = itemIdList1,
                    tracking_url = "https://www.17track.net",
                    shipper_id = "159",
                    shipping_description = "Gati_International",
                    tracking_number = trackingNum1,
                    weight = "0.029000"
                };
                string json = JsonConvert.SerializeObject(parameter);
                HttpWebRequest httpRequest = null;
                paytmShipmentUrl1 = "https://fulfillment.paytm.com/v1/merchant/572305/fulfillment/bulkmarkshipped?fulfillment_ids=6912017838&authtoken=7a9aef55-7723-426f-a078-86cf78d5282c";
                httpRequest = (HttpWebRequest)WebRequest.Create(paytmShipmentUrl1);
                httpRequest.UserAgent = userAgent;// "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json; charset=utf-8";//返回的数据格式
                //byte[] bytesToPost = Encoding.UTF8.GetBytes(json);//转换请求数据为二进制
                //httpRequest.ContentLength = bytesToPost.Length;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //Stream requestStream = httpRequest.GetRequestStream();
                //requestStream.Write(bytesToPost, 0, bytesToPost.Length);
                //requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public static void ParameterToJson()
        {

            IDataParameter[] Parameters = {
                                                  new SqlParameter("@Sku", SqlDbType.NVarChar ),
                                                  new SqlParameter("@PurchaseOrderNo", SqlDbType.NVarChar),
                                                  new SqlParameter("@SupplyName", SqlDbType.NVarChar ),
                                                  new SqlParameter("@PurchaseStatus", SqlDbType.NVarChar),
                                                  new SqlParameter("@SettleType", SqlDbType.NVarChar ),
                                                  new SqlParameter("@PayStatus", SqlDbType.NVarChar ),
                                                  new SqlParameter("@AuduitStatus", SqlDbType.NVarChar ),
                                                  new SqlParameter("@LeftPurStatus",SqlDbType.NVarChar),
                                                  new SqlParameter("@BeginTime",SqlDbType.NVarChar),
                                                  new SqlParameter("@EndTime",SqlDbType.NVarChar),
                                                  new SqlParameter("@ShowMode",SqlDbType.Int)
                                          };
            Parameters[0].Value = "skus";
            Parameters[1].Value = "purchaseOrderNo";
            Parameters[2].Value = "SupplyName";
            Parameters[3].Value = "PurchaseStatusList";
            Parameters[4].Value = "SettleType";
            Parameters[5].Value = "PayStatusList";
            Parameters[6].Value = "AuduitStatusList";
            Parameters[7].Value = "LeftPurStatus";
            Parameters[8].Value = "BeginTime";
            Parameters[9].Value = "EndTime";
            Parameters[10].Value = "ShowMode";
            string json = JsonConvert.SerializeObject(Parameters.Select(p => new { p.ParameterName, p.Value }));

        }
    }

}
