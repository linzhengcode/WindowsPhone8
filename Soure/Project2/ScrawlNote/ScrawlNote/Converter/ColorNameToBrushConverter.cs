using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ScrawlNote.Converter
{
    public class ColorNameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value.ToString();
            Color white = Colors.White;
            if (s == "yellow")
            {
                white = Colors.Yellow;
            }
            else if (s == ("green"))
            {
                white = Colors.Green;
            }
            else if (s == ("blue"))
            {
                white = Colors.Blue;
            }
            else if (s == ("orange"))
            {
                white = Colors.Orange;
            }

            return new SolidColorBrush(white);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
