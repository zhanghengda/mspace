using System;
using System.Globalization;
using System.Windows.Data;
using Mmc.Mspace.Common;

namespace Mmc.Mspace.WireTowerModule.Converter
{
    public  class SignTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string type)) return null;

            if (type == CommonContract.SignType.TopCenter.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.TopCenter}Sign");
            }
            else if (type == CommonContract.SignType.TopLeft.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.TopLeft}Sign");
            }
            else if (type == CommonContract.SignType.TopRight.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.TopRight}Sign");
            }
            else if (type == CommonContract.SignType.Left.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Left}Sign");
            }
            //else if (type == CommonContract.SignType.LeftUp.ToString())
            //{
            //    type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.LeftUp}Sign");
            //}
            //else if (type == CommonContract.SignType.LeftDown.ToString())
            //{
            //    type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.LeftDown}Sign");
            //}
            else if (type == CommonContract.SignType.Right.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Right}Sign");
            }
            //else if (type == CommonContract.SignType.RightUp.ToString())
            //{
            //    type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.RightUp}Sign");
            //}
            //else if (type == CommonContract.SignType.RightDown.ToString())
            //{
            //    type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.RightDown}Sign");
            //}
            else if (type == CommonContract.SignType.Inner.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Inner}Sign");
            }
            else if (type == CommonContract.SignType.Aided.ToString())
            {
                type = Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Aided}Sign");
            }
            else
            {
                type = null;
            }
            return type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string type)) return null;

            if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.TopCenter}Sign"))
            {
                type = CommonContract.SignType.TopCenter.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.TopLeft}Sign"))
            {
                type =  CommonContract.SignType.TopLeft.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.TopRight}Sign"))
            {
                type = CommonContract.SignType.TopRight.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Left}Sign"))
            {
                type = CommonContract.SignType.Left.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Right}Sign"))
            {
                type = CommonContract.SignType.Right.ToString();
            }
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Inner}Sign"))
            {
                type = CommonContract.SignType.Inner.ToString();
            }

            //else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.LeftUp}Sign"))
            //{
            //    type = CommonContract.SignType.LeftUp.ToString();
            //}
            //else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.LeftDown}Sign"))
            //{
            //    type = CommonContract.SignType.LeftDown.ToString();
            //}

            //else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.RightUp}Sign"))
            //{
            //    type = CommonContract.SignType.RightUp.ToString();
            //}
            //else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.RightDown}Sign"))
            //{
            //    type = CommonContract.SignType.RightDown.ToString();
            //}
            else if (type == Helpers.ResourceHelper.FindKey($"WT{CommonContract.SignType.Aided}Sign"))
            {
                type = CommonContract.SignType.Aided.ToString();
            }
            else
            {
                type = null;
            }

            return type;
        }
    }
}
