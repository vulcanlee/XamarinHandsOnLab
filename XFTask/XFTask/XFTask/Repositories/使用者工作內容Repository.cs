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
    /// <summary>
    /// 提供存取後端使用者工作紀錄服務
    /// </summary>
    public class 使用者工作內容Repository
    {
        /// <summary>
        /// 呼叫 API 回傳後，回報的結果內容
        /// </summary>
        public APIResult fooAPIResult { get; set; } = new APIResult();
        /// <summary>
        /// 使用者工作紀錄
        /// </summary>
        public List<UserTasks> Items { get; set; } = new List<UserTasks>();

        public 使用者工作內容Repository()
        {
        }

        /// <summary>
        /// 取得尚未完成的工作
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<APIResult> GetDateRangeAsync(string account)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        // http://xamarinhandsonlab.azurewebsites.net/api/UserTasks?account=user1
                        string FooUrl = $"{PCLGlobal.UserTasksAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooUrl}?account={account}";
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

        /// <summary>
        /// 更新使用者
        /// </summary>
        /// <param name="userTasks"></param>
        /// <returns></returns>
        public async Task<APIResult> PutAsync(UserTasks userTasks)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        // http://xamarinhandsonlab.azurewebsites.net/api/UserTasks
                        string FooUrl = $"{PCLGlobal.UserTasksAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                        #region  設定相關網址內容
                        // 設定相關網址內容
                        var fooFullUrl = $"{FooUrl}";
                        client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                        var foouserTasksJSON = JsonConvert.SerializeObject(userTasks);
                        #endregion

                        // 使用Put方法，並且傳遞 Json 字串內容到遠端 Web API
                        response = await client.PutAsync(fooFullUrl, new StringContent(foouserTasksJSON, Encoding.UTF8, "application/json"));
                        #endregion

                        #region Response
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
        /// 上傳圖片到遠端 Web API 主機上
        /// </summary>
        /// <param name="file">使用 Media套件取得的圖片 MediaFile 物件 </param>
        /// <returns></returns>
        public async Task<APIResult> UploadImageAsync(MediaFile file)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        #region 呼叫遠端 Web API
                        // http://xamarinhandsonlab.azurewebsites.net/api/UploadImage
                        string FooUrl = $"{PCLGlobal.UploadImageAPIUrl}";
                        HttpResponseMessage response = null;

                        client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                        #region  設定相關網址內容
                        var fooFullUrl = $"{FooUrl}";
                        client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");

                        #region 將剛剛拍照的檔案，上傳到網路伺服器上(使用 Multipart 的規範)
                        // 規格說明請參考 https://www.w3.org/Protocols/rfc1341/7_2_Multipart.html
                        using (var content = new MultipartFormDataContent())
                        {
                            // 取得這個圖片檔案的完整路徑
                            var path = file.Path;
                            // 取得這個檔案的最終檔案名稱
                            var filename = Path.GetFileName(path);

                            // 開啟這個圖片檔案，並且讀取其內容
                            using (var fs = file.GetStream())
                            {
                                var fooSt = Path.GetFileName(path);
                                var streamContent = new StreamContent(fs);
                                streamContent.Headers.Add("Content-Type", "application/octet-stream");
                                streamContent.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + fooSt + "\"");
                                content.Add(streamContent, "file", filename);

                                // 上傳到遠端伺服器上
                                response = await client.PostAsync(fooFullUrl, content);
                            }
                        }
                        #endregion
                        #endregion
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
                                    fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
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
            string data = JsonConvert.SerializeObject(Items);
            await StorageUtility.WriteToDataFileAsync("", PCLGlobal.資料主目錄, PCLGlobal.UserTasksAPIName, data);
        }

        /// <summary>
        /// 將資料讀取出來
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTasks>> Read()
        {
            string data = "";
            data = await StorageUtility.ReadFromDataFileAsync("", PCLGlobal.資料主目錄, PCLGlobal.UserTasksAPIName);
            Items = JsonConvert.DeserializeObject<List<UserTasks>>(data);
            if (Items == null)
            {
                Items = new List<UserTasks>();
            }
            return Items;
        }

    }
}
