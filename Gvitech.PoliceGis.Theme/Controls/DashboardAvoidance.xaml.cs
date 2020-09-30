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
    /// DashboardAvoidance.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardAvoidance : UserControl
    {
        /// <summary>
        /// 动画时间(毫秒)
        /// </summary>
        public static int AnimationInterval = 1000;

        public DashboardAvoidance()
        {
            InitializeComponent();
        }



        public int Direction
        {
            get { return (int)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Direction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(int), typeof(DashboardAvoidance), new PropertyMetadata(0,OnDirectionChanged));

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
