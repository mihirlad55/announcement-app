using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Announcement_App_2
{
    class imageUtils
    {
        public static void setBrushPath(string path, ref Rectangle rectFill)
        {
            var imageBrush = rectFill.Fill as ImageBrush;
            imageBrush.ImageSource = new BitmapImage(new Uri(path));
            rectFill.Fill = imageBrush;
        }
        public static void setBrushPath(string path, ref Ellipse ellipseFill)
        {
            var imageBrush = ellipseFill.Fill as ImageBrush;
            imageBrush.ImageSource = new BitmapImage(new Uri(path));
            ellipseFill.Fill = imageBrush;
        }

        public static void checkImageResourceIntegrity()
        {
            if (true)
            {
                Announcement_App_2.App.GetResourceStream(new Uri(""));
                
            }
        }
    }
}
