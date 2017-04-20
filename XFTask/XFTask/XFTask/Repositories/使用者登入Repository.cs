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
    /// <summary>
    /// 提供存取後端使用者身分驗證的服務
    /// </summary>
    public class 使用者登入Repository
    {
        /// <summary>
        /// 呼叫 API 回傳後，回報的結果內容
        /// </summary>
        public APIResult fooAPIResult { get; set; } = new APIResult();
        /// <summary>
        /// 身分驗證成功後的使用者紀錄
        /// </summary>
        public Users Item { get; set; } = new Users();


        public 使用者登入Repository()
        {
        }

        /// <summary>
        /// 使用者身分驗證：登入
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<APIResult> GetAsync(string account, string password)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        // http://xamarinhandsonlab.azurewebsites.net/api/UserLogin?account=user1&password=pw1
                        string FooUrl = $"{PCLGlobalHelper.UserLoginAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooUrl}?account={account}&password={password}";
                        #endregion

                        response = await client.GetAsync(fooFullUrl);
                        #endregion

                        #region 處理呼叫完成 Web API 之後的回報結果
                        if (response != null)
                        {
                            // 取得呼叫完成 API 後的回報內容
                            String strResult = await response.Content.ReadAsStringAsync();

                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.OK:
                                    #region 狀態碼為 OK
                                    // 將 API 回傳碼進行反序列會為 .NET 的物件
                                    fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                                    if (fooAPIResult.Success == true)
                                    {
                                        #region API 的狀態碼為成功
                                        // 將 Payload 裡面的內容，反序列化為真實 .NET 要用到的資料
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
                                            // fooAPIResult 直接使用 API 回傳的內容
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region API 的狀態碼為 不成功
                                        Item = new Users();
                                        fooAPIResult = new APIResult
                                        {
                                            Success = false,
                                            Message = fooAPIResult.Message,
                                            Payload = Item,
                                        };
                                        #endregion
                                    }
                                    await Write();
                                    #endregion
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

        /// <summary>
        /// 將資料寫入到檔案內
        /// </summary>
        /// <returns></returns>
        public async Task Write()
        {
            string data = JsonConvert.SerializeObject(Item);
            await StorageUtility.WriteToDataFileAsync("", PCLGlobalHelper.資料主目錄, PCLGlobalHelper.UserLoginAPIName, data);
        }

        /// <summary>
        /// 將資料讀取出來
        /// </summary>
        /// <returns></returns>
        public async Task<Users> Read()
        {
            string data = "";
            data = await StorageUtility.ReadFromDataFileAsync("", PCLGlobalHelper.資料主目錄, PCLGlobalHelper.UserLoginAPIName);
            Item = JsonConvert.DeserializeObject<Users>(data);
            if (Item == null)
            {
                Item = new Users();
            }
            return Item;
        }
    }
}
