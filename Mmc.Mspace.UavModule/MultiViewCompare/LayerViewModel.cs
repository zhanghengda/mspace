using Gvitech.CityMaker.FdeGeometry;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.ToolModule.LayerController;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Mspace.PoiManagerModule.Dto;
using System;
using System.Collections.Generic;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.UavModule.MultiViewCompare
{
    public class LayerViewModel : LayerItemModel
    {
        public string Content { get; set; }

        public LayerViewModel(int viewPortIndex)
        {
            this.ViewPortIndex = viewPortIndex;
            // init();
        }

        public int ViewPortIndex { get; set; }

        /// <summary>
        /// 影像图层组
        /// </summary>
        public GroupLayerItemModel ImgGroupLayer { get; set; }

        /// <summary>
        /// 三维瓦片图层组
        /// </summary>
        public GroupLayerItemModel TileGroupLayer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GroupLayerItemModel PoiGroupLayer { get; set; }

        public void init()
        {
            try
            {
                //List<IDisplayLayer> showLayers = ServiceManager.GetService<IDataBaseService>(null).GetShowLayers();
                //bool flag = IEnumerableExtension.HasValues<IDisplayLayer>(showLayers);
                //if (flag)
                //{
                //    showLayers.ForEach(delegate (IDisplayLayer item)
                //    {
                //        IFeatureLayer featureLayer = item.FLyers.FirstOrDefault<IFeatureLayer>();
                //        base.Items.Add(new ActualLayerItem
                //        {
                //            Name = (string.IsNullOrEmpty(item.AliasName) ? item.Name : item.AliasName),
                //            Parameters = item.FLyers,
                //            IsVisible = (featureLayer != null && gviViewportMaskExtension.GetIsVisible(featureLayer.VisibleMask))
                //        });
                //    });
                //}
                //base.Items.Add(new TerrainLayerItem
                //{
                //    Name = "地形",
                //    IsVisible = gviViewportMaskExtension.GetIsVisible(GviMap.MapControl.Terrain.VisibleMask)
                //});

                //获取影像数据
                var imageLayers = DataBaseService.Instance.GetImageLayers();

                var imgGroupLayer = new GroupLayerItemModel
                {
                    Name = "影像",
                    Group = "影像"
                };
                foreach (var item in imageLayers)
                {
                    imgGroupLayer.Children.Add(new RenderableItem
                    //Items.Add(new RenderableItem
                    {
                        Name = item.Name,
                        LayerType = RenderLayerType.TileLayer,
                        Parameters = new RenderableViewPort
                        {
                            ViewPort = this.ViewPortIndex,
                            Renderable = item.Layer
                        },
                        IsVisible = item.Layer.VisibleMask.GetIsVisible()
                    });
                }
                this.ImgGroupLayer = imgGroupLayer;
                //Children.Add(imgGroupLayer);

                //获取瓦片数据
                var tileLayers = DataBaseService.Instance.GetTileLayers();
                var tileGroupLayer = new GroupLayerItemModel
                {
                    Name = "三维瓦片",
                    Group = "三维瓦片"
                };

                foreach (var item in tileLayers)
                {
                    tileGroupLayer.Children.Add(new RenderableItem
                    //Items.Add(new RenderableItem
                    {
                        Name = item.Name,
                        LayerType = RenderLayerType.TileLayer,
                        Parameters = new RenderableViewPort
                        {
                            ViewPort = this.ViewPortIndex,
                            Renderable = item.Layer
                        },
                        IsVisible = item.Layer.VisibleMask.GetIsVisible()
                    });
                }
                this.TileGroupLayer = tileGroupLayer;
                // Children.Add(tileGroupLayer);

              


              
               


                //标注
                var poiLayerRoot = new GroupLayerItemModel
                {
                    Name = "标注",
                    Group = "标注"
                };
                var tags = GviMap.PoiManager.GetPoiTags();
                foreach (var tag in tags)
                {
                    var tagTypeGroup = new GroupLayerItemModel
                    {
                        Name = tag
                    };
                    var pois = GviMap.PoiManager.GetPoiNameBytags(tag);
                    foreach (var key in pois.Keys)
                    {
                        var poiItem = new PoiItem()
                        {
                            PoiId = key,
                            Name = pois[key],
                            Tag = tag,
                            ViewPort = this.ViewPortIndex
                        };
                        tagTypeGroup.Children.Add(poiItem);
                    }
                    poiLayerRoot.Children.Add(tagTypeGroup);
                }
                this.PoiGroupLayer = poiLayerRoot;
                // Children.Add(poiLayerRoot);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        //public List<LayerItemModel> Items
        //{
        //    get
        //    {
        //        return this.items;
        //    }
        //    set
        //    {
        //        base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this.items, value, "Items");
        //    }
        //}

        //private List<LayerItemModel> items = new List<LayerItemModel>();

        public override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
            foreach (LayerItemModel layerItemModel in this.Children)
            {
                layerItemModel.IsVisible = this.IsVisible;
            }
        }
    }
}