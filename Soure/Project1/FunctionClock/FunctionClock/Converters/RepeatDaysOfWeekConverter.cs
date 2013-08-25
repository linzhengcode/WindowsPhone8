using FunctionClock.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FunctionClock.Converters
{
    public class RepeatDaysOfWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder stringBuilder = new StringBuilder();
            RepeatDaysOfWeek repeatDaysOfWeek = (RepeatDaysOfWeek)value;
            if (repeatDaysOfWeek.Sunday)
            {
                stringBuilder.Append(string.Format("{0}, ","周日"));
            }
            if (repeatDaysOfWeek.Monday)
            {
                stringBuilder.Append(string.Format("{0}, ", "周一"));
            }
            if (repeatDaysOfWeek.Tuesday)
            {
                stringBuilder.Append(string.Format("{0}, ", "周二"));
            }
            if (repeatDaysOfWeek.Wednesday)
            {
                stringBuilder.Append(string.Format("{0}, ", "周三"));
            }
            if (repeatDaysOfWeek.Thursday)
            {
                stringBuilder.Append(string.Format("{0}, ", "周四"));
            }
            if (repeatDaysOfWeek.Friday)
            {
                stringBuilder.Append(string.Format("{0}, ", "周五"));
            }
            if (repeatDaysOfWeek.Saturday)
            {
                stringBuilder.Append(string.Format("{0}, ", "周六"));
            }
            if (stringBuilder.Length == 0)
            {
                stringBuilder.Append("只响一次");
            }
            else
            {
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }
            return stringBuilder.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
