using Mmc.Windows.Framework.Commands;
using Mmc.Windows.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Controls
{
	public class SearchTextBox : TextBox
	{
		public static readonly DependencyProperty TipTextProperty;

		public static readonly DependencyProperty IsShowIconProperty;

		public static readonly DependencyProperty IsShowClearTextButtonProperty;

		public static readonly DependencyProperty IsShowSoftKeyBoardProperty;

		public static readonly DependencyProperty ClearTextCmdProperty;

		public string TipText
		{
			get
			{
				return (string)base.GetValue(SearchTextBox.TipTextProperty);
			}
			set
			{
				base.SetValue(SearchTextBox.TipTextProperty, value);
			}
		}

		public bool IsShowIcon
		{
			get
			{
				return (bool)base.GetValue(SearchTextBox.IsShowIconProperty);
			}
			set
			{
				base.SetValue(SearchTextBox.IsShowIconProperty, value);
			}
		}

		public bool IsShowClearTextButton
		{
			get
			{
				return (bool)base.GetValue(SearchTextBox.IsShowClearTextButtonProperty);
			}
			set
			{
				base.SetValue(SearchTextBox.IsShowClearTextButtonProperty, value);
			}
		}

		public bool IsShowSoftKeyBoard
		{
			get
			{
				return (bool)base.GetValue(SearchTextBox.IsShowSoftKeyBoardProperty);
			}
			set
			{
				base.SetValue(SearchTextBox.IsShowSoftKeyBoardProperty, value);
			}
		}

		public ICommand ClearTextCmd
		{
			get
			{
				return (ICommand)base.GetValue(SearchTextBox.ClearTextCmdProperty);
			}
			set
			{
				base.SetValue(SearchTextBox.ClearTextCmdProperty, value);
			}
		}

		static SearchTextBox()
		{
			SearchTextBox.TipTextProperty = DependencyProperty.Register("TipText", typeof(string), typeof(SearchTextBox), new PropertyMetadata());
			SearchTextBox.IsShowIconProperty = DependencyProperty.Register("IsShowIcon", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(true));
			SearchTextBox.IsShowClearTextButtonProperty = DependencyProperty.Register("IsShowClearTextButton", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(false));
			SearchTextBox.IsShowSoftKeyBoardProperty = DependencyProperty.Register("IsShowSoftKeyBoard", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(true));
			SearchTextBox.ClearTextCmdProperty = DependencyProperty.Register("ClearTextCmd", typeof(ICommand), typeof(SearchTextBox), new PropertyMetadata());
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox), new FrameworkPropertyMetadata(typeof(SearchTextBox)));
		}

		public SearchTextBox()
		{
            ClearTextCmd = new RelayCommand(() => { Text = null; });
        }

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.IsShowSoftKeyBoard)
			{
				SoftKeyBoard.Show();
			}
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			if (this.IsShowSoftKeyBoard)
			{
				SoftKeyBoard.Hide();
			}
		}
	}
}
