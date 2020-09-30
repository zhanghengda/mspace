using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Mmc.Mspace.Theme.Pop
{
   public class MmcComboboxData:DependencyObject
    {

        public string Title
        {
            get { return (string)GetValue(_TitleProperty); }
            set { SetValue(_TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MmcComboboxData), new PropertyMetadata(null));


        public ImageSource ImagePath { get; set; }
        public string NavId { get; set; }

        public string NavMethodName { get; set; }
    }
}
