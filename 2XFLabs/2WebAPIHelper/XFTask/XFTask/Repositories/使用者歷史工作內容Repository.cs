using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using XFTask.Helpers;
using XFTask.Models;

namespace XFTask.Repositories
{
    /// <summary>
    /// 提供存取後端使用者工作歷史紀錄服務
    /// </summary>
    public class 使用者歷史工作內容Repository
    {
        /// <summary>
        /// 呼叫 API 回傳後，回報的結果內容
        /// </summary>
        public APIResult fooAPIResult { get; set; } = new APIResult();
        /// <summary>
        /// 使用者工作歷史紀錄
        /// </summary>
        public List<UserTasks> Items { get; set; } = new List<UserTasks>();

        public 使用者歷史工作內容Repository()
        {
        }

        /// <summary>
        /// 取得歷史工作紀錄
        /// </summary>
        /// <param name="account">使用者帳號</param>
        /// <param name="startDate">開始時間</param>
        /// <param name="lastDate">結束時間</param>
        /// <returns></returns>
        public async Task<APIResult> GetDateRangeAsync(string account, DateTime startDate, DateTime lastDate)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        //http://xamarinhandsonlab.azurewebsites.net/api/UserTasks/Filter?account=user1&lastDate=2017/04/15&startDate=2017/04/15
                        string FooUrl = $"{PCLGlobalHelper.UserTasksHistoryAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                        #region  設定相關網址內容
                        var fooStartDate = startDate.ToString("yyyy/MM/dd");
                        var fooLastDate = lastDate.ToString("yyyy/MM/dd");
                        var fooFullUrl = $"{FooUrl}?account={account}&startDate={fooStartDate}&lastDate={fooLastDate}";
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
                                            // fooAPIResult 直接使用 API 回傳的內容
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region API 的狀態碼為 不成功
                                        Items = new List<UserTasks>();
                                        fooAPIResult = new APIResult
                                        {
                                            Success = false,
                                            Message = fooAPIResult.Message,
                                            Payload = null,
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

        public async Task Write()
        {
            string data = JsonConvert.SerializeObject(Items);
            await StorageUtility.WriteToDataFileAsync("", PCLGlobalHelper.資料主目錄, PCLGlobalHelper.UserTasksHistoryAPIName, data);
        }

        /// <summary>
        /// 將資料讀取出來
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTasks>> Read()
        {
            string data = "";
            data = await StorageUtility.ReadFromDataFileAsync("", PCLGlobalHelper.資料主目錄, PCLGlobalHelper.UserTasksHistoryAPIName);
            Items = JsonConvert.DeserializeObject<List<UserTasks>>(data);
            if (Items == null)
            {
                Items = new List<UserTasks>();
            }
            return Items;
        }

    }
}
