using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;

namespace Mmc.Wpf.Toolkit.Controls
{
	// Token: 0x0200001A RID: 26
	public class SearchTextBox : TextBox
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002F68 File Offset: 0x00001168
		static SearchTextBox()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox), new FrameworkPropertyMetadata(typeof(SearchTextBox)));
		}

        // Token: 0x06000065 RID: 101 RVA: 0x00003074 File Offset: 0x00001274
        public SearchTextBox()
        {
            ClearTextCmd = new RelayCommand(() => { Text = null; });
        }

        // Token: 0x06000066 RID: 102 RVA: 0x00003098 File Offset: 0x00001298
        protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			bool isShowSoftKeyBoard = this.IsShowSoftKeyBoard;
			if (isShowSoftKeyBoard)
			{
				SoftKeyBoard.Show();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030C0 File Offset: 0x000012C0
		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			bool isShowSoftKeyBoard = this.IsShowSoftKeyBoard;
			if (isShowSoftKeyBoard)
			{
				SoftKeyBoard.Hide();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030E8 File Offset: 0x000012E8
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000310A File Offset: 0x0000130A
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000311C File Offset: 0x0000131C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000313E File Offset: 0x0000133E
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003176 File Offset: 0x00001376
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000318C File Offset: 0x0000138C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000031AE File Offset: 0x000013AE
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000031C4 File Offset: 0x000013C4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000031E6 File Offset: 0x000013E6
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

	
		public static readonly DependencyProperty TipTextProperty = DependencyProperty.Register("TipText", typeof(string), typeof(SearchTextBox), new PropertyMetadata());

		// Token: 0x0400001F RID: 31
		public static readonly DependencyProperty IsShowIconProperty = DependencyProperty.Register("IsShowIcon", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(true));

		// Token: 0x04000020 RID: 32
		public static readonly DependencyProperty IsShowClearTextButtonProperty = DependencyProperty.Register("IsShowClearTextButton", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(false));

		// Token: 0x04000021 RID: 33
		public static readonly DependencyProperty IsShowSoftKeyBoardProperty = DependencyProperty.Register("IsShowSoftKeyBoard", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(true));

		// Token: 0x04000022 RID: 34
		public static readonly DependencyProperty ClearTextCmdProperty = DependencyProperty.Register("ClearTextCmd", typeof(ICommand), typeof(SearchTextBox), new PropertyMetadata());
	}
}
