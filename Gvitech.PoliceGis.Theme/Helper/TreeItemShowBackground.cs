using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mmc.Mspace.Theme.Helper
{
    public class TreeItemShowBackground
    {
        public static bool GetShowBackground(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowBackgroundProperty);
        }

        public static void SetShowBackground(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowBackgroundProperty, value);
        }
        public static readonly DependencyProperty ShowBackgroundProperty = DependencyProperty.RegisterAttached(
            "ShowBackground",
            typeof(bool),
            typeof(TreeItemShowBackground),
            new PropertyMetadata(OnShowBackground)
        );


        static List<TreeViewItem> _LastItems = new List<TreeViewItem>();
         private static void OnShowBackground(DependencyObject d, DependencyPropertyChangedEventArgs e)
         {
             var item = d as TreeViewItem;
 
             if (item == null || e.NewValue is bool == false || item.Background is SolidColorBrush == false) return;
 
             if ((bool) e.NewValue == true)
             {
 
                 if (_LastItems.Count == 0)
                 {
                     item.Background = new SolidColorBrush(Colors.LawnGreen);
                 }
                 else
                 {
                     if (_LastItems.Last().Parent != item && item != _LastItems[0])
                     {
                         _LastItems[0].Background = new SolidColorBrush(Colors.Transparent);
 
                         item.Background = new SolidColorBrush(Colors.LawnGreen);
                         _LastItems.Insert(0, item);
                     }
                 }
                 if (_LastItems.Contains(item) == false)
                 {
                     _LastItems.Add(item);
                 }
             }
             else
             {
                 if (_LastItems.FirstOrDefault() == item)
                 {
                     item.Background = new SolidColorBrush(Colors.Transparent);
                     _LastItems.Remove(item);
                     if (_LastItems.Count > 0)
                     {
                         _LastItems[0].Background = new SolidColorBrush(Colors.LawnGreen);
                     }
                 }
             }
         }
    }
}
