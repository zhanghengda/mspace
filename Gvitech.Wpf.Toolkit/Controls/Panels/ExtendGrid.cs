using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Wpf.Toolkit.Controls.Panels
{
	// Token: 0x0200001C RID: 28
	public class ExtendGrid : Grid
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003754 File Offset: 0x00001954
		static ExtendGrid()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendGrid), new FrameworkPropertyMetadata(typeof(ExtendGrid)));
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000037F0 File Offset: 0x000019F0
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003812 File Offset: 0x00001A12
		public int ColumnCount
		{
			get
			{
				return (int)base.GetValue(ExtendGrid.ColumnCountProperty);
			}
			set
			{
				base.SetValue(Grid.ColumnProperty, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003828 File Offset: 0x00001A28
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003812 File Offset: 0x00001A12
		public int RowCount
		{
			get
			{
				return (int)base.GetValue(ExtendGrid.RowCountProperty);
			}
			set
			{
				base.SetValue(Grid.ColumnProperty, value);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000384C File Offset: 0x00001A4C
		private static void ColumnCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool flag = d == null;
			if (!flag)
			{
				ExtendGrid extendGrid = d as ExtendGrid;
				int num = 0;
				int.TryParse(e.NewValue.ToString(), out num);
				int count = extendGrid.ColumnDefinitions.Count;
				int num2 = num - count;
				int num3;
				for (int i = num2 - 1; i >= 0; i = num3 - 1)
				{
					extendGrid.ColumnDefinitions.Add(new ColumnDefinition());
					num3 = i;
				}
				for (int j = 0; j > num2; j = num3 - 1)
				{
					extendGrid.ColumnDefinitions.RemoveAt(count - j - 1);
					num3 = j;
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000038F8 File Offset: 0x00001AF8
		private static void RowCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool flag = d == null;
			if (!flag)
			{
				ExtendGrid extendGrid = d as ExtendGrid;
				int num = 0;
				int.TryParse(e.NewValue.ToString(), out num);
				int count = extendGrid.RowDefinitions.Count;
				int num2 = num - count;
				int num3;
				for (int i = num2 - 1; i >= 0; i = num3 - 1)
				{
					extendGrid.RowDefinitions.Add(new RowDefinition());
					num3 = i;
				}
				for (int j = 0; j > num2; j = num3 - 1)
				{
					extendGrid.RowDefinitions.RemoveAt(count - j - 1);
					num3 = j;
				}
			}
		}

		// Token: 0x04000026 RID: 38
		public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(ExtendGrid), new PropertyMetadata(new PropertyChangedCallback(ExtendGrid.ColumnCountPropertyChangedCallback)));

		// Token: 0x04000027 RID: 39
		public static readonly DependencyProperty RowCountProperty = DependencyProperty.Register("RowCount", typeof(int), typeof(ExtendGrid), new PropertyMetadata(new PropertyChangedCallback(ExtendGrid.RowCountPropertyChangedCallback)));
	}
}
