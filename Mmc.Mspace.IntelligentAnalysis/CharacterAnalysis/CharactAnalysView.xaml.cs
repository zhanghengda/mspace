using Mmc.Windows.Services;
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
using System.Windows.Shapes;

namespace Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis
{
    /// <summary>
    /// CharactAnalysView.xaml 的交互逻辑
    /// </summary>
    public partial class CharactAnalysView 
    {
        public CharactAnalysView()
        {
            InitializeComponent();
            analyseBox.Items.Clear();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!(e.Source is System.Windows.Controls.Image))
                    base.DragMove();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }
}
