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

namespace Mmc.Windows.Framework.Controls.InkPad
{
	public class StylusSettings : Window, IComponentConnector
	{
		private Color currColor = Colors.Black;

		private double penHeight = 2.0;

		private double penWidth = 2.0;

		internal DockPanel LayoutRoot;

		internal Button btnOk;

		internal CheckBox chkPressure;

		internal CheckBox chkHighlight;

		internal UniformGrid ugColors;

		private bool _contentLoaded;

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

		public StylusSettings()
		{
			this.InitializeComponent();
			this.createGridOfColor();
		}

		private void createGridOfColor()
		{
			PropertyInfo[] props = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
			List<string> colors = new List<string>();
			PropertyInfo[] array = props;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo p = array[i];
				colors.Add(p.GetValue(null, null).ToString());
			}
			colors.Sort();
			colors.RemoveAt(0);
			colors.Reverse();
			foreach (string p2 in colors)
			{
				Button b = new Button();
				b.Background = new SolidColorBrush
				{
					Color = (Color)ColorConverter.ConvertFromString(p2)
				};
				b.Click += new RoutedEventHandler(this.b_Click);
				this.ugColors.Children.Add(b);
			}
		}

		private void b_Click(object sender, RoutedEventArgs e)
		{
			SolidColorBrush sb = (SolidColorBrush)(sender as Button).Background;
			this.currColor = sb.Color;
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocater = new Uri("/Mmc.Windows.Framework;component/controls/inkpad/stylussettings.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocater);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.LayoutRoot = (DockPanel)target;
				break;
			case 2:
				this.btnOk = (Button)target;
				this.btnOk.Click += new RoutedEventHandler(this.btnOk_Click);
				break;
			case 3:
				this.chkPressure = (CheckBox)target;
				break;
			case 4:
				this.chkHighlight = (CheckBox)target;
				break;
			case 5:
				this.ugColors = (UniformGrid)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}
	}
}
