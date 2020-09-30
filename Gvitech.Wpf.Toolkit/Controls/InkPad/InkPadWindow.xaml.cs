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
using Mmc.Windows.Events;
using Microsoft.Win32;

namespace Mmc.Wpf.Toolkit.Controls.InkPad
{
    /// <summary>
    ///     InkPadWindow.xaml 的交互逻辑
    /// </summary>
    /// <summary>
    /// InkPadWindow
    /// </summary>
    // Token: 0x0200001F RID: 31
    public partial class InkPadWindow : Window
    {
        private readonly RisCaptureLib.ScreenCaputre screenCaputre = new RisCaptureLib.ScreenCaputre();
        private System.Windows.Size? lastSize;
        public InkPadWindow()
        {
            this.InitializeComponent();
            this.radInk.IsChecked = new bool?(true);
            this.b_2.IsChecked = new bool?(true);
            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            //Show();
            Focus();
        }

        private void OnScreenCaputred(object sender, RisCaptureLib.ScreenCaputredEventArgs e)
        {
            //set last size
            lastSize = new System.Windows.Size(e.Bmp.Width, e.Bmp.Height);
           
            base.Dispatcher.BeginInvoke(new Action(delegate
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Bitmap files (*.jpg)|*.jpg|Bitmap files (*.png)|*.png";
                bool value = saveFileDialog.ShowDialog(this).Value;
                if (value)
                {
                    try
                    {
                        Thread.Sleep(500);
                        using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
                            var bmp = e.Bmp;
                            bmpBitmapEncoder.Frames.Add(BitmapFrame.Create(bmp));
                            bmpBitmapEncoder.Save(fileStream);
                            fileStream.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, base.Title);
                    }
                }
                this.penPanel.Visibility = Visibility.Visible;
            }), DispatcherPriority.Background, new object[0]);
            Show();

            //test

            //var win = new Window { SizeToContent = SizeToContent.WidthAndHeight, ResizeMode = ResizeMode.NoResize };

            //var canvas = new Canvas { Width = bmp.Width, Height = bmp.Height, Background = new ImageBrush(bmp) };

            //win.Content = canvas;
            //win.Show();
        }
        // Token: 0x0600009C RID: 156 RVA: 0x00004058 File Offset: 0x00002258
        private void btnNew_Click(object sender, RoutedEventArgs args)
        {
            this.Ink.Strokes.Clear();
        }

        // Token: 0x0600009D RID: 157 RVA: 0x0000406C File Offset: 0x0000226C
        private void btnOpen_Click(object sender, RoutedEventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "Ink Serialized Format (*.isf)|*.isf|All files (*.*)|*.*";
            bool value = openFileDialog.ShowDialog(this).Value;
            if (value)
            {
                this.Ink.Strokes.Clear();
                try
                {
                    using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        bool flag = !openFileDialog.FileName.ToLower().EndsWith(".isf");
                        if (flag)
                        {
                            MessageBox.Show("The requested file is not a Ink Serialized Format file\r\n\r\nplease retry", base.Title);
                        }
                        else
                        {
                            this.Ink.Strokes = new StrokeCollection(fileStream);
                            fileStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, base.Title);
                }
            }
        }

        // Token: 0x0600009E RID: 158 RVA: 0x00004164 File Offset: 0x00002364
        private void btnSave_Click(object sender, RoutedEventArgs args)
        {
            this.penPanel.Visibility = Visibility.Collapsed;
            base.Dispatcher.BeginInvoke(new Action(delegate
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Ink Serialized Format (*.isf)|*.isf|Bitmap files (*.bmp)|*.bmp|Bitmap files (*.jpg)|*.jpg|Bitmap files (*.png)|*.png";
                bool value = saveFileDialog.ShowDialog(this).Value;
                if (value)
                {
                    try
                    {
                        Thread.Sleep(500);
                        using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            bool flag = saveFileDialog.FilterIndex == 1;
                            if (flag)
                            {
                                this.Ink.Strokes.Save(fileStream);
                                fileStream.Close();
                            }
                            else
                            {
                                BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
                                BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(this.GetScreenImage(new Rectangle
                                {
                                    Width = (int)this.Ink.ActualWidth,
                                    Height = (int)this.Ink.ActualHeight
                                }).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                                bmpBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
                                bmpBitmapEncoder.Save(fileStream);
                                fileStream.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, base.Title);
                    }
                }
                this.penPanel.Visibility = Visibility.Visible;
            }), DispatcherPriority.Background, new object[0]);
        }

        // Token: 0x0600009F RID: 159 RVA: 0x00004194 File Offset: 0x00002394
        private void btnCut_Click(object sender, RoutedEventArgs args)
        {
            //bool flag = this.Ink.GetSelectedStrokes().Count > 0;
            //if (flag)
            //{
            //	this.Ink.CutSelection();
            //}

            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);
        }

        // Token: 0x060000A0 RID: 160 RVA: 0x000041C8 File Offset: 0x000023C8
        private void btnCopy_Click(object sender, RoutedEventArgs args)
        {
            bool SolarTerm = this.Ink.GetSelectedStrokes().Count > 0;
            if (SolarTerm)
            {
                this.Ink.CopySelection();
            }
        }

        // Token: 0x060000A1 RID: 161 RVA: 0x000041FC File Offset: 0x000023FC
        private void btnPaste_Click(object sender, RoutedEventArgs args)
        {
            bool SolarTerm = this.Ink.CanPaste();
            if (SolarTerm)
            {
                this.Ink.Paste();
            }
        }

        // Token: 0x060000A2 RID: 162 RVA: 0x00004228 File Offset: 0x00002428
        private void btnDelete_Click(object sender, RoutedEventArgs args)
        {
            bool flag = this.Ink.GetSelectedStrokes().Count > 0;
            if (flag)
            {
                foreach (Stroke item in this.Ink.GetSelectedStrokes())
                {
                    this.Ink.Strokes.Remove(item);
                }
            }
        }

        // Token: 0x060000A3 RID: 163 RVA: 0x000042A4 File Offset: 0x000024A4
        private void btnSelectAll_Click(object sender, RoutedEventArgs args)
        {
            this.Ink.Select(this.Ink.Strokes);
            this.radSelect.IsChecked = new bool?(true);
        }

        // Token: 0x060000A4 RID: 164 RVA: 0x000042D0 File Offset: 0x000024D0
        private void rad_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            this.Ink.EditingMode = (InkCanvasEditingMode)radioButton.Tag;
        }

        // Token: 0x060000A5 RID: 165 RVA: 0x000042FC File Offset: 0x000024FC
        private void penSize_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            DrawingAttributes drawingAttributes = new DrawingAttributes();
            drawingAttributes.Width = radioButton.FontSize;
            drawingAttributes.Height = radioButton.FontSize;
            drawingAttributes.Color = this.Ink.DefaultDrawingAttributes.Color;
            drawingAttributes.IsHighlighter = this.Ink.DefaultDrawingAttributes.IsHighlighter;
            this.Ink.DefaultDrawingAttributes = drawingAttributes;
        }

        // Token: 0x060000A6 RID: 166 RVA: 0x0000436C File Offset: 0x0000256C
        private void btnStylusSettings_Click(object sender, RoutedEventArgs e)
        {
            StylusSettings items = new StylusSettings();
            items.Owner = this;
            items.DrawingAttributes = this.Ink.DefaultDrawingAttributes;
            bool vr = items.ShowDialog().GetValueOrDefault();
            if (vr)
            {
                this.Ink.DefaultDrawingAttributes = items.DrawingAttributes;
            }
        }

        // Token: 0x060000A7 RID: 167 RVA: 0x000043C0 File Offset: 0x000025C0
        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            StylusSettings stylusSettings = new StylusSettings();
            stylusSettings.Owner = this;
            StrokeCollection selectedStrokes = this.Ink.GetSelectedStrokes();
            bool flag = selectedStrokes.Count > 0;
            if (flag)
            {
                stylusSettings.DrawingAttributes = selectedStrokes[0].DrawingAttributes;
            }
            else
            {
                stylusSettings.DrawingAttributes = this.Ink.DefaultDrawingAttributes;
            }
            bool valueOrDefault = stylusSettings.ShowDialog().GetValueOrDefault();
            if (valueOrDefault)
            {
                foreach (Stroke stroke in selectedStrokes)
                {
                    stroke.DrawingAttributes = stylusSettings.DrawingAttributes;
                }
            }
        }

        // Token: 0x14000001 RID: 1
        // (add) Token: 0x060000A8 RID: 168 RVA: 0x0000447C File Offset: 0x0000267C
        // (remove) Token: 0x060000A9 RID: 169 RVA: 0x000044B4 File Offset: 0x000026B4
        public event WorkerCompletedEventHandler InkPadCloseCompleted;

        // Token: 0x060000AA RID: 170 RVA: 0x000044EC File Offset: 0x000026EC
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
            bool flag = this.InkPadCloseCompleted != null;
            if (flag)
            {
                this.InkPadCloseCompleted(this, new EventArgs());
            }
        }

        // Token: 0x060000AB RID: 171 RVA: 0x00004520 File Offset: 0x00002720
        private Bitmap GetScreenImage(Rectangle rec)
        {
            Bitmap bitmap = new Bitmap(rec.Width, rec.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(rec.X, rec.Y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
            return bitmap;
        }
    }
}
