using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Windows;


namespace CubeExercise
{
    class ImageConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = null;
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.IndexOf("devenv") >= 0)
            {
                path = @"E:\Projects\CubeExercise\source\main\CubeExercise\image_OLL_001.bmp";
            }
            else
            {
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                path = System.IO.Path.Combine(path, value as string);
            }
            
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            image.EndInit();

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
