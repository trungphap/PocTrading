using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfBasic
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolToFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "" : "#eee";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
