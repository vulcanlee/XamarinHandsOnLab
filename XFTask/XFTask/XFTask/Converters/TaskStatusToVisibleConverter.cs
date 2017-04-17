using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFTask.Converters
{
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
