using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// <see cref="System.Windows.Controls.TreeViewItem"/> 的扩展方法。
    /// </summary>
    public static class TreeViewItemExt
    {
        /// <summary>
        /// 返回指定 <see cref="System.Windows.Controls.TreeViewItem"/> 的深度。
        /// </summary>
        /// <param name="item">要获取深度的 <see cref="System.Windows.Controls.TreeViewItem"/> 对象。</param>
        /// <returns><see cref="System.Windows.Controls.TreeViewItem"/> 所在的深度。</returns>
        public static int GetDepth(this TreeViewItem item)
        {
            int depth = 0;
            while ((item = item.GetAncestor<TreeViewItem>()) != null)
            {
                depth++;
            }
            return depth;
        }

        public static DependencyObject VisualUpwardSearch<M>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(M))
            {
                if (source is Visual || source is Visual3D)
                    source = VisualTreeHelper.GetParent(source);
                else
                    source = LogicalTreeHelper.GetParent(source);
            }
            return source;
        }
    }
}
