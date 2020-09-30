using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mmc.Wpf.Toolkit.Attached
{
	/// <summary>
	///     针对控件进行属性扩展
	/// </summary>
	// Token: 0x02000021 RID: 33
	public class AttachedProperties
	{
		/// <summary>
		///     获取控件的方向属性
		/// </summary>
		/// <param name="obj">要获取方向的控件对象</param>
		/// <returns>方向</returns>
		// Token: 0x060000B7 RID: 183 RVA: 0x00004E74 File Offset: 0x00003074
		public static Orientation GetOrientation(DependencyObject obj)
		{
			return (Orientation)obj.GetValue(AttachedProperties.OrientationProperty);
		}

		/// <summary>
		///     设置控件的方向属性
		/// </summary>
		/// <param name="obj">要设置方向的控件对象</param>
		/// <param name="value">新的方向值</param>
		// Token: 0x060000B8 RID: 184 RVA: 0x00004E96 File Offset: 0x00003096
		public static void SetOrientation(DependencyObject obj, Orientation value)
		{
			obj.SetValue(AttachedProperties.OrientationProperty, value);
		}

		/// <summary>
		///     获取控件背景属性
		/// </summary>
		/// <param name="obj">要获取背景属性的控件</param>
		/// <returns>背景笔刷</returns>
		// Token: 0x060000B9 RID: 185 RVA: 0x00004EAC File Offset: 0x000030AC
		public static Brush GetBackground(DependencyObject obj)
		{
			return (Brush)obj.GetValue(AttachedProperties.BackgroundProperty);
		}

		/// <summary>
		///     设置控件的背景属性
		/// </summary>
		/// <param name="obj">要设置背景的控件对象</param>
		/// <param name="value">新的背景笔刷值</param>
		// Token: 0x060000BA RID: 186 RVA: 0x00004ECE File Offset: 0x000030CE
		public static void SetBackground(DependencyObject obj, Brush value)
		{
			obj.SetValue(AttachedProperties.BackgroundProperty, value);
		}

		/// <summary>
		///     方向附加属性
		/// </summary>
		// Token: 0x04000051 RID: 81
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(AttachedProperties), new PropertyMetadata(Orientation.Vertical));

		/// <summary>
		///     背景附加属性
		/// </summary>
		// Token: 0x04000052 RID: 82
		public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(AttachedProperties), new PropertyMetadata());
	}
}
