using System;
using System.Globalization;
using System.Windows.Data;

namespace TitleBar.Models
{
    public class DriveVolumeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (long)value / 1024 / 1024 / 1024;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
