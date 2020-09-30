using Mmc.Windows.Events;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Windows.Framework.Controls.InkPad
{
	public class InkPadWindow : Window, IComponentConnector
	{
		internal Grid LayoutRoot;

		public InkCanvas Ink;

		internal Border penPanel;

		internal RadioButton b_2;

		internal RadioButton b_4;

		internal RadioButton b_6;

		internal RadioButton b_8;

		internal RadioButton b_10;

		internal RadioButton b_12;

		internal RadioButton b_14;

		internal RadioButton radInk;

		internal RadioButton radErasePoint;

		internal RadioButton radEraseByStroke;

		internal RadioButton radSelect;

		internal StackPanel fishButtons;

		internal Button btnNew;

		internal Button btnSave;

		internal Button btnOpen;

		internal Button btnCut;

		internal Button btnCopy;

		internal Button btnPaste;

		internal Button btnDelete;

		internal Button btnSelectAll;

		internal Button btnFormatSelection;

		internal Button btnStylusSetting;

		internal Button btnClose;

		private bool _contentLoaded;

		public event WorkerCompletedEventHandler InkPadCloseCompleted;

		public InkPadWindow()
		{
			this.InitializeComponent();
			this.radInk.IsChecked = new bool?(true);
			this.b_2.IsChecked = new bool?(true);
		}

		private void btnNew_Click(object sender, RoutedEventArgs args)
		{
			this.Ink.Strokes.Clear();
		}

		private void btnOpen_Click(object sender, RoutedEventArgs args)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.Filter = "Ink Serialized Format (*.isf)|*.isf|All files (*.*)|*.*";
			if (dlg.ShowDialog(this).Value)
			{
				this.Ink.Strokes.Clear();
				try
				{
					using (FileStream file = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
					{
						if (!dlg.FileName.ToLower().EndsWith(".isf"))
						{
                            Messages.ShowMessage("The requested file is not a Ink Serialized Format file\r\n\r\nplease retry");
                        }
						else
						{
							this.Ink.Strokes = new StrokeCollection(file);
							file.Close();
						}
					}
				}
				catch (Exception exc)
				{
                    Messages.ShowMessage(exc.Message);
				}
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs args)
		{
			this.penPanel.Visibility = Visibility.Collapsed;
			base.Dispatcher.BeginInvoke(new Action(delegate
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Ink Serialized Format (*.isf)|*.isf|Bitmap files (*.bmp)|*.bmp|Bitmap files (*.jpg)|*.jpg|Bitmap files (*.png)|*.png";
				if (dlg.ShowDialog(this).Value)
				{
					try
					{
						Thread.Sleep(500);
						using (FileStream file = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
						{
							if (dlg.FilterIndex == 1)
							{
								this.Ink.Strokes.Save(file);
								file.Close();
							}
							else
							{
								BmpBitmapEncoder encoder = new BmpBitmapEncoder();
								BitmapSource bitsoure = Imaging.CreateBitmapSourceFromHBitmap(this.GetScreenImage(new Rectangle
								{
									Width = (int)this.Ink.ActualWidth,
									Height = (int)this.Ink.ActualHeight
								}).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
								encoder.Frames.Add(BitmapFrame.Create(bitsoure));
								encoder.Save(file);
								file.Close();
							}
						}
					}
					catch (Exception exc)
					{
                        Messages.ShowMessage(exc.Message);
					}
				}
				this.penPanel.Visibility = Visibility.Visible;
			}), DispatcherPriority.Background, new object[0]);
		}

		private void btnCut_Click(object sender, RoutedEventArgs args)
		{
			if (this.Ink.GetSelectedStrokes().Count > 0)
			{
				this.Ink.CutSelection();
			}
		}

		private void btnCopy_Click(object sender, RoutedEventArgs args)
		{
			if (this.Ink.GetSelectedStrokes().Count > 0)
			{
				this.Ink.CopySelection();
			}
		}

		private void btnPaste_Click(object sender, RoutedEventArgs args)
		{
			if (this.Ink.CanPaste())
			{
				this.Ink.Paste();
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs args)
		{
			if (this.Ink.GetSelectedStrokes().Count > 0)
			{
				foreach (Stroke strk in this.Ink.GetSelectedStrokes())
				{
					this.Ink.Strokes.Remove(strk);
				}
			}
		}

		private void btnSelectAll_Click(object sender, RoutedEventArgs args)
		{
			this.Ink.Select(this.Ink.Strokes);
			this.radSelect.IsChecked = new bool?(true);
		}

		private void rad_Click(object sender, RoutedEventArgs e)
		{
			RadioButton rad = sender as RadioButton;
			this.Ink.EditingMode = (InkCanvasEditingMode)rad.Tag;
		}

		private void penSize_Click(object sender, RoutedEventArgs e)
		{
			RadioButton rad = sender as RadioButton;
			DrawingAttributes inkDA = new DrawingAttributes();
			inkDA.Width = rad.FontSize;
			inkDA.Height = rad.FontSize;
			inkDA.Color = this.Ink.DefaultDrawingAttributes.Color;
			inkDA.IsHighlighter = this.Ink.DefaultDrawingAttributes.IsHighlighter;
			this.Ink.DefaultDrawingAttributes = inkDA;
		}

		private void btnStylusSettings_Click(object sender, RoutedEventArgs e)
		{
			StylusSettings dlg = new StylusSettings();
			dlg.Owner = this;
			dlg.DrawingAttributes = this.Ink.DefaultDrawingAttributes;
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				this.Ink.DefaultDrawingAttributes = dlg.DrawingAttributes;
			}
		}

		private void btnFormat_Click(object sender, RoutedEventArgs e)
		{
			StylusSettings dlg = new StylusSettings();
			dlg.Owner = this;
			StrokeCollection strokes = this.Ink.GetSelectedStrokes();
			if (strokes.Count > 0)
			{
				dlg.DrawingAttributes = strokes[0].DrawingAttributes;
			}
			else
			{
				dlg.DrawingAttributes = this.Ink.DefaultDrawingAttributes;
			}
			if (dlg.ShowDialog().GetValueOrDefault())
			{
				foreach (Stroke strk in strokes)
				{
					strk.DrawingAttributes = dlg.DrawingAttributes;
				}
			}
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			if (this.InkPadCloseCompleted != null)
			{
				this.InkPadCloseCompleted(this, new EventArgs());
			}
		}

		private Bitmap GetScreenImage(Rectangle rec)
		{
			Bitmap bitmap = new Bitmap(rec.Width, rec.Height);
			Graphics gp = Graphics.FromImage(bitmap);
			gp.CopyFromScreen(rec.X, rec.Y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
			return bitmap;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocater = new Uri("/Mmc.Windows.Framework;component/controls/inkpad/inkpadwindow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocater);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.LayoutRoot = (Grid)target;
				break;
			case 2:
				this.Ink = (InkCanvas)target;
				break;
			case 3:
				this.penPanel = (Border)target;
				break;
			case 4:
				this.b_2 = (RadioButton)target;
				this.b_2.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 5:
				this.b_4 = (RadioButton)target;
				this.b_4.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 6:
				this.b_6 = (RadioButton)target;
				this.b_6.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 7:
				this.b_8 = (RadioButton)target;
				this.b_8.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 8:
				this.b_10 = (RadioButton)target;
				this.b_10.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 9:
				this.b_12 = (RadioButton)target;
				this.b_12.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 10:
				this.b_14 = (RadioButton)target;
				this.b_14.Click += new RoutedEventHandler(this.penSize_Click);
				break;
			case 11:
				this.radInk = (RadioButton)target;
				this.radInk.Click += new RoutedEventHandler(this.rad_Click);
				break;
			case 12:
				this.radErasePoint = (RadioButton)target;
				this.radErasePoint.Click += new RoutedEventHandler(this.rad_Click);
				break;
			case 13:
				this.radEraseByStroke = (RadioButton)target;
				this.radEraseByStroke.Click += new RoutedEventHandler(this.rad_Click);
				break;
			case 14:
				this.radSelect = (RadioButton)target;
				this.radSelect.Click += new RoutedEventHandler(this.rad_Click);
				break;
			case 15:
				this.fishButtons = (StackPanel)target;
				break;
			case 16:
				this.btnNew = (Button)target;
				this.btnNew.Click += new RoutedEventHandler(this.btnNew_Click);
				break;
			case 17:
				this.btnSave = (Button)target;
				this.btnSave.Click += new RoutedEventHandler(this.btnSave_Click);
				break;
			case 18:
				this.btnOpen = (Button)target;
				this.btnOpen.Click += new RoutedEventHandler(this.btnOpen_Click);
				break;
			case 19:
				this.btnCut = (Button)target;
				this.btnCut.Click += new RoutedEventHandler(this.btnCut_Click);
				break;
			case 20:
				this.btnCopy = (Button)target;
				this.btnCopy.Click += new RoutedEventHandler(this.btnCopy_Click);
				break;
			case 21:
				this.btnPaste = (Button)target;
				this.btnPaste.Click += new RoutedEventHandler(this.btnPaste_Click);
				break;
			case 22:
				this.btnDelete = (Button)target;
				this.btnDelete.Click += new RoutedEventHandler(this.btnDelete_Click);
				break;
			case 23:
				this.btnSelectAll = (Button)target;
				this.btnSelectAll.Click += new RoutedEventHandler(this.btnSelectAll_Click);
				break;
			case 24:
				this.btnFormatSelection = (Button)target;
				this.btnFormatSelection.Click += new RoutedEventHandler(this.btnFormat_Click);
				break;
			case 25:
				this.btnStylusSetting = (Button)target;
				this.btnStylusSetting.Click += new RoutedEventHandler(this.btnStylusSettings_Click);
				break;
			case 26:
				this.btnClose = (Button)target;
				this.btnClose.Click += new RoutedEventHandler(this.btnClose_Click);
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}
	}
}
