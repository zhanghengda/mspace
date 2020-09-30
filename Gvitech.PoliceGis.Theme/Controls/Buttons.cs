using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mmc.Mspace.Theme.Controls
{
    public class Buttons : Button
    {
        public static readonly DependencyProperty PathDataProperty = DependencyProperty.RegisterAttached("PathData", typeof(Geometry), typeof(Buttons), new PropertyMetadata(null, PathDataChanged));
        public static readonly DependencyProperty HasPathDataProperty = DependencyProperty.RegisterAttached("HasPathData", typeof(bool), typeof(Buttons), new PropertyMetadata(false));
        public static readonly DependencyProperty MouseOverBackgroundProperty =
   DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.RoyalBlue));
        public static readonly DependencyProperty PressedBackgroundProperty =
   DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.DarkBlue));
        public static readonly DependencyProperty PressedForegroundProperty =
  DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty MouseOverForegroundProperty =
 DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty RIconProperty =
       DependencyProperty.Register("RIcon", typeof(ImageSource), typeof(Buttons), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for MouseOverRIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverRIconProperty =
            DependencyProperty.Register("MouseOverRIcon", typeof(ImageSource), typeof(Buttons), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for PressedRIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedRIconProperty =
            DependencyProperty.Register("PressedRIcon", typeof(ImageSource), typeof(Buttons), new PropertyMetadata(null));
        public static readonly DependencyProperty PressInnerPathFillProperty = DependencyProperty.Register("PressInnerPathFill", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.White));


        //是否需要启用选择按钮选中
        public static readonly DependencyProperty IsNeedSelectedProperty = DependencyProperty.RegisterAttached("IsNeedSelected", typeof(bool), typeof(Buttons), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(Buttons), new PropertyMetadata(false));
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached("GroupName", typeof(string), typeof(Buttons), new PropertyMetadata(""));
        private static Dictionary<string, List<Buttons>> Dic = new Dictionary<string, List<Buttons>>();
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Border border = this.GetTemplateChild("PART_Background") as Border;
            if (border == null) return;
            if ((bool)this.GetValue(IsNeedSelectedProperty))
            {
                border.MouseDown -= Border_MouseDown;
                border.MouseDown += Border_MouseDown;
            }
            string groupName = (string)this.GetValue(GroupNameProperty);
            if (!string.IsNullOrEmpty(groupName))
            {
                List<Buttons> listButtons = new List<Buttons>();
                if (Dic.ContainsKey(groupName))
                    listButtons = Dic[groupName];
                else
                    Dic.Add(groupName, listButtons);
                listButtons.Add(this);
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string groupName = (string)this.GetValue(GroupNameProperty);
            if (!string.IsNullOrEmpty(groupName) && Dic.ContainsKey(groupName))
            {
                List<Buttons> list = Dic[groupName];
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SetValue(IsSelectedProperty, false);
                }
            }
            this.SetValue(IsSelectedProperty, true);
            //this.Command.Execute(null);
        }

        private static void PathDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Geometry)
            {
                Buttons btn = d as Buttons;
                btn.SetValue(HasPathDataProperty, true);
            }
        }
        /// <summary>
        /// 内部Path的填充色
        /// </summary>
        public Brush InnerPathFill
        {
            get { return (Brush)GetValue(InnerPathFillProperty); }
            set { SetValue(InnerPathFillProperty, value); }
        }

        public static readonly DependencyProperty InnerPathFillProperty =
            DependencyProperty.Register("InnerPathFill", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// 鼠标滑动
        /// </summary>
        public Brush OverInnerPathFill
        {
            get { return (Brush)GetValue(OverInnerPathFillProperty); }
            set { SetValue(OverInnerPathFillProperty, value); }
        }

        public static readonly DependencyProperty OverInnerPathFillProperty =
            DependencyProperty.Register("OverInnerPathFill", typeof(Brush), typeof(Buttons), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// 按键选中时内部Path的填充色
        /// </summary>
        public Brush PressInnerPathFill
        {
            get { return (Brush)GetValue(PressInnerPathFillProperty); }
            set { SetValue(PressInnerPathFillProperty, value); }
        }

        public bool IsNeedSelected
        {
            get { return (bool)GetValue(IsNeedSelectedProperty); }
            set { SetValue(IsNeedSelectedProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        public Geometry PathData
        {
            get { return (Geometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public bool HasPathData
        {
            get { return (bool)GetValue(HasPathDataProperty); }
            set { SetValue(HasPathDataProperty, value); }
        }
        public ImageSource RIcon
        {
            get { return (ImageSource)GetValue(RIconProperty); }
            set { SetValue(RIconProperty, value); }
        }
        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        /// <summary>
        /// 鼠标进入背景样式
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }
        /// <summary>
        /// 鼠标进入前景样式
        /// </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }
        /// <summary>
        /// 鼠标按下前景样式（图标、文字）
        /// </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }
        public ImageSource MouseOverRIcon
        {
            get { return (ImageSource)GetValue(MouseOverRIconProperty); }
            set { SetValue(MouseOverRIconProperty, value); }
        }
        public ImageSource PressedRIcon
        {
            get { return (ImageSource)GetValue(PressedRIconProperty); }
            set { SetValue(PressedRIconProperty, value); }
        }
    }
}
