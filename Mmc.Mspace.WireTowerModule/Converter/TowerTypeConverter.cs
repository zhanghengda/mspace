using System;
using System.Globalization;
using System.Windows.Data;
using Mmc.Mspace.Common;

namespace Mmc.Mspace.WireTowerModule.Converter
{
    public class TowerTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string type)) return null;

            if (type == CommonContract.TowerType.Straight.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Straight}Tower");
            }
            else if (type == CommonContract.TowerType.Wine.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Wine}Tower");
            }
            else if (type == CommonContract.TowerType.Safe.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Safe}Tower");
            }
            //else if (type == CommonContract.TowerType.Other.ToString())
            //{
            //    type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Other}Tower");
            //}
            else
            {
                type = null;
            }
            return type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string type)) return null;

            if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Straight}Tower"))
            {
                type = CommonContract.TowerType.Straight.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Wine}Tower"))
            {
                type = CommonContract.TowerType.Wine.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Safe}Tower"))
            {
                type = CommonContract.TowerType.Safe.ToString();
            }
            //else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.TowerType.Other}Tower"))
            //{
            //    type = CommonContract.TowerType.Other.ToString();
            //}
            else
            {
                type = null;
            }

            return type;
        }
    }
}
