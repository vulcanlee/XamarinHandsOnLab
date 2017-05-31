using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFTask.ViewModels
{

    public class ProcessingMaskVM : BindableBase
    {
        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property

        #region IsRunning
        private bool _IsRunning;
        /// <summary>
        /// IsRunning
        /// </summary>
        public bool IsRunning
        {
            get { return this._IsRunning; }
            set { this.SetProperty(ref this._IsRunning, value); }
        }
        #endregion

        #region ProcessingTitle
        private string _ProcessingTitle;
        /// <summary>
        /// PropertyDescription
        /// </summary>
        public string ProcessingTitle
        {
            get { return this._ProcessingTitle; }
            set { this.SetProperty(ref this._ProcessingTitle, value); }
        }
        #endregion

        #region ProcessingContent
        private string _ProcessingContent;
        /// <summary>
        /// ProcessingContent
        /// </summary>
        public string ProcessingContent
        {
            get { return this._ProcessingContent; }
            set { this.SetProperty(ref this._ProcessingContent, value); }
        }
        #endregion

        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位

        #endregion

        #region Constructor 建構式
        public ProcessingMaskVM()
        {
            IsRunning = false;
            ProcessingTitle = "";
            ProcessingContent = "";

            #region 相依性服務注入的物件

            #endregion

            #region 頁面中綁定的命令

            #endregion

            #region 事件聚合器訂閱

            #endregion
        }

        #endregion

        #region 設計時期或者執行時期的ViewModel初始化
        #endregion

        #region 相關事件
        #endregion

        #region 相關的Command定義
        #endregion

        #region 其他方法

        #endregion

    }
}
