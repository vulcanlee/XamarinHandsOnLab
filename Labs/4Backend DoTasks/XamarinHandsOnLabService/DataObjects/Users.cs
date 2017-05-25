using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XamarinHandsOnLabService.DataObjects
{
    /// <summary>
    /// 使用者資訊
    /// </summary>
    public class Users
    {
        /// <summary>
        /// 使用者編號
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 使用者大頭貼網址
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 主管代號
        /// </summary>
        public long ManagerId { get; set; }
    }
}