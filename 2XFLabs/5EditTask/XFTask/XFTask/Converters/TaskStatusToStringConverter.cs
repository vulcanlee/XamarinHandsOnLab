using System;
using System.Globalization;
using Xamarin.Forms;

namespace XFTask.Converters
{
    /// <summary>
    /// 工作紀錄狀態列舉的數值轉換器，轉換成為中文說明文字
    /// </summary>
    class TaskStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fooResult = "";

            //var fooStatusEnum = Enum.Parse(typeof(XFTask.Models.TaskStatus), value.ToString());
            var fooStatusEnum = (XFTask.Models.TaskStatus)value;
            switch (fooStatusEnum)
            {
                case Models.TaskStatus.NOT_START:
                    fooResult = "尚未開始";
                    break;
                case Models.TaskStatus.CHECKIN:
                    fooResult = "已經打卡";
                    break;
                case Models.TaskStatus.INPUT:
                    fooResult = "資料輸入";
                    break;
                case Models.TaskStatus.UPLOAD_IMAGE:
                    fooResult = "圖片上傳";
                    break;
                case Models.TaskStatus.REPORTED:
                    fooResult = "工作回報";
                    break;
                default:
                    break;
            }
            return fooResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
