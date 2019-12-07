using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PathFindingVisualization.WPF.Converters
{
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value
              ? new SolidColorBrush(Colors.DarkBlue)
              : new SolidColorBrush(Colors.Transparent);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("This converter was not made for this operation.");
        }
    }
}
