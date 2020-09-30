using System;
using Gvitech.CityMaker.FdeGeometry;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.DTO;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Tools;
using Mmc.Windows.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Windows.Services;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class RouteManageViewModel : BaseViewModel
    {
        #region varies

        public List<RouteForClient> CurrentRouteList { get; set; }

        private WireTowerConverter _wireTowerConverter;
        private CalculateAndOutputRoute _calculateAndOutputRoute;

        #endregion

        #region binging varies and command
        private LineForClient _selectedLine;
        public LineForClient SelectedLine
        {
            get { return _selectedLine; }
            set
            {
                _selectedLine = value;
                OnPropertyChanged("SelectedLine");
            }
        }
        private ObservableCollection<LineForClient> _lineSet;
        public ObservableCollection<LineForClient> LineSet
        {
            get { return _lineSet; }
            set
            {
                _lineSet = value;
                OnPropertyChanged("LineSet");
            }
        }

        private ObservableCollection<RouteForClient> _routeSet;
        public ObservableCollection<RouteForClient> RouteSet
        {
            get { return _routeSet; }
            set
            {
                _routeSet = value;
                OnPropertyChanged("RouteSet");
            }
        }

        private Visibility _isDetailBtnVisible;
        public Visibility IsDetailBtnVisible
        {
            get { return _isDetailBtnVisible; }
            set
            {
                _isDetailBtnVisible = value;
                OnPropertyChanged("IsDetailBtnVisible");
            }
        }

        private Visibility _isDeleteBtnVisible;
        public Visibility IsDeleteBtnVisible
        {
            get { return _isDeleteBtnVisible; }
            set
            {
                _isDeleteBtnVisible = value;
                OnPropertyChanged("IsDeleteBtnVisible");
            }
        }

        [XmlIgnore] public ICommand ImportCommand { get; set; }
        [XmlIgnore] public ICommand ImportKmlCommand { get; set; }
        [XmlIgnore] public ICommand BatchDelCommand { get; set; }
        [XmlIgnore] public ICommand BatchOutCommand { get; set; }
        [XmlIgnore] public ICommand EditCommand { get; set; }
        [XmlIgnore] public ICommand DeleteCommand { get; set; }
        [XmlIgnore] public ICommand DetailCommand { get; set; }
        [XmlIgnore] public ICommand OutputCommand { get; set; }
        [XmlIgnore] public ICommand OutputMissionCommand { get; set; }

        #endregion

        public RouteManageViewModel()
        {
            this.ImportCommand = new Mmc.Wpf.Commands.RelayCommand(onImportRoute);
            this.ImportKmlCommand = new Mmc.Wpf.Commands.RelayCommand(onImportKmlRoute);
            this.BatchOutCommand = new Mmc.Wpf.Commands.RelayCommand(batchOutRoute);
            this.BatchDelCommand = new Mmc.Wpf.Commands.RelayCommand(batchDelRoute);
            this.EditCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnEditRoute);
            this.DeleteCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnDeleteRoute);
            this.DetailCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnShowRoute);
            this.OutputCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnOutputRoute);
            this.OutputMissionCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnOutputMissionRoute);
            Messenger.Messengers.Register<RouteForClient>("AddRouteOfWir", AddRouteData);

            CurrentRouteList = new List<RouteForClient>();
            _wireTowerConverter = new WireTowerConverter();
            _calculateAndOutputRoute = new CalculateAndOutputRoute();
            LineSet = new ObservableCollection<LineForClient>(WirTowRenderManagement.Instance.Lines);
            if (LineSet?.Count > 0) SelectedLine = LineSet.FirstOrDefault();
        }

        public void UpdateLineSetValue(List<LineForClient> obj)
        {
            if (obj == null) return;
            LineSet = new ObservableCollection<LineForClient>(obj);
        }

        private void OnShowRoute(object obj)
        {
            if (!(obj is RouteForClient route)) return;
            RenderCurrentRoute(route);
        }

        private void OnOutputMissionRoute(object obj)
        {
            if (!(obj is RouteForClient route)) return;
            var polyline = _calculateAndOutputRoute.GenerateMissionRoute(route.RouteName, route.Towers);
            if (polyline == null)
            {
                Messages.ShowMessage($"{route.Serial} {Helpers.ResourceHelper.FindKey("WTTowerInfoMiss")}");
                return;
            }

            WirTowRenderManagement.Instance.ClearRenderObj();
            WirTowRenderManagement.Instance.RenderPolyline(polyline, null);
            WirTowRenderManagement.Instance.FlyToGeometry(polyline);
        }

        private void OnOutputRoute(object obj)
        {
            if (!(obj is RouteForClient route)) return;
            _calculateAndOutputRoute.GenerateRoute(route.RouteName, route.Towers, false);
            RenderCurrentRoute(route);
        }

        private void onImportKmlRoute()
        {
            //throw new NotImplementedException();
        }

        private void batchOutRoute()
        {
            //throw new NotImplementedException();
        }

        private void batchDelRoute()
        {
            //throw new NotImplementedException();
        }

        private void onImportRoute()
        {
            if (!IsLineChoosed()) return;

            try
            {

                string filePath = string.Empty;
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.ShowDialog();
                if (dialog.SelectedPath != string.Empty) filePath = dialog.SelectedPath;
                else return;

                string routeName = Path.GetFileName(filePath);
                string leftDir = filePath + "\\" + routeName + "甲线";
                string rightDir = filePath + "\\" + routeName + "乙线";

                var leftList = ReadFileInDirEx(leftDir, out string kmlfile);
                var rightList = ReadFileInDirEx(rightDir, out kmlfile);

                var leftLineList = leftList.Item1;
                var rightLineList = rightList.Item1;
                kml kml = new kml();
                List<Placemark> placemarks = kml.ReadkmlFile(kmlfile);
                var routemodel = new RouteForClient 
                {
                    RouteName = routeName,
                    TowerList = placemarks,
                    TowerCount = placemarks.Count,
                    LeftLineList = leftLineList,
                    RightLineList = rightLineList,
                    LeftWay=leftList.Item2,
                    RightWay=rightList.Item2,
                    //Pid = SelectedLine,
                };

                var files = Directory.GetFiles(filePath, "*", SearchOption.TopDirectoryOnly);
                var towerfile = files?.FirstOrDefault(p => p.ToLower().Contains("towers.json"));
                routemodel.Towers = ReadTowers(towerfile);



                AddRouteData(routemodel);

                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LOADSUCCESSED"));

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex.ToString());
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LOADFAILED"));
            }
        }

        private List<TowerForClient> ReadTowers(string towerfile)
        {
            if (!File.Exists(towerfile)) return null;
            List<TowerForClient> temptowers = new List<TowerForClient>();
            try
            {
                StreamReader srReadFile = new StreamReader(towerfile);
                string jsonText = "";
                while (!srReadFile.EndOfStream)
                {
                    jsonText += srReadFile.ReadLine();
                }

                var towerList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TowerModel>>(jsonText);

                if (towerList == null || towerList.Count <= 0) return null;
                foreach (var item in towerList)
                {
                    temptowers.Add(_wireTowerConverter.TowerConvert(item));
                }

                return temptowers;
            }
            catch
            {
                return null;
            }
        }

        private void AddRouteData(RouteForClient route)
        {
            if (CurrentRouteList.Find(p => p.RouteName.Equals(route.RouteName)) == null)
            {
                CurrentRouteList.Add(route);
                RefleshData();
            }

            RenderCurrentRoute(route);
        }

        private void RefleshData()
        {
            RouteSet = new ObservableCollection<RouteForClient>(CurrentRouteList);
        }

        private List<IPolyline> ReadFileInDir(string dirPath, out string kmlfile)
        {
            if (Directory.Exists(dirPath))
            {
                var files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
                kmlfile = files.FirstOrDefault(p => p.ToLower().Contains(".kml"));
                var flightFiles = files.ToList().FindAll(p => p.ToLower().Contains(".json"));

                var tempList = new List<IPolyline>();
                foreach (var file in flightFiles)
                {
                    var temp = JsonUtil.DeserializeFromFile<HttpFlight>(file);
                    var polyline = _wireTowerConverter.FlightToPolyline(temp.data);
                    tempList.Add(polyline);
                }
                return tempList;
            }
            else
            {
                kmlfile = string.Empty;
                return null;
            }
        }


        private Tuple<List<IPolyline>, List<FlightWay>> ReadFileInDirEx(string dirPath, out string kmlfile)
        {
            if (Directory.Exists(dirPath))
            {
                var files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
                kmlfile = files.FirstOrDefault(p => p.ToLower().Contains(".kml"));
                var flightFiles = files.ToList().FindAll(p => p.ToLower().Contains(".json"));

                var tempList = new List<IPolyline>();
                var flyWayList = new List<FlightWay>();
                foreach (var file in flightFiles)
                {
                    var temp = JsonUtil.DeserializeFromFile<HttpFlight>(file);
                    var polyline = _wireTowerConverter.FlightToPolyline(temp.data);
                    tempList.Add(polyline);
                    flyWayList.Add(temp.data);
                }
                return new Tuple<List<IPolyline>, List<FlightWay>>(tempList,flyWayList);
            }
            else
            {
                kmlfile = string.Empty;
                return null;
            }
        }

        private void OnDeleteRoute(object obj)
        {
            if (!(obj is RouteForClient route)) return;
            if (CurrentRouteList.Contains(route) && Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemWarn"), $"{Helpers.ResourceHelper.FindKey("WTItemDelete")}{route.Serial}"))
            {
                CurrentRouteList.Remove(route);
                RefleshData();
                WirTowRenderManagement.Instance.ClearRenderObj();
            }
        }

        private void OnEditRoute(object obj)
        {
            //throw new NotImplementedException();
        }

        private void RenderCurrentRoute(RouteForClient route)
        {
            WirTowRenderManagement.Instance.ClearRenderObj();
            WirTowRenderManagement.Instance.RenderLookAt(route);
            WirTowRenderManagement.Instance.RenderRoute(route);
        }

        private bool IsLineChoosed()
        {
            bool isChoosed = false;
            if (SelectedLine == null || SelectedLine.LineName == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTChooseOprObj"));
            }
            else
            {
                isChoosed = true;
            }
            return isChoosed;
        }
    }
}