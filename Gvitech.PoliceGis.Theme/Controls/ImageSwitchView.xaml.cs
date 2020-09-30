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

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// ImageSwitchView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageSwitchView : BlankWindow
    {
        private List<ImageSource> _imgePathList;
        public List<ImageSource> ImagePathList
        {
            get { return _imgePathList ?? (_imgePathList = new List<ImageSource>()); }
            set { _imgePathList = value; }
        }

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(ImageSwitchView), new PropertyMetadata(string.Empty,OnTitleTextChanged));

        private static void OnTitleTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageSwitchView imageSwitchView)
            {
                imageSwitchView.TitleTextBlock.Text = e.NewValue.ToString();
            }
        }

        public int SelectIndex
        {
            get { return (int)GetValue(SelectIndexProperty); }
            set { SetValue(SelectIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectIndexProperty =
            DependencyProperty.Register("SelectIndex", typeof(int), typeof(ImageSwitchView), new PropertyMetadata(-1,OnSelectIndexChanged));

        private static void OnSelectIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is ImageSwitchView view)
            {
                int index = (int) e.NewValue;

                index = index < 0 ? 0 : index;

                if (view.ImagePathList.Count > 0 && index < view.ImagePathList.Count)
                {
                    view.PictureView.ImagePath = view.ImagePathList[index];
                }
            }
        }


        public ImageSwitchView()
        {
            InitializeComponent();
        }

        private void PreImage_Click(object sender, RoutedEventArgs e)
        {
            int preIndex = SelectIndex - 1;
            SelectIndex = preIndex < 0 ? 0 : preIndex;
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            int nextIndex = SelectIndex + 1;
            SelectIndex = nextIndex < ImagePathList.Count ? nextIndex : ImagePathList.Count - 1;
        }
    }
}
