using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmc.Windows.Framework.Controls
{
	public class IconToggleButton : ToggleButton
	{
		public static readonly DependencyProperty IconProperty;

		public static readonly DependencyProperty CheckedIconProperty;

		public static readonly DependencyProperty MouseOverIconProperty;
        public static readonly DependencyProperty PressedOverIconProperty;

        public string Icon
		{
			get
			{
				return (string)base.GetValue(IconToggleButton.IconProperty);
			}
			set
			{
				base.SetValue(IconToggleButton.IconProperty, value);
			}
		}

		public string CheckedIcon
		{
			get
			{
				return (string)base.GetValue(IconToggleButton.CheckedIconProperty);
			}
			set
			{
				base.SetValue(IconToggleButton.CheckedIconProperty, value);
			}
		}

		public string MouseOverIcon
		{
			get
			{
				return (string)base.GetValue(IconToggleButton.MouseOverIconProperty);
			}
			set
			{
				base.SetValue(IconToggleButton.MouseOverIconProperty, value);
			}
		}

        public string PressedOverIcon
        {
            get
            {
                return (string)base.GetValue(IconToggleButton.PressedOverIconProperty);
            }
            set
            {
                base.SetValue(IconToggleButton.PressedOverIconProperty, value);
            }
        }

        static IconToggleButton()
		{
			IconToggleButton.IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());
			IconToggleButton.CheckedIconProperty = DependencyProperty.Register("CheckedIcon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());
            IconToggleButton.MouseOverIconProperty = DependencyProperty.Register("MouseOverIcon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());
            IconToggleButton.PressedOverIconProperty = DependencyProperty.Register("PressedOverIcon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggleButton), new FrameworkPropertyMetadata(typeof(IconToggleButton)));
		}
	}
}
