using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.StatisticService;
using Mmc.Mspace.ToolModule.AlarmStatisticLayerController;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Toolkit.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.ToolModule.LayerController
{
    public class ToggleLayerControllerBase : ToolItemModel
    {
        private static bool isRegisted;

        private List<LayerItemModel> items = new List<LayerItemModel>();

        [XmlIgnore]
        public static FrameworkElement LegenedView { get; set; }

        public static FrameworkElement StatisticView { get; set; }

        [XmlIgnore]
        public ICommand CheckCommand { get; set; }
        public List<LayerItemModel> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this.items, value, "Items");
            }
        }

        public override FrameworkElement CreatedRightView()
        {
            return new StatisticsView();
        }

        public override FrameworkElement CreatedView()
        {
            bool flag = !IEnumerableExtension.HasValues<LayerItemModel>(this.Items);
            if (flag)
            {
                this.Items = ServiceManager.GetService<IStatisticLayerService>(null).GetStatisticLayers();
            }
            ToggleLayersView toggleLayersView = new ToggleLayersView();
            IconPopupButton iconPopupButton = toggleLayersView.FindName("layers") as IconPopupButton;
            bool flag2 = iconPopupButton != null;
            if (flag2)
            {
                iconPopupButton.Placement = PlacementMode.Right;
            }
            return toggleLayersView;
        }

        public override void Initialize()
        {
            base.ViewType = (ViewType)5;
            base.Initialize();
            this.Items.Clear();
            this.CheckCommand = new RelayCommand(delegate (object p)
            {
                LayerItemModel layerItemModel = (LayerItemModel)p;
                Chromatography lengend = ServiceManager.GetService<IStatisticLayerService>(null).GetLengend(layerItemModel.Name.ToString());
                bool flag = ToggleLayerControllerBase.LegenedView == null;
                if (flag)
                {
                    ToggleLayerControllerBase.LegenedView = new StatisticLegened
                    {
                        Owner = Application.Current.MainWindow
                    };
                }
                ToggleLayerControllerBase.LegenedView.DataContext = lengend;
                ToggleLayerControllerBase.LegenedView.Visibility = (layerItemModel.IsVisible ? Visibility.Visible : Visibility.Collapsed);
                bool isVisible = layerItemModel.IsVisible;
                if (isVisible)
                {
                    this.RegisterSelectXQ();
                }
                else
                {
                    this.UnRegisterSelectXQ();
                    bool flag2 = ToggleLayerControllerBase.StatisticView != null;
                    if (flag2)
                    {
                        ToggleLayerControllerBase.StatisticView.Visibility = Visibility.Collapsed;
                    }
                }
                this.TopView();
            });
        }

        public override void Reset()
        {
            base.Reset();
            bool flag = IEnumerableExtension.HasValues<LayerItemModel>(this.Items);
            if (flag)
            {
                this.Items.ForEach(delegate (LayerItemModel item)
                {
                    item.IsVisible = false;
                });
            }
            this.UnRegisterSelectXQ();
            bool flag2 = ToggleLayerControllerBase.LegenedView != null;
            if (flag2)
            {
                ToggleLayerControllerBase.LegenedView.Visibility = Visibility.Collapsed;
            }
            bool flag3 = ToggleLayerControllerBase.StatisticView != null;
            if (flag3)
            {
                ToggleLayerControllerBase.StatisticView.Visibility = Visibility.Collapsed;
            }
        }

        private void RegisterSelectXQ()
        {
            bool flag = !ToggleLayerControllerBase.isRegisted;
            if (flag)
            {
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractSelect;
                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                GviMap.AxMapControl.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClickSelect);
                ToggleLayerControllerBase.isRegisted = true;
            }
        }

        private void RenderControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            bool flag = PickResult == null;
            if (!flag)
            {
                bool flag2 = PickResult.Type != gviObjectType.gviObjectFeatureLayer;
                if (!flag2)
                {
                    IFeatureLayerPickResult featureLayerPickResult = PickResult as IFeatureLayerPickResult;
                    bool flag3 = featureLayerPickResult.FeatureId < 0;
                    if (!flag3)
                    {
                        IDisplayLayer disPlayLayerByFCGuid = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCGuid(featureLayerPickResult.FeatureLayer.FeatureClassId.ToString());
                        bool flag4 = disPlayLayerByFCGuid == null || disPlayLayerByFCGuid.Fc == null;
                        if (!flag4)
                        {
                            IRowBuffer row = disPlayLayerByFCGuid.Fc.GetRow(featureLayerPickResult.FeatureId);
                            string policement = StringExtension.ParseTo<string>(row.GetValue(row.Fields.IndexOf("MC")), null);
                            AlarmStatisticalChart chartData = ServiceManager.GetService<IStatisticLayerService>(null).GetChartData(policement);
                            bool flag5 = ToggleLayerControllerBase.StatisticView == null;
                            if (flag5)
                            {
                                ToggleLayerControllerBase.StatisticView = new StatisticsView
                                {
                                    Owner = Application.Current.MainWindow
                                };
                            }
                            ToggleLayerControllerBase.StatisticView.DataContext = chartData;
                            ToggleLayerControllerBase.StatisticView.Visibility = Visibility.Visible;
                            FdeCoreExtension.ReleaseComObject(row);
                        }
                    }
                }
            }
        }

        private void TopView()
        {
            IVector3 vector;
            IEulerAngle eulerAngle;
            GviMap.AxMapControl.Camera.GetCamera(out vector, out eulerAngle);
            vector.Z = 100000.0;
            eulerAngle.Tilt = -90.0;
            GviMap.AxMapControl.Camera.SetCamera(vector, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
        }
        private void UnRegisterSelectXQ()
        {
            bool flag = ToggleLayerControllerBase.isRegisted;
            if (flag)
            {
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                GviMap.AxMapControl.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClickSelect);
                ToggleLayerControllerBase.isRegisted = false;
            }
        }
    }
}