using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Windows.Framework.Controls.Panels
{
	public class StackPanelWithSpacing : Panel
	{
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(StackPanelWithSpacing), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

		public static readonly DependencyProperty SpaceBetweenItemsProperty = DependencyProperty.Register("SpaceBetweenItems", typeof(double), typeof(StackPanelWithSpacing), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

		public Orientation Orientation
		{
			get
			{
				return (Orientation)base.GetValue(StackPanelWithSpacing.OrientationProperty);
			}
			set
			{
				base.SetValue(StackPanelWithSpacing.OrientationProperty, value);
			}
		}

		public double SpaceBetweenItems
		{
			get
			{
				return (double)base.GetValue(StackPanelWithSpacing.SpaceBetweenItemsProperty);
			}
			set
			{
				base.SetValue(StackPanelWithSpacing.SpaceBetweenItemsProperty, value);
			}
		}

		protected override bool HasLogicalOrientation
		{
			get
			{
				return true;
			}
		}

		protected override Orientation LogicalOrientation
		{
			get
			{
				return this.Orientation;
			}
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Orientation orientation = this.Orientation;
			if (orientation == Orientation.Horizontal)
			{
				availableSize.Width = double.PositiveInfinity;
			}
			else
			{
				availableSize.Height = double.PositiveInfinity;
			}
			double spaceBetweenItems = this.SpaceBetweenItems;
			double pos = 0.0;
			double maxWidth = 0.0;
			bool hasVisibleItems = false;
			foreach (UIElement element in base.InternalChildren)
			{
				element.Measure(availableSize);
				Size desiredSize = element.DesiredSize;
				if (orientation == Orientation.Horizontal)
				{
					maxWidth = Math.Max(maxWidth, desiredSize.Height);
					pos += desiredSize.Width;
				}
				else
				{
					maxWidth = Math.Max(maxWidth, desiredSize.Width);
					pos += desiredSize.Height;
				}
				if (element.Visibility != Visibility.Collapsed)
				{
					pos += spaceBetweenItems;
					hasVisibleItems = true;
				}
			}
			if (hasVisibleItems)
			{
				pos -= spaceBetweenItems;
			}
			Size result;
			if (orientation == Orientation.Horizontal)
			{
				result = new Size(pos, maxWidth);
			}
			else
			{
				result = new Size(maxWidth, pos);
			}
			return result;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			Orientation orientation = this.Orientation;
			double spaceBetweenItems = this.SpaceBetweenItems;
			double pos = 0.0;
			foreach (UIElement element in base.InternalChildren)
			{
				if (orientation == Orientation.Horizontal)
				{
					element.Arrange(new Rect(pos, 0.0, element.DesiredSize.Width, finalSize.Height));
					pos += element.DesiredSize.Width;
				}
				else
				{
					element.Arrange(new Rect(0.0, pos, finalSize.Width, element.DesiredSize.Height));
					pos += element.DesiredSize.Height;
				}
				if (element.Visibility != Visibility.Collapsed)
				{
					pos += spaceBetweenItems;
				}
			}
			return finalSize;
		}
	}
}
