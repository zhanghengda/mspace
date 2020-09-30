using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Wpf.Toolkit.Controls
{
    // Token: 0x02000014 RID: 20
    public class IconButton : Button
    {
        // Token: 0x0600003F RID: 63 RVA: 0x0000292C File Offset: 0x00000B2C
        static IconButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000040 RID: 64 RVA: 0x000029B0 File Offset: 0x00000BB0
        // (set) Token: 0x06000041 RID: 65 RVA: 0x000029D2 File Offset: 0x00000BD2
        public string Icon
        {
            get
            {
                return (string)base.GetValue(IconButton.IconProperty);
            }
            set
            {
                base.SetValue(IconButton.IconProperty, value);
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000042 RID: 66 RVA: 0x000029E4 File Offset: 0x00000BE4
        // (set) Token: 0x06000043 RID: 67 RVA: 0x00002A06 File Offset: 0x00000C06
        public string MouseOverIcon
        {
            get
            {
                return (string)base.GetValue(IconButton.MouseOverIconProperty);
            }
            set
            {
                base.SetValue(IconButton.MouseOverIconProperty, value);
            }
        }

        // Token: 0x0400000F RID: 15
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconButton), new PropertyMetadata());

        // Token: 0x04000010 RID: 16
        public static readonly DependencyProperty MouseOverIconProperty = DependencyProperty.Register("MouseOverIcon", typeof(string), typeof(IconButton), new PropertyMetadata());





        public string PressedOverIcon
        {
            get
            {
                return (string)base.GetValue(IconButton.PressedOverIconProperty);
            }
            set
            {
                base.SetValue(IconButton.PressedOverIconProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for PressedOverIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedOverIconProperty =
            DependencyProperty.Register("PressedOverIcon", typeof(string), typeof(IconButton), new PropertyMetadata());

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Border border = this.GetTemplateChild("PART_Background") as Border;
            if (border == null) return;

            border.MouseDown -= Border_MouseDown;
            border.MouseDown += Border_MouseDown;

        }
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.SetValue(IsSelectedProperty, !IsSelected);
            //this.Command.Execute(null);
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(IconButton), new PropertyMetadata(OnIsSelectedChanged));


        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
