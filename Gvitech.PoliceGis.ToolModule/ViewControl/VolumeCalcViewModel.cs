using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    /// <summary>
    /// 体积计算
    /// </summary>
    public class VolumeCalcViewModel : CheckedToolItemModel
    {
        private readonly string _lableKey = "lableKey";
        private readonly string _VolumeMeasureOperationKey = "VolumeMeasureOperation_Key";
        private IPolyline _polyline;
        private DrawCustomerUC _drawCustomer;
        private readonly string _drawCustomerName = "体积计算";
        private bool DrawFlag;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
        }
        public override void OnChecked()
        {
            try
            {
                base.OnChecked();
                if (_drawCustomer == null)
                    _drawCustomer = new DrawCustomerUC(_drawCustomerName, DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
                RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, _drawCustomer, RCMouseOperType.PickPoint);
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished += PolygonDraw_OnDrawFinished;
                GviMap.AxMapControl.RcLButtonUp += AxMapControl_RcLButtonUp;
        }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

}



        private bool AxMapControl_RcLButtonUp(uint Flags, int X, int Y)
        {
            //DumpTest();
            if (DrawFlag)
            {
                GviMap.AxMapControl.RcRButtonUp -= this.AxRenderControl_RcRButtonUp;
            }
            return false;
        }
        //private void DumpTest()
        //{
        //    string temp = null;
        //    string temp2 = temp.ToString();
        //}

        private bool AxRenderControl_RcRButtonUp(uint Flags, int X, int Y)
        {
            this.ReleaseRenderObj();
            return false;
        }

        private void ReleaseRenderObj()
        {
            this.ReleaseRenderPolygon();
        }

        private void ReleaseRenderPolygon()
        {
            if (GviMap.TempRObjectPool.ContainsKey(_lableKey))
            {
                GviMap.ObjectManager.DeleteObject(GviMap.TempRObjectPool[_lableKey].Guid);
                GviMap.TempRObjectPool.Remove(_lableKey);
            }
            if (GviMap.TempRObjectPool.ContainsKey(_VolumeMeasureOperationKey))
            {
                GviMap.ObjectManager.DeleteObject(GviMap.TempRObjectPool[_VolumeMeasureOperationKey].Guid);
                GviMap.TempRObjectPool.Remove(_VolumeMeasureOperationKey);
            }

            DrawFlag = true;
        }

        private void PolygonDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                if (DrawClient.Instance.DrawCustomer != _drawCustomer)
                    return;
                var rPolygon = result as IRenderPolygon;
                var polygon = rPolygon.GetFdeGeometry() as IPolygon;
                IVolumeMeasureOperation volumeMeasureOperation = null;

                if (!GviMap.TempRObjectPool.ContainsKey(_VolumeMeasureOperationKey))
                {
                    volumeMeasureOperation = GviMap.ObjectManager.CreateVolumeMeasureOperation(GviMap.ProjectTree.RootID);
                    GviMap.TempRObjectPool.Add(_VolumeMeasureOperationKey, volumeMeasureOperation);
                }
                volumeMeasureOperation = GviMap.TempRObjectPool[_VolumeMeasureOperationKey] as IVolumeMeasureOperation;
                volumeMeasureOperation.SetPolygon(polygon);
                volumeMeasureOperation.PolygonFixedHeight = 0;
                volumeMeasureOperation.SampleGridLength = 0.1;
                volumeMeasureOperation.PolygonFixedHeightEnabled = false; //采用均值平面
                volumeMeasureOperation.Execute(); //显示挖填方效果
                IPolygon CutFillmPolygon = volumeMeasureOperation.GetPolygon();
                volumeMeasureOperation.GetVolume(out double CutVolumn, out double FillVolume);

                ILabel VolScan = null;
                if (!GviMap.TempRObjectPool.ContainsKey(_lableKey))
                {
                    VolScan = GviMap.ObjectManager.CreateLabel();
                    GviMap.TempRObjectPool.Add(_lableKey, VolScan);
                }


                VolScan = GviMap.TempRObjectPool[_lableKey] as ILabel;
                //空间查询    
                var ivevtor3 = polygon.Envelope.Center;

                //与瓦片判交，求最高点
                var centerPt = polygon.Centroid;
                //_polyline.StartPoint = centerPt;
                //var endPt = polygon.Centroid.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                //endPt.Z = 500;
                //_polyline.EndPoint = endPt;
                //double z = -10;
                //foreach (var item in DataBaseService.Instance.GetTileLayers())
                //{
                //    if (item.Layer.PolylineIntersect(_polyline, out string fdsetName, out string fcName, out int fid, out IVector3 intertSetPt))
                //        z = z > intertSetPt.Z ? z : intertSetPt.Z;
                //}
                //centerPt.Z = z;
                VolScan.Position = centerPt;
                VolScan.MaxVisibleDistance = 5000;
                VolScan.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;//禁用深度检测
                VolScan.Text = Helpers.ResourceHelper.FindKey("Volume") + Math.Round(CutVolumn, 3).ToString() + "m³";
                //endPt.Dispose();
                //endPt = null;

                if (_drawCustomer == null)
                    _drawCustomer = new DrawCustomerUC(_drawCustomerName, DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
                RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, _drawCustomer, RCMouseOperType.PickPoint);
                DrawFlag = false;
                GviMap.AxMapControl.RcRButtonUp += this.AxRenderControl_RcRButtonUp;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }



        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();
                //取消注册事件
                RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
                //清除渲染对象
                if (GviMap.TempRObjectPool.Count == 0) return;

                if (GviMap.TempRObjectPool.ContainsKey(_lableKey))
                {
                    GviMap.ObjectManager.DeleteObject(GviMap.TempRObjectPool[_lableKey].Guid);
                    GviMap.TempRObjectPool.Remove(_lableKey);
                }
                if (GviMap.TempRObjectPool.ContainsKey(_VolumeMeasureOperationKey))
                {
                    GviMap.ObjectManager.DeleteObject(GviMap.TempRObjectPool[_VolumeMeasureOperationKey].Guid);
                    GviMap.TempRObjectPool.Remove(_VolumeMeasureOperationKey);
                }

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        private void ShowView()
        {
            //其他组建隐藏
            //ServiceManager.GetService<IShellService>(null).LeftWindow.Hide();
            //ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;
            //ServiceManager.GetService<IShellService>(null).RightToolMenu.Visibility = System.Windows.Visibility.Hidden;
            var width = ServiceManager.GetService<IShellService>(null).LeftWindow.Width;
            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            mapView.Width -= width;
            mapView.Left = width;

        }

        private void HideView()
        {

            //ServiceManager.GetService<IShellService>(null).RightToolMenu.Visibility = System.Windows.Visibility.Visible;
            //ServiceManager.GetService<IShellService>(null).BottomToolMenu.Visibility = System.Windows.Visibility.Visible;
            //ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
            var width = ServiceManager.GetService<IShellService>(null).LeftWindow.Width;
            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            mapView.Width += width;
            mapView.Left = 0;
            mapView.UpdateLayout();
            //退出恢复单屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
        }
    }
}
