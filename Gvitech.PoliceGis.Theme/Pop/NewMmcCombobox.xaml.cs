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

namespace Mmc.Mspace.Theme.Pop
{
    /// <summary>
    /// NewMmcCombobox.xaml 的交互逻辑
    /// </summary>
    public partial class NewMmcCombobox : UserControl
    {
        public NewMmcCombobox()
        {
            InitializeComponent();
        }
        //选中菜单事件
        public static readonly DependencyProperty SelectedMenuCommandProperty = DependencyProperty.Register("SelectedMenuCommand", typeof(ICommand), typeof(NewMmcCombobox));
        //public static readonly DependencyProperty DataContextSourceProperty = DependencyProperty.Register("DataContextSource", typeof(object), typeof(TYCombox));
        public static readonly DependencyProperty DataContextSourceProperty = DependencyProperty.Register("DataContextSource", typeof(object), typeof(NewMmcCombobox), new PropertyMetadata(null, ContextChanged));


        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(NewMmcCombobox), new PropertyMetadata(string.Empty));




        public Brush ComboxBackground
        {
            get { return (Brush)GetValue(ComboxBackgroundProperty); }
            set { SetValue(ComboxBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComboxBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboxBackgroundProperty =
            DependencyProperty.Register("ComboxBackground", typeof(Brush), typeof(NewMmcCombobox), new PropertyMetadata(Brushes.Transparent));



        public ImageSource DataPath
        {
            get { return (ImageSource)GetValue(DataPathProperty); }
            set { SetValue(DataPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataPathProperty =
            DependencyProperty.Register("DataPath", typeof(ImageSource), typeof(NewMmcCombobox), new PropertyMetadata(null));

        public ImageSource PressDataPath
        {
            get { return (ImageSource)GetValue(PressDataPathProperty); }
            set { SetValue(PressDataPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressDataPathProperty =
            DependencyProperty.Register("PressDataPath", typeof(ImageSource), typeof(NewMmcCombobox), new PropertyMetadata(null));

        public ImageSource HoverDataPath
        {
            get { return (ImageSource)GetValue(HoverDataPathProperty); }
            set { SetValue(HoverDataPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverDataPathProperty =
            DependencyProperty.Register("HoverDataPath", typeof(ImageSource), typeof(NewMmcCombobox), new PropertyMetadata(null));

        public ICommand SelectedMenuCommand
        {
            get { return (ICommand)GetValue(SelectedMenuCommandProperty); }
            set { SetValue(SelectedMenuCommandProperty, value); }
        }

        public object DataContextSource
        {
            get { return (object)GetValue(DataContextSourceProperty); }
            set { SetValue(DataContextSourceProperty, value); }
        }

        private static void ContextChanged(System.Windows.DependencyObject d, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;
            NewMmcCombobox control = (NewMmcCombobox)d;
            control.myComBox.DataContext = e.NewValue;
            //throw new NotImplementedException();
        }


        private void MmcComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            MmcComboboxData data = (MmcComboboxData)e.AddedItems[0];
            if (SelectedMenuCommand != null)
            {
                SelectedMenuCommand.Execute(data);
            }
            this.myComBox.SelectedValue = null;
        }
    }
}