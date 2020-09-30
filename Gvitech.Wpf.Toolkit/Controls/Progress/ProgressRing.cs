using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mmc.Wpf.Toolkit.Controls.Progress
{
	// Token: 0x0200001B RID: 27
	[TemplatePart(Name = "PART_Body", Type = typeof(Grid))]
	public class ProgressRing : Control
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003204 File Offset: 0x00001404
		static ProgressRing()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032D8 File Offset: 0x000014D8
		public ProgressRing()
		{
			ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri("/Mmc.Wpf.Toolkit;component/Themes/ProgressRingStyle.xaml", UriKind.Relative));
			base.Style = (Style)resourceDictionary["ProgressRingStyle"];
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000331C File Offset: 0x0000151C
		private void SetStoryBoard(Duration speed)
		{
			int num = 0;
			foreach (object obj in this.Body.Children)
			{
				Ellipse ellipse = (Ellipse)obj;
				ellipse.RenderTransform = new RotateTransform(0.0);
				DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, -360.0, speed)
				{
					RepeatBehavior = RepeatBehavior.Forever,
					EasingFunction = new QuarticEase
					{
						EasingMode = EasingMode.EaseInOut
					},
					BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds((double)(num += 100)))
				};
				Storyboard storyboard = new Storyboard();
				storyboard.Children.Add(doubleAnimation);
				Storyboard.SetTarget(doubleAnimation, ellipse);
				Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Rectangle.RenderTransform).(RotateTransform.Angle)", new object[0]));
				storyboard.Begin();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003434 File Offset: 0x00001634
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.Body = (base.Template.FindName("PART_Body", this) as Grid);
			ProgressRing.ItemsChanged(this, new DependencyPropertyChangedEventArgs(ProgressRing.ItemsProperty, 0, this.Items));
			ProgressRing.SpeedChanged(this, new DependencyPropertyChangedEventArgs(ProgressRing.SpeedProperty, Duration.Forever, this.Speed));
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000034AE File Offset: 0x000016AE
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000034B6 File Offset: 0x000016B6
		public Grid Body { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000034C0 File Offset: 0x000016C0
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000034E2 File Offset: 0x000016E2
		public Duration Speed
		{
			get
			{
				return (Duration)base.GetValue(ProgressRing.SpeedProperty);
			}
			set
			{
				base.SetValue(ProgressRing.SpeedProperty, value);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000034F8 File Offset: 0x000016F8
		private static void SpeedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			ProgressRing progressRing = (ProgressRing)dependencyObject;
			bool flag = progressRing.Body == null;
			if (!flag)
			{
				Duration storyBoard = (Duration)dependencyPropertyChangedEventArgs.NewValue;
				progressRing.SetStoryBoard(storyBoard);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003534 File Offset: 0x00001734
		private static object SpeedValueCallback(DependencyObject dependencyObject, object baseValue)
		{
			bool flag = ((Duration)baseValue).HasTimeSpan && ((Duration)baseValue).TimeSpan > TimeSpan.FromSeconds(5.0);
			object result;
			if (flag)
			{
				result = new Duration(TimeSpan.FromSeconds(5.0));
			}
			else
			{
				result = baseValue;
			}
			return result;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000359C File Offset: 0x0000179C
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000035BE File Offset: 0x000017BE
		public int Items
		{
			get
			{
				return (int)base.GetValue(ProgressRing.ItemsProperty);
			}
			set
			{
				base.SetValue(ProgressRing.ItemsProperty, value);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035D4 File Offset: 0x000017D4
		private static void ItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProgressRing progressRing = (ProgressRing)d;
			bool flag = progressRing.Body == null;
			if (!flag)
			{
				progressRing.Body.Children.Clear();
				int num = (int)e.NewValue;
				int num2;
				for (int i = 0; i < num; i = num2 + 1)
				{
					Ellipse ellipse = new Ellipse
					{
						VerticalAlignment = VerticalAlignment.Stretch,
						HorizontalAlignment = HorizontalAlignment.Stretch,
						ClipToBounds = false,
						RenderTransformOrigin = new Point(0.5, 2.5),
						Width = 15.0,
						Height = 15.0
					};
					progressRing.Body.Children.Add(ellipse);
					Binding binding = new Binding(Control.ForegroundProperty.Name)
					{
						RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ProgressRing), 1)
					};
					BindingOperations.SetBinding(ellipse, Shape.FillProperty, binding);
					Grid.SetColumn(ellipse, 2);
					Grid.SetRow(ellipse, 0);
					num2 = i;
				}
				progressRing.SetStoryBoard(progressRing.Speed);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003710 File Offset: 0x00001910
		private static object ItemsValueCallback(DependencyObject d, object basevalue)
		{
			bool flag = (int)basevalue > 20;
			object result;
			if (flag)
			{
				result = 20;
			}
			else
			{
				bool flag2 = (int)basevalue < 1;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = basevalue;
				}
			}
			return result;
		}

		// Token: 0x04000024 RID: 36
		public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed ", typeof(Duration), typeof(ProgressRing), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(2.5)), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ProgressRing.SpeedChanged), new CoerceValueCallback(ProgressRing.SpeedValueCallback)));

		// Token: 0x04000025 RID: 37
		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(int), typeof(ProgressRing), new FrameworkPropertyMetadata(6, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ProgressRing.ItemsChanged), new CoerceValueCallback(ProgressRing.ItemsValueCallback)));
	}
}
