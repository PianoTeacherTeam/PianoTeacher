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
    public class PlayingNotaToMarginValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var nota = value as PlayNotaModel;

            var margin = new Thickness();

            var index = nota.Code.GetNotaIndex() - 1;
            var marginTop = index * 12;
            margin.Bottom = marginTop;

            return margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
