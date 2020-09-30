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
using Mmc.Mspace.UavModule.Views;

namespace Mmc.Mspace.UavModule.UavTracing
{
    /// <summary>
    /// MultiWebVideoView.xaml 的交互逻辑
    /// </summary>
    public partial class MultiVideoView
    {
        private List<Border> listVideoBorders;
        private List<SinggleVideoView> listVideoViews;
        private Dictionary<string, Border> dic = new Dictionary<string, Border>();
        
        private bool isFullScreen = false;
        private int CurrentColumn;
        private int CurrentRow;

        public MultiVideoView()
        {
            InitializeComponent();

            Loaded += MultiVideoView_Loaded;
        }

        private void MultiVideoView_Loaded(object sender, RoutedEventArgs e)
        {
            listVideoBorders = new List<Border>
            {
                bdLUp,bdRU1,bdRU2,
                bdRM1,bdRM2,
                bdLD1,bdLD2,bdRD1,bdRD2
            };

            listVideoViews = new List<SinggleVideoView>
            {
                LUpView,RUp1View,RUp2View,
                RM1View,RM2View,
                LDwon1View,LDwon2View,RDwon1View,RDwon2View
            };

            for (int i = 0; i < listVideoBorders.Count; i++)
            {
                var border = listVideoBorders[i];
                var view = listVideoViews[i];

                dic.Add(view.Name, border);
            }

            foreach (var singgleVideoView in listVideoViews)
            {
                singgleVideoView.VideoViewMouseDoubleClick += FullScreen;
            }
        }

        private void FullScreen(object sender)
        {
            var videoView = sender as SinggleVideoView;

            if (videoView != null)
            {
                var viewBorder = dic[videoView.Name];

                if (!isFullScreen)
                {
                    CurrentRow = Grid.GetRow(viewBorder);
                    CurrentColumn = Grid.GetColumn(viewBorder);

                    foreach (var border in listVideoBorders)
                    {
                        border.Visibility = Visibility.Collapsed;
                    }

                    Grid.SetColumn(viewBorder, 0);
                    Grid.SetColumnSpan(viewBorder, 4);
                    Grid.SetRow(viewBorder, 0);
                    Grid.SetRowSpan(viewBorder, 4);

                    viewBorder.Visibility = Visibility.Visible;
                    isFullScreen = true;
                }
                else
                {
                    if (CurrentColumn == 0 && CurrentRow == 1)
                    {
                        Grid.SetColumn(viewBorder, 0);
                        Grid.SetColumnSpan(viewBorder, 2);
                        Grid.SetRow(viewBorder, 1);
                        Grid.SetRowSpan(viewBorder, 2);
                    }
                    else
                    {
                        Grid.SetColumn(viewBorder, CurrentColumn);
                        Grid.SetColumnSpan(viewBorder, 1);
                        Grid.SetRow(viewBorder, CurrentRow);
                        Grid.SetRowSpan(viewBorder, 1);
                    }

                    foreach (var border in listVideoBorders)
                    {
                        border.Visibility = Visibility.Visible;
                    }

                    isFullScreen = false;
                }
            }
        }
    }
}
