using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// BatteryView.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryView : UserControl
    {
        public BatteryView()
        {
            InitializeComponent();
        }



        public float MaxPower
        {
            get { return (float)GetValue(MaxPowerProperty); }
            set { SetValue(MaxPowerProperty, value); }
        }

        public static readonly DependencyProperty MaxPowerProperty =
            DependencyProperty.Register("MaxPower", typeof(float), typeof(BatteryView), new PropertyMetadata(0.0f));

        public float MinPower
        {
            get { return (float)GetValue(MinPowerProperty); }
            set { SetValue(MinPowerProperty, value); }
        }

        public static readonly DependencyProperty MinPowerProperty =
            DependencyProperty.Register("MinPower", typeof(float), typeof(BatteryView), new PropertyMetadata(0.0f));

        public float CurrentPower
        {
            get { return (float)GetValue(CurrentPowerProperty); }
            set { SetValue(CurrentPowerProperty, value); }
        }

        public static readonly DependencyProperty CurrentPowerProperty =
            DependencyProperty.Register("CurrentPower", typeof(float), typeof(BatteryView), new PropertyMetadata(0.0f, CurrentPowerChanged));

        private static void CurrentPowerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var batteryView = d as BatteryView;
            var currentValue = (float)e.NewValue;

            if (batteryView != null)
            {
                var max = batteryView.MaxPower;
                var min = batteryView.MinPower;
                if (max > min)
                {
                    float ranges = max - min;
                    var scale = ranges * 0.2;

                    if (ranges > 0.0f)
                    {
                        batteryView.TextValue.Text = ((int)((currentValue - min)*100 / ranges)).ToString();
                    }

                    if (currentValue <= max && currentValue > max - scale)
                    {
                        batteryView.Image.Source = new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/Controls/Battery/battery5.png"));
                    }
                    else if (currentValue <= max - scale && currentValue > max - 2 * scale)
                    {
                        batteryView.Image.Source = new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/Controls/Battery/battery4.png"));
                    }
                    else if (currentValue <= max - 2 * scale && currentValue > max - 3 * scale)
                    {
                        batteryView.Image.Source = new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/Controls/Battery/battery3.png"));
                    }
                    else if (currentValue <= max - 3 * scale && currentValue > max - 4 * scale)
                    {
                        batteryView.Image.Source = new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/Controls/Battery/battery2.png"));
                    }
                    else if (currentValue <= max - 4 * scale)
                    {
                        batteryView.Image.Source = new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/Controls/Battery/battery1.png"));
                    }
                }
                else
                {
                    throw new Exception("请确定数据是否正确");
                }
            }
        }
    }
}
