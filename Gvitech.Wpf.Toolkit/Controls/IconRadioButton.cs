using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mmc.Wpf.Toolkit.Controls
{
	// Token: 0x02000018 RID: 24
	public class IconRadioButton : RadioButton
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002BB4 File Offset: 0x00000DB4
		static IconRadioButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconRadioButton), new FrameworkPropertyMetadata(typeof(IconRadioButton)));
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002C88 File Offset: 0x00000E88
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002CAA File Offset: 0x00000EAA
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002CBC File Offset: 0x00000EBC
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002CDE File Offset: 0x00000EDE
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



        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000054 RID: 84 RVA: 0x00002CF0 File Offset: 0x00000EF0
        // (set) Token: 0x06000055 RID: 85 RVA: 0x00002D12 File Offset: 0x00000F12
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D24 File Offset: 0x00000F24
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002D46 File Offset: 0x00000F46
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

		// Token: 0x06000058 RID: 88 RVA: 0x00002D58 File Offset: 0x00000F58
		protected override void OnClick()
		{
			base.IsChecked = !base.IsChecked;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D98 File Offset: 0x00000F98
		protected override void OnChecked(RoutedEventArgs e)
		{
			base.OnChecked(e);
			bool flag = this.NCommand != null;
			if (flag)
			{
				this.NCommand.Execute(base.IsChecked);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DD4 File Offset: 0x00000FD4
		protected override void OnUnchecked(RoutedEventArgs e)
		{
			base.OnUnchecked(e);
			bool flag = this.NCommand != null;
			if (flag)
			{
				this.NCommand.Execute(base.IsChecked);
			}
		}

		// Token: 0x04000017 RID: 23
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());

		// Token: 0x04000018 RID: 24
		public static readonly DependencyProperty MouseOverIconProperty = DependencyProperty.Register("MouseOverIcon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());

		// Token: 0x04000019 RID: 25
		public static readonly DependencyProperty CheckedIconProperty = DependencyProperty.Register("CheckedIcon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());

		// Token: 0x0400001A RID: 26
		public static readonly DependencyProperty NCommandProperty = DependencyProperty.Register("NCommand", typeof(ICommand), typeof(IconRadioButton), new PropertyMetadata());
        // Using a DependencyProperty as the backing store for PressedOverIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedOverIconProperty = DependencyProperty.Register("PressedOverIcon", typeof(string), typeof(IconRadioButton), new PropertyMetadata());
    }
}
