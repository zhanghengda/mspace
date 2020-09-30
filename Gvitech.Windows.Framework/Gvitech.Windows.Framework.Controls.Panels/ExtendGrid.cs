using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Windows.Framework.Controls.Panels
{
	public class ExtendGrid : Grid
	{
		public static readonly DependencyProperty ColumnCountProperty;

		public static readonly DependencyProperty RowCountProperty;

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

		static ExtendGrid()
		{
			ExtendGrid.ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(ExtendGrid), new PropertyMetadata(new PropertyChangedCallback(ExtendGrid.ColumnCountPropertyChangedCallback)));
			ExtendGrid.RowCountProperty = DependencyProperty.Register("RowCount", typeof(int), typeof(ExtendGrid), new PropertyMetadata(new PropertyChangedCallback(ExtendGrid.RowCountPropertyChangedCallback)));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendGrid), new FrameworkPropertyMetadata(typeof(ExtendGrid)));
		}

		private static void ColumnCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d != null)
			{
				ExtendGrid initGrid = d as ExtendGrid;
				int newColumnCount = 0;
				int.TryParse(e.NewValue.ToString(), out newColumnCount);
				int total = initGrid.ColumnDefinitions.Count;
				int count = newColumnCount - total;
				for (int i = count - 1; i >= 0; i--)
				{
					initGrid.ColumnDefinitions.Add(new ColumnDefinition());
				}
				for (int i = 0; i > count; i--)
				{
					initGrid.ColumnDefinitions.RemoveAt(total - i - 1);
				}
			}
		}

		private static void RowCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d != null)
			{
				ExtendGrid initGrid = d as ExtendGrid;
				int newRowCount = 0;
				int.TryParse(e.NewValue.ToString(), out newRowCount);
				int total = initGrid.RowDefinitions.Count;
				int count = newRowCount - total;
				for (int i = count - 1; i >= 0; i--)
				{
					initGrid.RowDefinitions.Add(new RowDefinition());
				}
				for (int i = 0; i > count; i--)
				{
					initGrid.RowDefinitions.RemoveAt(total - i - 1);
				}
			}
		}
	}
}
