using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mmc.Mspace.Theme.Helper
{
    public class TextBoxHelper
    {
        public static readonly DependencyProperty IsMonitoringProperty = DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(TextBoxHelper), new UIPropertyMetadata(false, OnIsMonitoringChanged));
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(TextBoxHelper), new UIPropertyMetadata(string.Empty));
        public static readonly DependencyProperty PasswordTextProperty = DependencyProperty.RegisterAttached("PasswordText", typeof(string), typeof(TextBoxHelper), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.RegisterAttached("TextBoxText", typeof(string), typeof(TextBoxHelper), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(TextBoxHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SelectAllOnFocusProperty = DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false));

        public static readonly DependencyProperty TextBoxEditProperty = DependencyProperty.RegisterAttached("TextBoxEdit", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false, OnTextBoxEditChanged));
        //textbox内部按钮触发父亲窗口命令
        public static readonly DependencyProperty ButtonCommandProperty = DependencyProperty.RegisterAttached("ButtonCommand", typeof(ICommand), typeof(TextBoxHelper), new PropertyMetadata(null));
        public static readonly DependencyProperty ButtonCommandParameterProperty = DependencyProperty.RegisterAttached("ButtonCommandParameter", typeof(object), typeof(TextBoxHelper), new PropertyMetadata(null));

        public static readonly DependencyProperty IsNeedClearButtonProperty = DependencyProperty.RegisterAttached("IsNeedClearButton", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false));

        public static readonly DependencyProperty IsVisibilityImageProperty = DependencyProperty.RegisterAttached("IsVisibilityImage", typeof(Visibility), typeof(TextBoxHelper), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty ImageSourcePathProperty = DependencyProperty.RegisterAttached("ImageSourcePath", typeof(ImageSource), typeof(TextBoxHelper), new PropertyMetadata(null));

        public static readonly DependencyProperty RegisterClearButtonProperty = DependencyProperty.RegisterAttached("RegisterClearButton", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false, RegisterButtonClearChanged));

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(TextBoxHelper),
                new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            switch (d.GetType().Name.ToLower())
            {
                case "textbox":
                    var textbox = d as TextBox;
                    if ((bool)e.NewValue)
                    {
                        textbox.TextChanged += textbox_TextChanged;
                        textbox.GotFocus += textbox_GotFocus;
                        textbox.Unloaded += Textbox_Unloaded;
                        textbox.Dispatcher.BeginInvoke(new Action(() => { textbox_TextChanged(textbox, new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None)); }));
                    }
                    else
                    {
                        textbox.TextChanged -= textbox_TextChanged;
                        textbox.GotFocus -= textbox_GotFocus;
                    }
                    break;
                case "passwordbox":
                    var passwordbox = d as PasswordBox;
                    if ((bool)e.NewValue)
                    {
                        passwordbox.PasswordChanged += Passwordbox_PasswordChanged;
                    }
                    else
                    {
                        passwordbox.PasswordChanged -= Passwordbox_PasswordChanged;
                    }
                    break;
            }
        }

        private static void Passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null)
            {
                return;
            }
            SetPasswordLength(pb, pb.Password.Length);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        private static void Textbox_Unloaded(object sender, RoutedEventArgs e)
        {
            switch (sender.GetType().Name.ToLower())
            {
                case "textbox":
                    var textbox = sender as TextBox;
                    //textbox.Text = null;
                    break;
            }
        }
        private static void OnTextBoxEditChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            switch (d.GetType().Name.ToLower())
            {
                case "textbox":
                    var textbox = d as TextBox;
                    if (GetTextBoxEdit(textbox))
                    {
                        textbox.Focus();
                        textbox.SelectAll();
                        textbox.SelectionStart = 0;
                        textbox.SelectionLength = textbox.Text.Length;
                    }
                    break;
            }
        }
        private static void RegisterButtonClearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button button = d as Button;
            if (button != null && (bool)e.NewValue)
            {
                button.Click += btn_Click;
            }
            //throw new NotImplementedException();
        }

        static void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var parent = VisualTreeHelper.GetParent(btn);
            while (!(parent is TextBox || parent is PasswordBox || parent is ComboBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            //处理按钮带处理事件的情形
            ICommand command = GetButtonCommand(parent);
            if (command != null)
            {
                object para = GetButtonCommandParameter(parent);
                command.Execute(para);
            }
            if (parent is TextBox)
            {
                if (GetIsNeedClearButton(parent))//有清空按钮的时候，需要清空数据
                {
                    TextBox tbx = (TextBox)parent;
                    tbx.SetValue(TextBox.TextProperty, null);
                }
            }
            //throw new NotImplementedException();
        }

        static void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textboxtext = sender as TextBox;
            ControlGotFocus(sender as TextBox, textbox => textbox.SelectAll());
        }

        static void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            SetTextLength(tbx, textbox => textbox.Text.Length);
            if (tbx.Text == "")
                tbx.SetValue(TextBox.TextProperty, null);
            //tbx.Text = null;
            SetTextBoxText(tbx, tbx.Text);
        }

        private static void SetTextLength<TDependencyObject>(TDependencyObject sender, Func<TDependencyObject, int> func) where TDependencyObject : DependencyObject
        {
            if (sender != null)
            {
                int length = func(sender);
                sender.SetValue(HasTextProperty, length > 0);
            }
        }
        private static void ControlGotFocus<TDependencyObject>(TDependencyObject sender, Action<TDependencyObject> Ac) where TDependencyObject : DependencyObject
        {
            if (sender != null)
            {
                TextBox textbox;
                if (GetSelectAllOnFocus(sender))
                {
                    sender.Dispatcher.BeginInvoke(Ac, sender);
                }
                textbox = sender as TextBox;
            }
        }



        static void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static ImageSource GetImageSourcePath(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageSourcePathProperty);
        }

        public static void SetImageSourcePath(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageSourcePathProperty, value);
        }
        public static Visibility GetIsVisibilityImage(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(IsVisibilityImageProperty);
        }
        public static void SetIsVisibilityImage(DependencyObject obj, Visibility value)
        {
            obj.SetValue(IsVisibilityImageProperty, value);
        }

        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }
        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }
        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }
        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextProperty, value);
        }

        public static bool GetSelectAllOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllOnFocusProperty);
        }
        public static void SetSelectAllOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnFocusProperty, value);
        }

        public static bool GetTextBoxEdit(DependencyObject obj)
        {
            return (bool)obj.GetValue(TextBoxEditProperty);
        }
        public static void SetTextBoxEdit(DependencyObject obj, bool value)
        {
            obj.SetValue(TextBoxEditProperty, value);
        }

        public static ICommand GetButtonCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ButtonCommandProperty);
        }

        public static void SetButtonCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ButtonCommandProperty, value);
        }

        public static bool GetIsNeedClearButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNeedClearButtonProperty);
        }

        public static void SetIsNeedClearButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNeedClearButtonProperty, value);
        }

        public static object GetButtonCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(ButtonCommandParameterProperty);
        }

        public static void SetButtonCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonCommandParameterProperty, value);
        }

        public static string GetPasswordText(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordTextProperty);
        }

        public static void SetPasswordText(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordTextProperty, value);
        }

        public static string GetTextBoxText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextBoxTextProperty);
        }

        public static void SetTextBoxText(DependencyObject obj, string value)
        {
            obj.SetValue(TextBoxTextProperty, value);
        }

        public static bool GetRegisterClearButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(RegisterClearButtonProperty);
        }

        public static void SetRegisterClearButton(DependencyObject obj, bool value)
        {
            obj.SetValue(RegisterClearButtonProperty, value);
        }

    }

}
