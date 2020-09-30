using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule
{
    public class GpsViewModel: CheckedToolItemModel
    {
        private List<LayerItemModel> Layers { get; set; }
        private List<IRenderable> _renderableList;
        private Dictionary<string, DeviceInfoModel> _deviceInfoList;
        private System.Timers.Timer timer = new System.Timers.Timer(3000);//实例化Timer类
        private readonly object SynObject = new object();
        private CreatePoiManager _poiManager;
        private DeviceDataManager _deviceManager;
        private Dictionary<string, IRenderPOI> _rPoiDic;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this._renderableList = new List<IRenderable>();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            //this.Command = new  RelayCommand(GetSource);
            _deviceInfoList = new Dictionary<string, DeviceInfoModel>();
            _poiManager = new CreatePoiManager();
            _deviceManager = new DeviceDataManager();
            _rPoiDic = new Dictionary<string, IRenderPOI>();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ShowData();
        }


        public override void OnUnchecked()
        {
            base.OnUnchecked();
            HiddenData();
        }

        //private static bool ShowState = false;
        //private void GetSource()
        //{
        //    if (ShowState)
        //        HiddenData();
        //    else
        //        ShowData();
        //}
        private  void ShowData()
        {
            IsSelected = true;
            //ShowState = true;

            Task.Run(() =>
            {
                try
                {
                    try
                    {
                        _deviceManager.GetDeviceInfoList("phonegis", ref _deviceInfoList,false);
                    }
                    catch { }

                    foreach (var item in _deviceInfoList.ToArray())
                    {
                        var rPoi = _poiManager.CreatePoi(item.Value, "保安");
                        _renderableList.Add(rPoi);
                        _rPoiDic.Add(item.Key, rPoi);
                    }
                    timer.Start();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                }
            });
        }
        private void HiddenData()
        {
            IsSelected = false;
            //ShowState = false;
            timer.Stop();
            GviMap.ObjectManager.ReleaseRenderObject(_renderableList?.ToArray());
            _renderableList?.Clear();
            _deviceInfoList?.Clear();
            _rPoiDic?.Clear();
        }

        private void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            lock (SynObject)
            {
                Task.Run(() =>
                {
                    try
                    {
                        _deviceManager.GetDeviceInfoList("phonegis", ref _deviceInfoList,false);
                    }
                    catch { }

                    foreach (var item in _deviceInfoList.ToArray())
                    {
                        if(_rPoiDic.ContainsKey(item.Key))
                            _poiManager.ChangePoiPosition(_rPoiDic[item.Key],item.Value);
                    }
                });
            }
        }
    }
}
