using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CubeExercise
{
    public class Int32Converter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int targetValue = 0;
            if (Int32.TryParse(value.ToString().Trim(), out targetValue))
            {
                return targetValue;
            }
            else
            {
                throw new FormatException("Value format is illegal");
            }
        }

        #endregion
    }
}
