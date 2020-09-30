using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mmc.Windows.Framework.Controls.Progress
{
	[TemplatePart(Name = "PART_Body", Type = typeof(Grid))]
	public class ProgressRing : Control
	{
		public static readonly DependencyProperty SpeedProperty;

		public static readonly DependencyProperty ItemsProperty;

		public Grid Body
		{
			get;
			private set;
		}

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

		static ProgressRing()
		{
			ProgressRing.SpeedProperty = DependencyProperty.Register("Speed ", typeof(Duration), typeof(ProgressRing), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(2.5)), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ProgressRing.SpeedChanged), new CoerceValueCallback(ProgressRing.SpeedValueCallback)));
			ProgressRing.ItemsProperty = DependencyProperty.Register("Items", typeof(int), typeof(ProgressRing), new FrameworkPropertyMetadata(6, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ProgressRing.ItemsChanged), new CoerceValueCallback(ProgressRing.ItemsValueCallback)));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
		}

		public ProgressRing()
		{
			ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri("/Mmc.Windows.Framework;component/Themes/ProgressRingStyle.xaml", UriKind.Relative));
			base.Style = (Style)res["ProgressRingStyle"];
		}

		private void SetStoryBoard(Duration speed)
		{
			int delay = 0;
			foreach (Ellipse ellipse in this.Body.Children)
			{
				ellipse.RenderTransform = new RotateTransform(0.0);
				DoubleAnimation animation = new DoubleAnimation(0.0, -360.0, speed)
				{
					RepeatBehavior = RepeatBehavior.Forever,
					EasingFunction = new QuarticEase
					{
						EasingMode = EasingMode.EaseInOut
					},
					BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds((double)(delay += 100)))
				};
				Storyboard storyboard = new Storyboard();
				storyboard.Children.Add(animation);
				Storyboard.SetTarget(animation, ellipse);
				Storyboard.SetTargetProperty(animation, new PropertyPath("(Rectangle.RenderTransform).(RotateTransform.Angle)", new object[0]));
				storyboard.Begin();
			}
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.Body = (base.Template.FindName("PART_Body", this) as Grid);
			ProgressRing.ItemsChanged(this, new DependencyPropertyChangedEventArgs(ProgressRing.ItemsProperty, 0, this.Items));
			ProgressRing.SpeedChanged(this, new DependencyPropertyChangedEventArgs(ProgressRing.SpeedProperty, Duration.Forever, this.Speed));
		}

		private static void SpeedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			ProgressRing wpr = (ProgressRing)dependencyObject;
			if (wpr.Body != null)
			{
				Duration speed = (Duration)dependencyPropertyChangedEventArgs.NewValue;
				wpr.SetStoryBoard(speed);
			}
		}

		private static object SpeedValueCallback(DependencyObject dependencyObject, object baseValue)
		{
			object result;
			if (((Duration)baseValue).HasTimeSpan && ((Duration)baseValue).TimeSpan > TimeSpan.FromSeconds(5.0))
			{
				result = new Duration(TimeSpan.FromSeconds(5.0));
			}
			else
			{
				result = baseValue;
			}
			return result;
		}

		private static void ItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProgressRing wpr = (ProgressRing)d;
			if (wpr.Body != null)
			{
				wpr.Body.Children.Clear();
				int items = (int)e.NewValue;
				for (int i = 0; i < items; i++)
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
					wpr.Body.Children.Add(ellipse);
					Binding binding = new Binding(Control.ForegroundProperty.Name)
					{
						RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ProgressRing), 1)
					};
					BindingOperations.SetBinding(ellipse, Shape.FillProperty, binding);
					Grid.SetColumn(ellipse, 2);
					Grid.SetRow(ellipse, 0);
				}
				wpr.SetStoryBoard(wpr.Speed);
			}
		}

		private static object ItemsValueCallback(DependencyObject d, object basevalue)
		{
			object result;
			if ((int)basevalue > 20)
			{
				result = 20;
			}
			else if ((int)basevalue < 1)
			{
				result = 1;
			}
			else
			{
				result = basevalue;
			}
			return result;
		}
	}
}
