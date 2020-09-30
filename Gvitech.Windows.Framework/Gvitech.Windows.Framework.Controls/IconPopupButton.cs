using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmc.Windows.Framework.Controls
{
	public class IconPopupButton : IconToggleButton
	{
		public static readonly DependencyProperty PlacementProperty;

		public PlacementMode Placement
		{
			get
			{
				return (PlacementMode)base.GetValue(IconPopupButton.PlacementProperty);
			}
			set
			{
				base.SetValue(IconPopupButton.PlacementProperty, value);
			}
		}

		static IconPopupButton()
		{
			IconPopupButton.PlacementProperty = DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(IconPopupButton), new PropertyMetadata(PlacementMode.Right));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconPopupButton), new FrameworkPropertyMetadata(typeof(IconPopupButton)));
		}
	}
}
