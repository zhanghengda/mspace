using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Mmc.Mspace.PoiManagerModule
{
    /// <summary>
    /// PoiDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class MakerLineView 
    {
        DispatcherTimer timer;
        public MakerLineView()
        {
            InitializeComponent();
            this.HeightTypesBox.Items.Clear();
            this.Unloaded += MakerLineView_Unloaded;
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void MakerLineView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer != null)
                timer.Stop();
        }

        private void BuildDetailView_Activated(object sender, EventArgs e)
        {
        }

        private void BuildDetailView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }


        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!(e.Source is System.Windows.Controls.Image))
                    base.DragMove();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void poitypesBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
        int i = 0;
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            i += 1;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };
            timer.IsEnabled = true;
            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                var vm = this.DataContext as MakerLineViewModel;

                vm.DisplayImgCommand.Execute(null);
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            var vm = this.DataContext as PoiBaseViewModel;
            vm.StyleColor = ColorPicker.SelectedColor;
        }
    }
}