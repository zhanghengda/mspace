using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Mmc.Mspace.Theme.Converter
{
    public class InspectionParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Count() == 0)
                return null;
            string dataType = "";
            bool isChecked = false;
            string tag = "";
            for (int i = 0; i < values.Count(); i++)
            {
                if (i == 0) dataType = values[i].ToString();
                if (i == 1) isChecked = (bool)values[i];
                if (i == 2) tag = values[i].ToString();
            }
            if(tag== "PART_Import")
            {
                if (dataType == "Dom")
                {
                    return !isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Picture")
                {
                    return !isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Route")
                {
                    return !isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Video")
                {
                    return !isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Report")
                {
                    return !isChecked ? Visibility.Visible : Visibility.Collapsed;
                }

            }
            if (tag == "PART_Check")
            {
                if (dataType == "Dom" && isChecked)
                {
                    return isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Picture")
                {
                    return isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Route")
                {
                    return isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (dataType == "Video")
                {
                    return isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            if (tag == "PART_Download")
            {
                if (dataType == "Report")
                {
                    return isChecked ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
