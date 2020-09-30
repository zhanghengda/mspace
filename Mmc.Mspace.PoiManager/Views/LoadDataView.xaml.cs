using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// Load2DLayerView.xaml 的交互逻辑
    /// </summary>
    public partial class LoadDataView
    {
        public Func<List<string>> getLocalFileName;
        public Action<string, string, string, double,int> addData;
        public Func<string, string, IDictionary<string, string>> getNetServiceLayerInfo;
        private UserInfo _userInfo;
       // private string _filetype;
        private bool _onLocal = false;
        //public LoadDataView()
        //{
        //    InitializeComponent();
        //}
        private ObservableCollection<LoadType> _typeCollection = new ObservableCollection<LoadType>();
        public ObservableCollection<LoadType> TypeCollection
        {
            get { return _typeCollection; }
            set
            {
                _typeCollection = value;
                // OnPropertyChanged("TypeCollection");
            }
        }
        //private System.Windows.Visibility _isImage = Visibility.Hidden;
        public System.Windows.Visibility IsImage;
        //{
        //    get { return _isImage; }
        //    set
        //    {
        //        _isImage = value;
        //        // OnPropertyChanged("TypeCollection");
        //    }
        //}
        public LoadDataView()
        {
            InitializeComponent();
            _userInfo = CacheData.UserInfo;
            _onLocal = true;
            ChangeStyle();

            //_filetype = type;
            LocalDataTab.IsSelected = true;
            okBtn2.IsEnabled = true;
            //if (type == "ImageGroupLayer")
            //  {
            UpdateCycleTxt.Visibility = Visibility.Hidden;
                UpdateCycleTxtBox.Visibility = Visibility.Hidden;//屏蔽周期更新控件
         //   }

            if (_userInfo.mspace_config?.is_administrator == "1")
            {
                ServerDataTab.Visibility = Visibility.Visible;
                netTypeComBox.Items.Add("TILE");
                netTypeComBox.Items.Add("WFS");
                netTypeComBox.Items.Add("WMTS");
                netTypeComBox.Items.Add("MODEL");
               
            }
            this.DeleteObjCmd = new Wpf.Commands.RelayCommand<LoadType>((loadType) => OnDeleteItem(loadType));
           // this.TextChangeCmd = new Wpf.Commands.RelayCommand<LoadType>((loadType) => OnChangeUpdateTime(loadType));
        }

        private void localFileBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> _filenames = getLocalFileName();
            if (_filenames?.Count!=0)
            { 
                foreach(var name in _filenames)
                {
                    addRecord(name);
                }
               
            }
        }
        private void loadFileName()//弃用
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(localFileTxtBox.Text);
            LoadType aload = new LoadType(Convert.ToString(TypeCollection.Count), filename, localFileTxtBox.Text,System.Windows.Visibility.Hidden,"30","0");
            routeplandg.DataContext = this;
            TypeCollection.Add(aload);

        }
        private void netConBtn_Click(object sender, RoutedEventArgs e)
        {
            //okBtn.IsEnabled = false;
            string servicetype = netTypeComBox.Text;
            string url = netFileUrlTxtBox.Text;
            if (url == null || url == "")
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ErrorServerURL"));//"服务地址为空，请先设置服务地址。"
                return;
            }

            var netVarify = new NetConnectableVarify();

            switch (servicetype)
            {
                case "WFS":

                    if (!netVarify.IsUrlConnectable(url))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NetUnConnectable"));
                        return;
                    }

                    if (!url.Contains("WFSServer"))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ErrorWFSServerURL"));//服务地址格式错误，请重新设置。e.g.http://192.168.4.190:6080/arcgis/services/luoxing/MapServer/WFSServer"
                        return;
                    }

                    IDictionary<string, string> layerInfoDic = getNetServiceLayerInfo(url, servicetype);
                    netLayerComBox.Items.Clear();
                    netLayerComBox.IsEnabled = true;

                    if (layerInfoDic.Count > 0)
                    {
                        foreach (var item in layerInfoDic)
                        {
                            netLayerComBox.Items.Add(item);
                        }
                    }
                    netLayerComBox.IsEnabled = true;
                    break;
                case "WMTS":

                    if (!netVarify.IsUrlConnectable(url))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NetUnConnectable"));
                        return;
                    }

                    if (!url.ToUpper().Contains("WMTS"))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ErrorWMTSServerURL"));
                        return;
                    }
                    netLayerComBox.IsEnabled = false;
                    break;
                case "TILE":
                    if (!netVarify.IsTileUrlConnectable(url))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NetUnConnectable"));
                        return;
                    }
                    break;
                case "MODEL":
                    if (!netVarify.IsModelUrlConnectable(url))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NetUnConnectable"));
                        return;
                    }
                    break;
            }
            //okBtn.IsEnabled = true;
        }

        private bool IsIPConnectable(string ipStr, int port)
        {
            bool isConnectable = false;
            IPAddress ip = IPAddress.Parse(ipStr);
            IPEndPoint point = new IPEndPoint(ip, port);
            try
            {
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.Connect(point);
                    Console.WriteLine("连接{0}成功!", point);
                    sock.Close();
                    isConnectable = true;
                }
            }
            catch (SocketException e)
            {
                Messages.ShowMessage(string.Format("{0}" + Helpers.ResourceHelper.FindKey("IPCheck"), point));
                Console.WriteLine("连接{0}失败", point);
            }
            return isConnectable;
        }
       
       
        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
           // string filetype = _filetype;
           // string fileAddress = string.Empty;
          //  string guid = string.Empty;        
           // double cycleTime = 30;
            if (_onLocal)
            {
                //routeplandg.IsEnabled = true;
                for (int i = 0; i < TypeCollection.Count; i++)
                {                   
                    bool enableChange = double.TryParse(TypeCollection[i].UpdateTime, out double _updatatime);
                  //  bool loadstatus = false;
                    if (enableChange)
                    {
                        LoadType load = routeplandg.Items.GetItemAt(i) as LoadType;
                  
                        if (_updatatime > 0)
                        {
                            loadLayer(TypeCollection[i].path, _updatatime, i);
                            string filetype = ExtensionConfirm(System.IO.Path.GetExtension(TypeCollection[i].path));
                            if (filetype!= "ShpGroupLayer"&&filetype!= "DataSetGroupLayer")//矢量和fdb图层采用队列加载方式，其它方式异步会先通过这里修改状态
                            {
                                TypeCollection[i].loadStation = "加载中...";
                            }
                            //
                        }
                        else
                        { 
                            loadLayer(TypeCollection[i].path, 20, i);//默认值
                            string filetype = ExtensionConfirm(System.IO.Path.GetExtension(TypeCollection[i].path));
                            if (filetype != "ShpGroupLayer" && filetype != "DataSetGroupLayer")//矢量和fdb图层采用队列加载方式，其它方式异步会先通过这里修改状态
                            {
                                TypeCollection[i].loadStation = "加载中...";
                            }
                        }
                    }
                    else
                    {
                        loadLayer(TypeCollection[i].path, 20,i);//默认值
                        string filetype = ExtensionConfirm(System.IO.Path.GetExtension(TypeCollection[i].path));
                        if (filetype != "ShpGroupLayer" && filetype != "DataSetGroupLayer")//矢量和fdb图层采用队列加载方式，其它方式异步会先通过这里修改状态
                        {
                            TypeCollection[i].loadStation = "加载中...";
                        }
                    }                   
                }
            }
            else
            {
                string guid ="";
                if (_userInfo.mspace_config.is_administrator != "1")
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoPermission"));
                    return;
                }
               var filetype = netTypeComBox.Text;
               var fileAddress = netFileUrlTxtBox.Text;
               
                switch (filetype)
                {
                    case "WFS":
                        guid = netLayerComBox.SelectedValue.ToString();
                        addData(filetype, fileAddress, guid, 20,-1);//-1代表网络图层
                        break;
                    case "WMTS":
                        addData(filetype, fileAddress, "",20, -1);
                        break;
                    case "TILE":
                        addData(filetype, fileAddress, "",20, -1);
                        break;
                    case "MODEL":
                        addData(filetype, fileAddress, "",20, -1);
                        break;
                }
            
                 
            }
            okBtn2.IsEnabled = false;
        }
  
        public ICommand DeleteObjCmd { get; set; }
        public ICommand TextChangeCmd { get; set; }
        
        private void OnDeleteItem(LoadType _aload)
        {
            TypeCollection.Remove(_aload);
        }
        private void OnChangeUpdateTime(LoadType _aload)//弃用
        {
            var poi = TypeCollection.IndexOf(_aload);
         //   this.routeplandg.Items.
            _aload.UpdateTime = "100";
     
        }
      
 
        private void loadLayer(string _fileAddress, double cycleTime ,int _index)
        {
            //string filetype = _filetype;
           // bool status = false;
            string guid = string.Empty;

            if (_fileAddress == null || _fileAddress == "")
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("EmptyFilePath")); //("请选择图层");
              
            }
            else
            {
                string filetype = ExtensionConfirm(System.IO.Path.GetExtension(_fileAddress));
                
                addData(filetype, _fileAddress, guid, cycleTime, _index);
            }
           
        }
        //private void loadALayer()
        //{
        //  //  string filetype = _filetype;
        //    string fileAddress = string.Empty;
        //    string guid = string.Empty;
        //    double cycleTime = 30;
        //    int index = 0;
        //    if (_onLocal)
        //    {
        //        fileAddress = localFileTxtBox.Text;
        //        if (fileAddress == null || fileAddress == "")
        //        {
        //            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("EmptyFilePath")); //("请选择图层");
        //            return;
        //        }

        //        if (!string.IsNullOrEmpty(UpdateCycleTxtBox.Text))
        //        {
        //            if (double.TryParse(UpdateCycleTxtBox.Text, out double day) && day >= 0)
        //            {
        //                cycleTime = day;
        //            }
        //            else
        //            {
        //                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalDoubleNumber")); //("请选择图层");
        //                return;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_userInfo.mspace_config.is_administrator != "1")
        //        {
        //            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoPermission"));
        //            return;
        //        }
        //       // filetype = netTypeComBox.Text;
        //        fileAddress = netFileUrlTxtBox.Text;

        //        switch (filetype)
        //        {
        //            case "WFS":
        //                guid = netLayerComBox.SelectedValue.ToString();
        //                break;
        //            case "WMTS":
        //                break;
        //            case "TILE":
        //                break;
        //            case "MODEL":
        //                break;
        //        }
        //    }
        //    addData(filetype, fileAddress, guid, cycleTime, index);
        //}
        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            
          //  this.Hide();//后台运行
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChangeStyle()
        {
            var fireColor = ColorTranslator.FromHtml("#FF50ABFF");
            var offColor = ColorTranslator.FromHtml("#FF6A7076");
            if (_onLocal)
            {
                //localRadio.IsChecked = true;
                localFileBtn.IsEnabled = true;
                localFileTxtBox.IsEnabled = true;
                netTypeComBox.IsEnabled = false;
                netFileUrlTxtBox.IsEnabled = false;
                netConBtn.IsEnabled = false;
                netLayerComBox.IsEnabled = false;
                //okBtn.IsEnabled = true;
                localFileBtn.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(fireColor.A, fireColor.R, fireColor.G, fireColor.B));
                //netConBtn.Background= new SolidColorBrush(System.Windows.Media.Color.FromArgb(offColor.A, offColor.R, offColor.G, offColor.B));
            }
            else
            {
                //netRadio.IsChecked = true;
                localFileBtn.IsEnabled = false;
                localFileTxtBox.IsEnabled = false;
                netTypeComBox.IsEnabled = true;
                netFileUrlTxtBox.IsEnabled = true;
                netConBtn.IsEnabled = true;
                netLayerComBox.IsEnabled = false;
                //okBtn.IsEnabled = false;
                //localFileBtn.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(offColor.A, offColor.R, offColor.G, offColor.B));
                netConBtn.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(fireColor.A, fireColor.R, fireColor.G, fireColor.B));
            }

        }

        private void ServerDataTab_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            _onLocal = false;
            ChangeStyle();
        }

        private void LocalDataTab_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            _onLocal = true;
            ChangeStyle();
        }

        private void NetTypeComBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //okBtn.IsEnabled = false;
            netFileUrlTxtBox.Text = null;
            //netConBtn.IsEnabled = false;
        }
       

        private void NetFileUrlTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //okBtn.IsEnabled = false;
        }
        private void Window_Drop(object sender, DragEventArgs e)
        {
            //string msg = "Drop";
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //   msg = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //    addRecord(msg);
            //}
            string dropFile = "Drop";
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                int count = ((System.Array)e.Data.GetData(System.Windows.DataFormats.FileDrop)).Length;
                for (int i = 0; i < count; i++)
                {
                    dropFile = ((System.Array)e.Data.GetData(System.Windows.DataFormats.FileDrop)).GetValue(i).ToString(); ;
                    addRecord(dropFile);
                }
            }
        }
           
        
        private void addRecord(string _path)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(_path);
            string _type = ExtensionConfirm(System.IO.Path.GetExtension(_path));
            if (_type != "")
            {

                if (_type != "ImageGroupLayer")
                {
                    IsImage = System.Windows.Visibility.Hidden;
                    LoadType aload = new LoadType(Convert.ToString(TypeCollection.Count), filename, _path, IsImage, "20", "未开始");
                    routeplandg.DataContext = this;
                    TypeCollection.Add(aload);
                }
                else
                {
                    IsImage = System.Windows.Visibility.Visible;
                    LoadType aload = new LoadType(Convert.ToString(TypeCollection.Count), filename, _path, IsImage, "20", "未开始");
                    routeplandg.DataContext = this;
                    TypeCollection.Add(aload);
                }
            }
        }

        private string ExtensionConfirm(string _extension)
        {
            string typeString = "";
            switch (_extension)
            {
                case ".shp":
                    typeString = "ShpGroupLayer";
                    break;
                case ".SHP":
                    typeString = "ShpGroupLayer";
                    break;
                case ".tdbx":
                    typeString = "TileGroupLayer";
                    break;
                case ".TDBX":
                    typeString = "TileGroupLayer";
                    break;
                case ".tif":
                    typeString = "ImageGroupLayer"; 
                    break;
                case ".TIF":
                    typeString = "ImageGroupLayer";
                    break;
                case ".tds":
                    typeString = "ImageGroupLayer";
                    break;
                case ".TDS":
                    typeString = "ImageGroupLayer";
                    break;
                case ".fdb":
                    typeString = "DataSetGroupLayer";
                    break;
                case ".FDB":
                    typeString = "DataSetGroupLayer";
                    break;
                default:
                    typeString = "";
                    break;

            }
                return typeString;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LocalDataTab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
