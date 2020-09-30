using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Markup;
using System.Windows.Media;

namespace Mmc.Wpf.Toolkit.Controls.InkPad
{
	/// <summary>
	///     StylusSettings.xaml 的交互逻辑
	/// </summary>
	/// <summary>
	/// StylusSettings
	/// </summary>
	// Token: 0x02000020 RID: 32
	public partial class StylusSettings : Window
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00004B50 File Offset: 0x00002D50
		public StylusSettings()
		{
			this.InitializeComponent();
			this.createGridOfColor();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004B9C File Offset: 0x00002D9C
		public DrawingAttributes DrawingAttributes
		{
			get
			{
				return new DrawingAttributes
				{
					IgnorePressure = this.chkPressure.IsChecked.Value,
					Width = this.penWidth,
					Height = this.penHeight,
					IsHighlighter = this.chkHighlight.IsChecked.Value,
					Color = this.currColor
				};
			}
			set
			{
				this.chkPressure.IsChecked = new bool?(value.IgnorePressure);
				this.chkHighlight.IsChecked = new bool?(value.IsHighlighter);
				this.penWidth = value.Width;
				this.penHeight = value.Height;
				this.currColor = value.Color;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004C70 File Offset: 0x00002E70
		private void createGridOfColor()
		{
			PropertyInfo[] properties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
			List<string> list = new List<string>();
			foreach (PropertyInfo propertyInfo in properties)
			{
				list.Add(propertyInfo.GetValue(null, null).ToString());
			}
			list.Sort();
			list.RemoveAt(0);
			list.Reverse();
			foreach (string value in list)
			{
				Button button = new Button();
				button.Background = new SolidColorBrush
				{
					Color = (Color)ColorConverter.ConvertFromString(value)
				};
				button.Click += this.b_Click;
				this.ugColors.Children.Add(button);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004D6C File Offset: 0x00002F6C
		private void b_Click(object sender, RoutedEventArgs e)
		{
			SolidColorBrush x = (SolidColorBrush)(sender as Button).Background;
			this.currColor = x.Color;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004D97 File Offset: 0x00002F97
		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
		}

		// Token: 0x04000048 RID: 72
		private Color currColor = Colors.Black;

		// Token: 0x04000049 RID: 73
		private double penHeight = 2.0;

		// Token: 0x0400004A RID: 74
		private double penWidth = 2.0;
	}
}
