using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mmc.Windows.Framework.Controls
{
	public class IconDataButton : Button
	{
		public static readonly DependencyProperty IconGeoDataProperty;

		public static readonly DependencyProperty IconDataButtonTypeProperty;

		public StreamGeometry IconGeoData
		{
			get
			{
				return (StreamGeometry)base.GetValue(IconDataButton.IconGeoDataProperty);
			}
			set
			{
				base.SetValue(IconDataButton.IconGeoDataProperty, value);
			}
		}

		public IconDataButtonTypes IconDataButtonType
		{
			get
			{
				return (IconDataButtonTypes)base.GetValue(IconDataButton.IconDataButtonTypeProperty);
			}
			set
			{
				base.SetValue(IconDataButton.IconDataButtonTypeProperty, value);
			}
		}

		static IconDataButton()
		{
			IconDataButton.IconGeoDataProperty = DependencyProperty.Register("IconGeoData", typeof(StreamGeometry), typeof(IconDataButton), new PropertyMetadata());
			IconDataButton.IconDataButtonTypeProperty = DependencyProperty.Register("IconDataButtonType", typeof(IconDataButtonTypes), typeof(IconDataButton), new PropertyMetadata(IconDataButtonTypes.Circle));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconDataButton), new FrameworkPropertyMetadata(typeof(IconDataButton)));
		}
	}
}
