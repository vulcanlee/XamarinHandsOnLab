using System;
using System.Globalization;
using Xamarin.Forms;

namespace XFTask.Converters
{
    /// <summary>
    /// 工作紀錄狀態可見度數值轉換器，當工作紀錄狀態較低的時候，無法看到工作紀錄狀態較高的欄位資料
    /// </summary>
    class TaskStatusToVisibleConverter : IValueConverter
    {
        /// <summary>
        /// 要比對的狀態
        /// </summary>
        public XFTask.Models.TaskStatus WatchStatus { get; set; } = Models.TaskStatus.REPORTED;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool fooResult = true;
            XFTask.Models.TaskStatus CurrentStatus = Models.TaskStatus.NOT_START;

            if (value is XFTask.Models.TaskStatus)
            {
                CurrentStatus = (XFTask.Models.TaskStatus)value;
            }
            int fooCurrent = (int)CurrentStatus;
            int fooWatch = (int)WatchStatus;

            if (fooCurrent >= fooWatch)
            {
                fooResult = true;
            }
            else
            {
                fooResult = false;
            }
            return fooResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
