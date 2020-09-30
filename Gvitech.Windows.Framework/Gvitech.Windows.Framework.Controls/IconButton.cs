using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Windows.Framework.Controls
{
	public class IconButton : Button
	{
		public static readonly DependencyProperty IconProperty;

		public static readonly DependencyProperty MouseOverIconProperty;

        public static readonly DependencyProperty PressedOverIconProperty;

        public string Icon
		{
			get
			{
				return (string)base.GetValue(IconButton.IconProperty);
			}
			set
			{
				base.SetValue(IconButton.IconProperty, value);
			}
		}

		public string MouseOverIcon
		{
			get
			{
				return (string)base.GetValue(IconButton.MouseOverIconProperty);
			}
			set
			{
				base.SetValue(IconButton.MouseOverIconProperty, value);
			}
		}

        public string PressedOverIcon
        {
            get
            {
                return (string)base.GetValue(IconButton.PressedOverIconProperty);
            }
            set
            {
                base.SetValue(IconButton.PressedOverIconProperty, value);
            }
        }

        static IconButton()
		{
			IconButton.IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconButton), new PropertyMetadata());
			IconButton.MouseOverIconProperty = DependencyProperty.Register("MouseOverIcon", typeof(string), typeof(IconButton), new PropertyMetadata());
            IconButton.PressedOverIconProperty = DependencyProperty.Register("PressedOverIcon", typeof(string), typeof(IconButton), new PropertyMetadata());
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
		}
	}
}
