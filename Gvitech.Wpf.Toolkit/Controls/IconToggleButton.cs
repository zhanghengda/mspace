using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmc.Wpf.Toolkit.Controls
{
	// Token: 0x02000019 RID: 25
	public class IconToggleButton : ToggleButton
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002E18 File Offset: 0x00001018
		static IconToggleButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggleButton), new FrameworkPropertyMetadata(typeof(IconToggleButton)));
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002EC4 File Offset: 0x000010C4
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002EE6 File Offset: 0x000010E6
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002EF8 File Offset: 0x000010F8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002F1A File Offset: 0x0000111A
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F2C File Offset: 0x0000112C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002F4E File Offset: 0x0000114E
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

        // Token: 0x0400001B RID: 27
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());

		// Token: 0x0400001C RID: 28
		public static readonly DependencyProperty CheckedIconProperty = DependencyProperty.Register("CheckedIcon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());

		// Token: 0x0400001D RID: 29
		public static readonly DependencyProperty MouseOverIconProperty = DependencyProperty.Register("MouseOverIcon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());

        public static readonly DependencyProperty PressedOverIconProperty = DependencyProperty.Register("PressedOverIcon", typeof(string), typeof(IconToggleButton), new PropertyMetadata());
    }
}
