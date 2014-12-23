using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PDFW;
using PDFW.Classes;
using PDFW.SL;
using PDFW.SL.Classes;
using System.Windows;

namespace PianoTeacher
{
    public class NumberToVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value.NVLDecimal(0);
            var max = parameter.NVLDecimal(0);

            if (val <= max)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
