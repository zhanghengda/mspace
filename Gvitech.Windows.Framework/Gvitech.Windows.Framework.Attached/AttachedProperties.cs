using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mmc.Windows.Framework.Attached
{
	public class AttachedProperties
	{
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(AttachedProperties), new PropertyMetadata(Orientation.Vertical));

		public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(AttachedProperties), new PropertyMetadata());

		public static Orientation GetOrientation(DependencyObject obj)
		{
			return (Orientation)obj.GetValue(AttachedProperties.OrientationProperty);
		}

		public static void SetOrientation(DependencyObject obj, Orientation value)
		{
			obj.SetValue(AttachedProperties.OrientationProperty, value);
		}

		public static Brush GetBackground(DependencyObject obj)
		{
			return (Brush)obj.GetValue(AttachedProperties.BackgroundProperty);
		}

		public static void SetBackground(DependencyObject obj, Brush value)
		{
			obj.SetValue(AttachedProperties.BackgroundProperty, value);
		}
	}
}
