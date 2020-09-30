using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmc.Windows.Framework.Controls.Panels
{
	public class UniformGridWithSpacing : UniformGrid
	{
		public static readonly DependencyProperty SpaceBetweenColumnsProperty = DependencyProperty.Register("SpaceBetweenColumns", typeof(double), typeof(UniformGridWithSpacing), new FrameworkPropertyMetadata(7.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

		public static readonly DependencyProperty SpaceBetweenRowsProperty = DependencyProperty.Register("SpaceBetweenRows", typeof(double), typeof(UniformGridWithSpacing), new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

		public double SpaceBetweenColumns
		{
			get
			{
				return (double)base.GetValue(UniformGridWithSpacing.SpaceBetweenColumnsProperty);
			}
			set
			{
				base.SetValue(UniformGridWithSpacing.SpaceBetweenColumnsProperty, value);
			}
		}

		public double SpaceBetweenRows
		{
			get
			{
				return (double)base.GetValue(UniformGridWithSpacing.SpaceBetweenRowsProperty);
			}
			set
			{
				base.SetValue(UniformGridWithSpacing.SpaceBetweenRowsProperty, value);
			}
		}

		protected override Size MeasureOverride(Size constraint)
		{
			Size s = base.MeasureOverride(constraint);
			return new Size(s.Width + (double)Math.Max(0, base.Columns - 1) * this.SpaceBetweenColumns, s.Height + (double)Math.Max(0, base.Rows - 1) * this.SpaceBetweenRows);
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			double spaceBetweenColumns = this.SpaceBetweenColumns;
			double spaceBetweenRows = this.SpaceBetweenRows;
			int rows = Math.Max(1, base.Rows);
			int columns = Math.Max(1, base.Columns);
			Rect rect = new Rect(0.0, 0.0, (arrangeSize.Width - spaceBetweenColumns * (double)(columns - 1)) / (double)columns, (arrangeSize.Height - spaceBetweenRows * (double)(rows - 1)) / (double)rows);
			int currentColumn = base.FirstColumn;
			rect.X += (double)currentColumn * (rect.Width + spaceBetweenColumns);
			foreach (UIElement element in base.InternalChildren)
			{
				element.Arrange(rect);
				if (element.Visibility != Visibility.Collapsed)
				{
					if (++currentColumn >= columns)
					{
						currentColumn = 0;
						rect.X = 0.0;
						rect.Y += rect.Height + spaceBetweenRows;
					}
					else
					{
						rect.X += rect.Width + spaceBetweenColumns;
					}
				}
			}
			return arrangeSize;
		}
	}
}
