using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Controls
{
	public class IconRadioButton : RadioButton
	{
		public static readonly DependencyProperty IconProperty;

		public static readonly DependencyProperty MouseOverIconProperty;

		public static readonly DependencyProperty CheckedIconProperty;

		public static readonly DependencyProperty NCommandProperty;

        public static readonly DependencyProperty PressedOverIconProperty;

        public string Icon
		{
			get
			{
				return (string)base.GetValue(IconRadioButton.IconProperty);
			}
			set
			{
				base.SetValue(IconRadioButton.IconProperty, value);
			}
		}

		public string MouseOverIcon
		{
			get
			{
				return (string)base.GetValue(IconRadioButton.MouseOverIconProperty);
			}
			set
			{
				base.SetValue(IconRadioButton.MouseOverIconProperty, value);
			}
		}

        public string PressedOverIcon
        {
            get
            {
                return (string)base.GetValue(IconRadioButton.PressedOverIconProperty);
            }
            set
            {
                base.SetValue(IconRadioButton.PressedOverIconProperty, value);
            }
        }

        public string CheckedIcon
		{
			get
			{
				return (string)base.GetValue(IconRadioButton.CheckedIconProperty);
			}
			set
			{
				base.SetValue(IconRadioButton.CheckedIconProperty, value);
			}
		}

		public ICommand NCommand
		{
			get
			{
				return (ICommand)base.GetValue(IconRadioButton.NCommandProperty);
			}
			set
			{
				base.SetValue(IconRadioButton.NCommandProperty, value);
			}
		}

		static IconRadioButton()
		{
			IconRadioButton.IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());
			IconRadioButton.MouseOverIconProperty = DependencyProperty.Register("MouseOverIcon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());
			IconRadioButton.CheckedIconProperty = DependencyProperty.Register("CheckedIcon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());
            IconRadioButton.PressedOverIconProperty = DependencyProperty.Register("PressedOverIcon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());
        IconRadioButton.NCommandProperty = DependencyProperty.Register("NCommand", typeof(ICommand), typeof(IconRadioButton), new PropertyMetadata());
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconRadioButton), new FrameworkPropertyMetadata(typeof(IconRadioButton)));
		}

		protected override void OnClick()
		{
			base.IsChecked = !base.IsChecked;
		}

		protected override void OnChecked(RoutedEventArgs e)
		{
			base.OnChecked(e);
			if (this.NCommand != null)
			{
				this.NCommand.Execute(base.IsChecked);
			}
		}

		protected override void OnUnchecked(RoutedEventArgs e)
		{
			base.OnUnchecked(e);
			if (this.NCommand != null)
			{
				this.NCommand.Execute(base.IsChecked);
			}
		}
	}
}
