using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RD15Controls.Converters
{
    public class ObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] parray = parameter.ToString().ToLower().Split(':');
            string valueStr = value == null ? string.Empty : value.ToString().ToLower();
            string returnValue = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(valueStr))
                {
                    returnValue = parray[0].Contains("#null") ? parray[1] : parray[2];
                }
                else if (parray[0].Contains("|"))
                {
                    returnValue = parray[0].Split('|').Contains(valueStr) ? parray[1] : parray[2];
                }
                else
                {
                    returnValue = parray[0].Equals(valueStr) ? parray[1] : parray[2];
                }
                if (returnValue.Equals("#source", StringComparison.CurrentCultureIgnoreCase))
                    return value;
                return returnValue;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ObjectConverter {ex.Message} {parameter}");
                Console.ResetColor();
            }
            return parray[2];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnValue = "otherValue";
            string[] parray = parameter.ToString().ToLower().Split(':');
            if (value == null)
                return returnValue;
            var valueStr = value.ToString().ToLower();
            if (valueStr != parray[1])
                return returnValue;
            else
                return parray[0].Contains('|') ? parray[0].Split('|')[0] : parray[0];
        }
        
    }
    public class BoolToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
