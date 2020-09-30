using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Wpf.Toolkit.Controls.Panels
{
	/// <summary>
	///     可以指定子元素布局间隔的StackPanel
	/// </summary>
	// Token: 0x0200001D RID: 29
	public class StackPanelWithSpacing : Panel
	{
		/// <summary>
		///     获取或设置面板的布局方向
		/// </summary>
		/// <value>布局方向</value>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000039AC File Offset: 0x00001BAC
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000039CE File Offset: 0x00001BCE
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

		/// <summary>
		///     获取或设置面板的子元素之间的间隔距离
		/// </summary>
		/// <value>两个子元素之间的间隔距离</value>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000039E4 File Offset: 0x00001BE4
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003A06 File Offset: 0x00001C06
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

		/// <summary>
		///     获取一个值，该值指示此 <see cref="T:System.Windows.Controls.Panel" /> 是否在单个维度中排列其子代。
		/// </summary>
		/// <value><c>true</c> if this instance has logical orientation; otherwise, <c>false</c>.</value>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003A1C File Offset: 0x00001C1C
		protected override bool HasLogicalOrientation
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		///     如果面板支持只有一个维度的布局，则为面板的 <see cref="T:System.Windows.Controls.Orientation" />。
		/// </summary>
		/// <value>The logical orientation.</value>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003A30 File Offset: 0x00001C30
		protected override Orientation LogicalOrientation
		{
			get
			{
				return this.Orientation;
			}
		}

		/// <summary>
		///     当在派生类中重写时，请测量子元素在布局中所需的大小，然后确定 <see cref="T:System.Windows.FrameworkElement" /> 派生类的大小。
		/// </summary>
		/// <param name="availableSize">此元素可以赋给子元素的可用大小。 可以指定无穷大值，这表示元素的大小将调整为内容的可用大小。</param>
		/// <returns>此元素在布局过程中所需的大小，这是由此元素根据对其子元素大小的计算而确定的。</returns>
		// Token: 0x0600008F RID: 143 RVA: 0x00003A48 File Offset: 0x00001C48
		protected override Size MeasureOverride(Size availableSize)
		{
			Orientation orientation = this.Orientation;
			bool flag = orientation == Orientation.Horizontal;
			if (flag)
			{
				availableSize.Width = double.PositiveInfinity;
			}
			else
			{
				availableSize.Height = double.PositiveInfinity;
			}
			double spaceBetweenItems = this.SpaceBetweenItems;
			double num = 0.0;
			double num2 = 0.0;
			bool flag2 = false;
			foreach (object obj in base.InternalChildren)
			{
				UIElement uielement = (UIElement)obj;
				uielement.Measure(availableSize);
				Size desiredSize = uielement.DesiredSize;
				bool flag3 = orientation == Orientation.Horizontal;
				if (flag3)
				{
					num2 = Math.Max(num2, desiredSize.Height);
					num += desiredSize.Width;
				}
				else
				{
					num2 = Math.Max(num2, desiredSize.Width);
					num += desiredSize.Height;
				}
				bool flag4 = uielement.Visibility != Visibility.Collapsed;
				if (flag4)
				{
					num += spaceBetweenItems;
					flag2 = true;
				}
			}
			bool flag5 = flag2;
			if (flag5)
			{
				num -= spaceBetweenItems;
			}
			bool flag6 = orientation == Orientation.Horizontal;
			Size result;
			if (flag6)
			{
				result = new Size(num, num2);
			}
			else
			{
				result = new Size(num2, num);
			}
			return result;
		}

		/// <summary>
		///     在派生类中重写时，请为 <see cref="T:System.Windows.FrameworkElement" /> 派生类定位子元素并确定大小。
		/// </summary>
		/// <param name="finalSize">父级中此元素应用来排列自身及其子元素的最终区域。</param>
		/// <returns>所用的实际大小。</returns>
		// Token: 0x06000090 RID: 144 RVA: 0x00003BA0 File Offset: 0x00001DA0
		protected override Size ArrangeOverride(Size finalSize)
		{
			Orientation orientation = this.Orientation;
			double spaceBetweenItems = this.SpaceBetweenItems;
			double num = 0.0;
			foreach (object obj in base.InternalChildren)
			{
				UIElement uielement = (UIElement)obj;
				bool flag = orientation == Orientation.Horizontal;
				if (flag)
				{
					uielement.Arrange(new Rect(num, 0.0, uielement.DesiredSize.Width, finalSize.Height));
					num += uielement.DesiredSize.Width;
				}
				else
				{
					uielement.Arrange(new Rect(0.0, num, finalSize.Width, uielement.DesiredSize.Height));
					num += uielement.DesiredSize.Height;
				}
				bool flag2 = uielement.Visibility != Visibility.Collapsed;
				if (flag2)
				{
					num += spaceBetweenItems;
				}
			}
			return finalSize;
		}

		/// <summary>
		///     布局方向属性
		/// </summary>
		// Token: 0x04000028 RID: 40
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(StackPanelWithSpacing), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

		/// <summary>
		///     两个子元素之间的间隔距离属性
		/// </summary>
		// Token: 0x04000029 RID: 41
		public static readonly DependencyProperty SpaceBetweenItemsProperty = DependencyProperty.Register("SpaceBetweenItems", typeof(double), typeof(StackPanelWithSpacing), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
	}
}
