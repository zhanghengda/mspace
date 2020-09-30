using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmc.Wpf.Toolkit.Controls.Panels
{
	/// <summary>
	///     可以指定子元素之间间隔距离的UniformGrid
	/// </summary>
	// Token: 0x0200001E RID: 30
	public class UniformGridWithSpacing : UniformGrid
	{
		/// <summary>
		///     获取或设置两列之间的间隔距离
		/// </summary>
		/// <value>两列之间的间隔距离</value>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003D4C File Offset: 0x00001F4C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003D6E File Offset: 0x00001F6E
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

		/// <summary>
		///     获取或设置两行之间的间隔距离
		/// </summary>
		/// <value>两行之间的间隔距离</value>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D84 File Offset: 0x00001F84
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003DA6 File Offset: 0x00001FA6
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

		/// <summary>
		///     通过测量所有子元素计算 <see cref="T:System.Windows.Controls.Primitives.UniformGrid" /> 的期望大小。
		/// </summary>
		/// <param name="constraint">网格可用区域的 <see cref="T:System.Windows.Size" />。</param>
		/// <returns>基于网格的子内容和 <paramref name="constraint" /> 参数的期望 <see cref="T:System.Windows.Size" />。</returns>
		// Token: 0x06000097 RID: 151 RVA: 0x00003DBC File Offset: 0x00001FBC
		protected override Size MeasureOverride(Size constraint)
		{
			Size size = base.MeasureOverride(constraint);
			return new Size(size.Width + (double)Math.Max(0, base.Columns - 1) * this.SpaceBetweenColumns, size.Height + (double)Math.Max(0, base.Rows - 1) * this.SpaceBetweenRows);
		}

		/// <summary>
		///     通过在所有子元素之间平均分配空间来定义 <see cref="T:System.Windows.Controls.Primitives.UniformGrid" /> 的布局。
		/// </summary>
		/// <param name="arrangeSize">供网格使用的区域的 <see cref="T:System.Windows.Size" />。</param>
		/// <returns>为显示可见子元素而呈现的网格的实际 <see cref="T:System.Windows.Size" />。</returns>
		// Token: 0x06000098 RID: 152 RVA: 0x00003E18 File Offset: 0x00002018
		protected override Size ArrangeOverride(Size arrangeSize)
		{
			double spaceBetweenColumns = this.SpaceBetweenColumns;
			double spaceBetweenRows = this.SpaceBetweenRows;
			int num = Math.Max(1, base.Rows);
			int num2 = Math.Max(1, base.Columns);
			Rect finalRect = new Rect(0.0, 0.0, (arrangeSize.Width - spaceBetweenColumns * (double)(num2 - 1)) / (double)num2, (arrangeSize.Height - spaceBetweenRows * (double)(num - 1)) / (double)num);
			int num3 = base.FirstColumn;
			finalRect.X += (double)num3 * (finalRect.Width + spaceBetweenColumns);
			foreach (object obj in base.InternalChildren)
			{
				UIElement uielement = (UIElement)obj;
				uielement.Arrange(finalRect);
				bool flag = uielement.Visibility != Visibility.Collapsed;
				if (flag)
				{
					int num4 = num3 + 1;
					num3 = num4;
					bool flag2 = num4 >= num2;
					if (flag2)
					{
						num3 = 0;
						finalRect.X = 0.0;
						finalRect.Y += finalRect.Height + spaceBetweenRows;
					}
					else
					{
						finalRect.X += finalRect.Width + spaceBetweenColumns;
					}
				}
			}
			return arrangeSize;
		}

		/// <summary>
		///     两列之间的间隔距离属性
		/// </summary>
		// Token: 0x0400002A RID: 42
		public static readonly DependencyProperty SpaceBetweenColumnsProperty = DependencyProperty.Register("SpaceBetweenColumns", typeof(double), typeof(UniformGridWithSpacing), new FrameworkPropertyMetadata(7.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

		/// <summary>
		///     两行之间的间隔距离属性
		/// </summary>
		// Token: 0x0400002B RID: 43
		public static readonly DependencyProperty SpaceBetweenRowsProperty = DependencyProperty.Register("SpaceBetweenRows", typeof(double), typeof(UniformGridWithSpacing), new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
	}
}
