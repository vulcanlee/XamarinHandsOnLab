using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XFTask.ViewModels
{

    public class DrawerMenuVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Repositories (遠端或本地資料存取)

        #endregion

        #region ViewModel Property (用於在 View 中作為綁定之用)

        #region 基本型別與類別的 Property
        /// <summary>
        /// Font Awesome 的圖示名稱
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// 功能表項目要顯示的文字
        /// </summary>
        public string MenuName { get; set; }
        #endregion

        #region 集合類別的 Property

        #endregion

        #endregion

        #region Field 欄位
        /// <summary>
        /// 按下這個功能表項目之後，要執行的命令
        /// </summary>
        public DelegateCommand DrawMenuCommand { get; set; }

        #endregion

        #region Constructor 建構式
        public DrawerMenuVM()
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
