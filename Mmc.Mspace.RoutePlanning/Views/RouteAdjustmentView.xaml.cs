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
using GFramework.BlankWindow;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Mspace.RoutePlanning.Views
{
    /// <summary>
    /// RouteAdjustmentView.xaml 的交互逻辑
    /// </summary>
    public partial class RouteAdjustmentView : BlankWindow
    {
        /// <summary>
        /// arg1:height arg2:interval
        /// </summary>
        public Action<double,double> tbOnTextChanged;

        public Action<double, double> OnConfirmAction;
        public Action OnCancelAction;

        private double _height = 3;
        private double _interval = 1;

        public RouteAdjustmentView()
        {
            InitializeComponent();
        }

        private void TbInterval_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbInterval.Text))
            {
                if (double.TryParse(tbInterval.Text, out double interval))
                {

                    if (interval > 50)
                    {
                        tbInterval.Text = "50";
                        interval = 50;
                    }

                    if (interval < 1)
                    {
                        tbInterval.Text = "1";
                        interval = 1;
                    }

                    _interval = interval;


                    tbOnTextChanged?.Invoke(_height, _interval);
                }
                else
                {
                    Messages.ShowMessage("请输入正确的数值!");
                }
            }
        }

        private void TbHeight_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbHeight.Text))
            {
                if (double.TryParse(tbHeight.Text, out double height))
                {
                    if (height > 20)
                    {
                        tbHeight.Text = "20";
                        height = 20;
                    }

                    if (height < 1)
                    {
                        tbHeight.Text = "1";
                        height = 1;
                    }

                    _height = height;

                    tbOnTextChanged?.Invoke(_height, _interval);
                }
                else
                {
                    Messages.ShowMessage("请输入正确的数值!");
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            OnConfirmAction?.Invoke(_height + 10, _interval);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancelAction?.Invoke();
            this.Close();
        }
    }
}
