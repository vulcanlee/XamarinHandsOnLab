using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XFTask.Helpers;
using XFTask.Models;

namespace XFTask.Repositories
{
    public class 使用者登入Repository
    {
        public APIResult fooAPIResult { get; set; } = new APIResult();
        public Users Item { get; set; } = new Users();


        public 使用者登入Repository()
        {
        }

        public async Task<APIResult> GetAsync(string account, string password)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        // http://xamarinhandsonlab.azurewebsites.net/api/UserLogin?account=user1&password=pw1
                        string FooUrl = $"{PCLGlobal.UserLoginAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        #region  進行 RESTfull API 呼叫
                        // 使用 Get 方式來呼叫
                        var fooFullUrl = $"{FooUrl}?account={account}&password={password}";
                        response = await client.GetAsync(fooFullUrl);

                        //else if (httpMethod == HttpMethod.Post)
                        //{
                        //    // 使用 Post 方式來呼叫
                        //    if (EncodingType == EnctypeMethod.FORMURLENCODED)
                        //    {
                        //        // 使用 FormUrlEncoded 方式來進行傳遞資料的編碼
                        //        response = await client.PostAsync(ub.Uri, dic.ToFormUrlEncodedContent());
                        //    }
                        //    else if (EncodingType == EnctypeMethod.XML)
                        //    {
                        //        response = await client.PostAsync(ub.Uri, new StringContent(dic["XML"], Encoding.UTF8, "application/xml"));
                        //    }
                        //    else if (EncodingType == EnctypeMethod.JSON)
                        //    {
                        //        client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                        //        response = await client.PostAsync(ub.Uri, new StringContent(dic["JSON"], Encoding.UTF8, "application/json"));
                        //    }
                        //}
                        //    else
                        //    {
                        //    throw new NotImplementedException("Not Found HttpMethod");
                        //}
                        #endregion

                        #region Response
                        if (response != null)
                        {
                            String strResult = await response.Content.ReadAsStringAsync();

                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.OK:
                                    fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                                    if (fooAPIResult.Success == true)
                                    {
                                        Item = JsonConvert.DeserializeObject<Users>(fooAPIResult.Payload.ToString());
                                        if (Item == null)
                                        {
                                            Item = new Users();

                                            fooAPIResult = new APIResult
                                            {
                                                Success = false,
                                                Message = $"回傳的 API 內容不正確 : {fooAPIResult.Payload.ToString()}",
                                                Payload = null,
                                            };
                                        }
                                        else
                                        {
                                        }
                                    }
                                    else
                                    {
                                        Item = new Users();
                                        fooAPIResult = new APIResult
                                        {
                                            Success = false,
                                            Message = fooAPIResult.Message,
                                            Payload = Item,
                                        };

                                    }
                                    string data = JsonConvert.SerializeObject(Item);
                                    await StorageUtility.WriteToDataFileAsync("", PCLGlobal.資料主目錄, PCLGlobal.UserLoginAPIName, data);
                                    break;

                                default:
                                    fooAPIResult = new APIResult
                                    {
                                        Success = false,
                                        Message = string.Format("Error Code:{0}, Error Message:{1}", response.StatusCode, response.Content),
                                        Payload = null,
                                    };
                                    break;
                            }
                        }
                        else
                        {
                            fooAPIResult = new APIResult
                            {
                                Success = false,
                                Message = "應用程式呼叫 API 發生異常",
                                Payload = null,
                            };
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        fooAPIResult = new APIResult
                        {
                            Success = false,
                            Message = ex.Message,
                            Payload = ex,
                        };
                    }
                }
            }

            return fooAPIResult;
        }
    }
}
