using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class 使用者歷史工作內容Repository
    {
        public APIResult fooAPIResult { get; set; } = new APIResult();
        public List<UserTasks> Items { get; set; } = new List<UserTasks>();

        public 使用者歷史工作內容Repository()
        {
        }

        /// <summary>
        /// 取得歷史工作
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<APIResult> GetDateRangeAsync(string account, DateTime startDate, DateTime lastDate)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        //http://xamarinhandsonlab.azurewebsites.net/api/UserTasks/Filter?account=user1&lastDate=2017/04/15&startDate=2017/04/15
                        string FooUrl = $"{PCLGlobal.UserTasksHistoryAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        #region  進行 RESTfull API 呼叫
                        // 使用 Get 方式來呼叫
                        var fooStartDate = startDate.ToString("yyyy/MM/dd");
                        var fooLastDate = lastDate.ToString("yyyy/MM/dd");
                        var fooFullUrl = $"{FooUrl}?account={account}&startDate={fooStartDate}&lastDate={fooLastDate}";
                        response = await client.GetAsync(fooFullUrl);
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
                                        Items = JsonConvert.DeserializeObject<List<UserTasks>>(fooAPIResult.Payload.ToString());
                                        if (Items == null)
                                        {
                                            Items = new List<UserTasks>();

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
                                        Items = new List<UserTasks>();
                                        fooAPIResult = new APIResult
                                        {
                                            Success = false,
                                            Message = fooAPIResult.Message,
                                            Payload = null,
                                        };

                                    }
                                    await Write();
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

        public async Task Write()
        {
            string data = JsonConvert.SerializeObject(Items);
            await StorageUtility.WriteToDataFileAsync("", PCLGlobal.資料主目錄, PCLGlobal.UserTasksHistoryAPIName, data);
        }

        /// <summary>
        /// 將資料讀取出來
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTasks>> Read()
        {
            string data = "";
            data = await StorageUtility.ReadFromDataFileAsync("", PCLGlobal.資料主目錄, PCLGlobal.UserTasksHistoryAPIName);
            Items = JsonConvert.DeserializeObject<List<UserTasks>>(data);
            if (Items == null)
            {
                Items = new List<UserTasks>();
            }
            return Items;
        }

    }
}
