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
using System.Windows.Media.Imaging;

namespace PianoTeacher
{
    public class PlayingNotaToNotaIconValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var nota = value as PlayNotaModel;

            string imgPath = "quarter.png";
            if (nota.Duration > 1)
                imgPath = "full.png";

            //imgPath = nota.Code + "." + nota.Duration.ToString().Replace(",", ".") + ".png";
            imgPath = "/PianoTeacher;Component/images/nota/" + imgPath;

            var img = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
