using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Wpf.Toolkit.Controls
{
    public class SimpleIconButton : Button
    {
        static SimpleIconButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleIconButton), new FrameworkPropertyMetadata(typeof(SimpleIconButton)));
        }

        public string Icon
        {
            get
            {
                return (string)base.GetValue(SimpleIconButton.IconProperty);
            }
            set
            {
                base.SetValue(SimpleIconButton.IconProperty, value);
            }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(SimpleIconButton), new PropertyMetadata());

    }
}
