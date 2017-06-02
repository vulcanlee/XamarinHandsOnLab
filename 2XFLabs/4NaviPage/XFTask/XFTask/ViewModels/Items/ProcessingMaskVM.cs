﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XFTask.ViewModels
{

    public class ProcessingMaskVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property
        /// <summary>
        /// 是否要顯示這個處理中遮罩
        /// </summary>
        public bool IsRunning { get; set; } = false;
        /// <summary>
        /// 處理中遮罩的主題名稱
        /// </summary>
        public string ProcessingTitle { get; set; } = "";
        /// <summary>
        /// 處理中遮罩的動作說明
        /// </summary>
        public string ProcessingContent { get; set; } = "";
        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位

        #endregion

        #region Constructor 建構式
        public ProcessingMaskVM()
        {

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
