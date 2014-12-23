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
using System.Windows.Media;

namespace PianoTeacher
{
    public class PlayStateToButtonForegroundValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (PlayStateEnum)value;

            var color = Colors.Black;
            if (state == PlayStateEnum.Playing)
                color = Colors.Red;
            else if (state == PlayStateEnum.Played)
                color = Colors.Green;

            var brush = new SolidColorBrush(color);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
