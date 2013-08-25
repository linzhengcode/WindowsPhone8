using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FunctionClock.Converters
{
    public class DayOfWeekToOpacityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num = System.Convert.ToInt32(parameter);
            int valueInt = System.Convert.ToInt32(value);
            if (num == valueInt)
            {
                return 1;
            }
            else
            {
                return 0.2;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
