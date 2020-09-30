namespace Mmc.Mspace.ToolModule.AlarmStatisticLayerController
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Markup;

    namespace Mmc.Mspace.ToolModule.LayerController
    {
        public class StatisticLegened : Window, IComponentConnector
        {
            public StatisticLegened()
            {
                this.InitializeComponent();
                base.Loaded += this.StatisticLegened_Loaded;
            }

            private void StatisticLegened_Loaded(object sender, RoutedEventArgs e)
            {
                this.SetWindowLocation();
            }

            private void SetWindowLocation()
            {
                base.WindowStartupLocation = WindowStartupLocation.Manual;
                base.Top = SystemParameters.WorkArea.Height - base.ActualHeight - 65.0;
                base.Left = 20.0;
            }

            [DebuggerNonUserCode]
            [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
            public void InitializeComponent()
            {
                bool contentLoaded = this._contentLoaded;
                if (!contentLoaded)
                {
                    this._contentLoaded = true;
                    Uri resourceLocator = new Uri("/Mmc.Mspace.ToolModule;component/alarmstatisticlayercontroller/statisticlegened.xaml", UriKind.Relative);
                    Application.LoadComponent(this, resourceLocator);
                }
            }

            [DebuggerNonUserCode]
            [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
            [EditorBrowsable(EditorBrowsableState.Never)]
            void IComponentConnector.Connect(int connectionId, object target)
            {
                this._contentLoaded = true;
            }

            private bool _contentLoaded;
        }
    }
}