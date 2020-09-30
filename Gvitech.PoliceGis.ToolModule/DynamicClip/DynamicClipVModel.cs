using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Windows.Utils;
using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
//using Gvitech.Framework.Services;
using Gvitech.Windows.Utils;
using System.Drawing;
using System.Data;
using Gvitech.CityMaker.Models;
using Gvitech.CityMaker.Math;
using System.IO;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.Common.Messenger;
using Mmc.Windows.Framework.Commands;
using System.Collections.ObjectModel;

using DevExpress.Services.Internal;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.ToolModule.DynamicClip
{

    public class DynamicClipVModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public System.Windows.Input.ICommand PolygonClipCmd { get; set; }

        [XmlIgnore]
        public System.Windows.Input.ICommand VolumeClipCmd { get; set; }
        [XmlIgnore]
        public System.Windows.Input.ICommand DisposeCmd2 { get; set; }//关闭
        [XmlIgnore]
        public System.Windows.Input.ICommand NewClipDataCmd { get; set; }
        [XmlIgnore]
        public System.Windows.Input.ICommand SaveClipCmd { get; set; }
      

        [XmlIgnore]
        public System.Windows.Input.ICommand DelClipCmd { get; set; }
        [XmlIgnore]
        public System.Windows.Input.ICommand RenameCmd { get; set; }


        [XmlIgnore]
        public System.Windows.Input.ICommand DoubleClickCommand { get; set; }
        

        private IClipPlaneOperation _curClipOper;
        private DynamicClip dynamicClip;

        private ILocalWsConfigService _localWsCfgSrv;
        //  public List<ClipListStruct> ClipListData = new List<ClipListStruct>();
        // private string _filePath = "";

        // Token: 0x04000018 RID: 24
        private string _tmpPath;
        private ObservableCollection<ClipData> _clipDataColletion = new ObservableCollection<ClipData>();
        public ObservableCollection<ClipData> ClipDataColletion
        {
            get { return _clipDataColletion; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ClipData>>(ref this._clipDataColletion, value, "ClipDataColletion");
            }
        }
        

        private void AddDataItem(ClipData clipItem)
        {
            var model = ClipDataColletion.FirstOrDefault(t => t.Guid == clipItem.Guid);
            if (model == null)
            {
                ClipDataColletion.Add(clipItem);
            }
        }

        private void RemoveClipDataItem(ClipData clipItem)
        {
            var model = ClipDataColletion.FirstOrDefault(t => t.Guid == clipItem.Guid);
            if (model != null)
            {
                ClipDataColletion.Remove(clipItem);
            }
        }


        private void Rename(ClipData clipItem, string newName)
        {
            var model = ClipDataColletion.FirstOrDefault(t => t.Guid == clipItem.Guid);
            if (model != null)
            {
                model.Name = newName;
            }
        }
        private NewClipObjectView _newClipObjectViewGS;
        public NewClipObjectView NewClipObjectViewGS
        {
            get { return _newClipObjectViewGS; }
            set { _newClipObjectViewGS = value; NotifyPropertyChanged("NewClipObjectViewGS"); }
        }
        private NewClipObjectVModel newClipObjectVModel;
        private  RenameObjectView  _renameObjectViewS;
        public RenameObjectView RenameObjectViewS
        {
            get { return _renameObjectViewS; }
            set { _renameObjectViewS = value; NotifyPropertyChanged("RenameObjectViewS"); }
        }
        private RenameObjectVModel renameObjectVModel;
        // Token: 0x04000019 RID: 25
        private Image _img;
        public override void Initialize()
        {

            try
            {

                _localWsCfgSrv = Mmc.Windows.Services.ServiceManager.GetService<ILocalWsConfigService>(null);
                // MessageBox.Show("11");
                //  OnFreshClip();
                base.Initialize();
            base.ViewType = (ViewType)1;
            string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CityMaker CityFacility");
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            _tmpPath = Path.Combine(text, "Data");

          //  this.LoadData();
            this.PolygonClipCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                // GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                // GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            //dd    GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractClipPlane;
                GviMap.AxMapControl.ClipMode = gviClipMode.gviClipCustomePlane;

                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            });

            this.NewClipDataCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
             {

             });
            this.VolumeClipCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractClipPlane;

                GviMap.MapControl.ClipMode = gviClipMode.gviClipBox;
            
            });
            this.DisposeCmd2 = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
                /*  GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;// gviSelectFeatureLayer;
                  GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;*/

                GviMap.MapControl.SetRenderParam(gviRenderControlParameters.gviRenderParamClipPlaneLineColor, Color.Red);
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;

                OnUnchecked();
                base.IsChecked = false;
                // base.OnUnchecked();
                //取消注册事件

            });
            this.SaveClipCmd = new Mmc.Wpf.Commands.RelayCommand(OnNewClip);
           
            this.DelClipCmd = new Mmc.Wpf.Commands.RelayCommand(() => RemoveClipData());
        
           this.RenameCmd = new Mmc.Wpf.Commands.RelayCommand(OnRenameClip);
         

          
            this.DoubleClickCommand = new RelayCommand(OnDataGridDoubleClick);
                if (_localWsCfgSrv != null) { 
                   
                var model = _localWsCfgSrv.ClipDatas.FindAll();
                    if (model != null) { 
                        foreach (var item in model)
                        {
                            ClipData clipOperaToData = new ClipData();
                            clipOperaToData.Position = item.Position;
                            clipOperaToData.BoxSize = item.BoxSize;
                            clipOperaToData.Guid = item.Guid;
                            CameraProperty camera = new CameraProperty
                            {
                                X = item.X,
                                Y = item.Y,
                                Z = item.Z,
                                Heading = item.Heading,
                                Tilt = item.Tilt,
                                Roll = item.Roll
                            };
                            clipOperaToData.Camera = camera;
                            clipOperaToData.Type = item.Type;
                            clipOperaToData.Angle = item.Angle;
                            clipOperaToData.Name = item.Name;
                            ClipDataColletion.Add(clipOperaToData);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }

          
        }

        public DynamicClipVModel()
        {
              
        }

        private void OnRenameClip()
        {
            if (RenameObjectViewS == null)
            {
                //ClipListStruct mySelectedElement = (ClipListStruct)dynamicClip.datagrid.SelectedItem;
                ClipData mySelectedElement = (ClipData)dynamicClip.datagrid.SelectedItem;
                renameObjectVModel = new RenameObjectVModel(mySelectedElement.Name, ClipDataColletion, mySelectedElement.Guid);


               // renameObjectVModel.ReNameObject = mySelectedElement.Name;
            }
            renameObjectVModel.ReSectionName += OnUpdateObjectReName;
            renameObjectVModel.ShowView();
            
        }

        private void RemoveClipData()
        {
            ClipData mySelectedElement = (ClipData)dynamicClip.datagrid.SelectedItem;
            RemoveClipDataItem(mySelectedElement);
            if (_localWsCfgSrv != null) { 
            _localWsCfgSrv.ClipDatas.Delete(t => t.Guid == mySelectedElement.Guid);
            }
        }

      
        private void OnDataGridDoubleClick()
        {
            ClipData mySelectedElement = (ClipData)dynamicClip.datagrid.SelectedItem;

            var model=ClipDataColletion.FirstOrDefault(t => t.Guid == mySelectedElement.Guid);
            if(model!=null)
            {
                ClipData clipData = model;
                if (this._curClipOper == null)
                {
                    this._curClipOper = GviMap.MapControl.ObjectManager.CreateClipPlaneOperation(GviMap.MapControl.ProjectTree.NotInTreeID);
                }
                ClipData.ClipDataToOpera(ref this._curClipOper, clipData);
                this._curClipOper.Execute();
                if (clipData.Camera != null)
                {
                    IEulerAngle eulerAngle = new EulerAngle();
                    IVector3 vector = new Vector3();
                    vector.Set(clipData.Camera.X, clipData.Camera.Y, clipData.Camera.Z);
                    eulerAngle.Set(clipData.Camera.Heading, clipData.Camera.Tilt, clipData.Camera.Roll);
                    GviMap.Camera.SetCamera(vector, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
                    GviMap.Camera.FlyTime = 0.0;
                }
            }
           
        }

        private string _newName;
        public string NewName
        {
            get { return _newName; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._newName, value, "NewName");
            }
        }
        private string _reName;
        public string ReName
        {
            get { return _reName; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._reName, value, "ReName");
            }
        }
        private int _listnum;
        public int Listnum
        {
            get { return _listnum; }
            set
            {
                base.SetAndNotifyPropertyChanged<int>(ref this._listnum, value, "Listnum");
            }
        }
    
        public void OnNewClip()
        {
            if (NewClipObjectViewGS == null)
            {
                newClipObjectVModel = new NewClipObjectVModel(gviClipMode.gviClipCustomePlane, _clipDataColletion);

            }
            newClipObjectVModel.ShowView();
            newClipObjectVModel.GeoSectionName += OnUpdateclipName;
          //  RefreshList();
        }
   
        public void OnSaveClip()
           {
         
            IClipPlaneOperation clipPlaneOperation = GviMap.MapControl.ObjectManager.CreateClipPlaneOperation(GviMap.MapControl.ProjectTree.NotInTreeID);
            if (clipPlaneOperation == null)
            {
                Messages.ShowMessage("请先进行切割绘制操作！");
            }
            else if (clipPlaneOperation != null)
            {
                if (this._clipDataColletion == null)
                {
                   // this._dtClipData = this.ConstructTable();

                }
                if (this._clipDataColletion != null)
                {
                    

                    //   ClipData clipmodel = new ClipData();
                        clipPlaneOperation.Name = _newName;// "裁剪面" + (this._dtClipData.Rows.Count + 1);// clipRename.newname;// "裁剪面" + (this._dtClipData.Rows.Count + 1);
                        IVector3 vector = null;
                        IEulerAngle eulerAngle = null;
                        GviMap.MapControl.Camera.GetCamera(out vector, out eulerAngle);
                        CameraProperty camera = new CameraProperty
                        {
                            X = vector.X,
                            Y = vector.Y,
                            Z = vector.Z,
                            Heading = eulerAngle.Heading,
                            Tilt = eulerAngle.Tilt,
                            Roll = eulerAngle.Roll
                        };
                        ClipData clipOperaToData = ClipData.GetClipOperaToData(clipPlaneOperation, camera);
                    _clipDataColletion.Add(clipOperaToData);
                  
                        if (this._curClipOper != null)
                        {
                            this._curClipOper.Cancel();
                            GviMap.MapControl.ObjectManager.DeleteObject(this._curClipOper.Guid);
                            this._curClipOper = null;
                        }

                    
                    //    this.dynamicClip.datagrid.AutoGenerateColumns = false;
                     //   this.dynamicClip.datagrid.ItemsSource = ClipDataColletion;
                        Messages.ShowMessage("保存记录成功！");
                   
                   // dynamicClip.ClipList.DataContext = this._dtClipData;
                   
                }
               // RefreshList();
            }

        }

     
      

     
        public override FrameworkElement CreatedView()
        {
            return new DynamicClip
            {
                Owner = Application.Current.MainWindow
            };
        }
        public override void OnChecked()
        {
            base.OnChecked();
          
            ShowView();

          //  RefreshList();
          
        }
        public override void OnUnchecked()
        {
            /* GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
             GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;// gviSelectFeatureLayer;
             GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
           */
            base.OnUnchecked();
            dynamicClip = (DynamicClip)base.View;
            dynamicClip.Hide();

        }
        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        public void ShowView()
        {
            dynamicClip = (DynamicClip)base.View;

            dynamicClip.DataContext = this;

            dynamicClip.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            dynamicClip.Show();
        }

        private void OnUpdateclipName(string name)
        {
            IVector3 vector = null;
            IEulerAngle eulerAngle = null;
            GviMap.MapControl.Camera.GetCamera(out vector, out eulerAngle);
            CameraProperty camera = new CameraProperty
            {
                X = vector.X,
                Y = vector.Y,
                Z = vector.Z,
                Heading = eulerAngle.Heading,
                Tilt = eulerAngle.Tilt,
                Roll = eulerAngle.Roll
            };
            IClipPlaneOperation clipPlaneOperation = GviMap.MapControl.ObjectManager.CreateClipPlaneOperation(GviMap.MapControl.ProjectTree.NotInTreeID);
            clipPlaneOperation.Name = name;
            ClipData clipOperaToData = ClipData.GetClipOperaToData(clipPlaneOperation, camera);
           
            ClipDataColletion.Add(clipOperaToData);
            Mmc.Mspace.Models.DynamicClipData.ClipData clipData = new Models.DynamicClipData.ClipData();
             clipData.Angle = clipOperaToData.Angle;
            //clipData.BoxCenter = clipOperaToData.BoxCenter;
            clipData.Position = clipOperaToData.Position;
            clipData.BoxSize = clipOperaToData.BoxSize;
            clipData.Guid = clipOperaToData.Guid;
            //  clipData.Camera = clipOperaToData.Camera;
            clipData.Heading = clipOperaToData.Camera.Heading;
            clipData.Roll = clipOperaToData.Camera.Roll;
            clipData.Tilt = clipOperaToData.Camera.Tilt;
            clipData.X = clipOperaToData.Camera.X;
            clipData.Y = clipOperaToData.Camera.Y;
            clipData.Z = clipOperaToData.Camera.Z;
            clipData.Type = clipOperaToData.Type;
            clipData.Name = clipOperaToData.Name;

            //clipData = (Mmc.Mspace.Models.DynamicClipData.ClipData)clipOperaToData;
            if (_localWsCfgSrv!=null)
            {
                _localWsCfgSrv.ClipDatas.Add(clipData);
            }
         
     
        }

        private void OnUpdateObjectReName(string name,string guid)
        {
            var model = ClipDataColletion.FirstOrDefault(t => t.Guid == guid);
            if (model != null)
            {
                model.Name = name;
                Mmc.Mspace.Models.DynamicClipData.ClipData clipData = new Models.DynamicClipData.ClipData();
                clipData.Angle = model.Angle;
                //clipData.BoxCenter = clipOperaToData.BoxCenter;
                clipData.Position = model.Position;
                clipData.BoxSize = model.BoxSize;
                clipData.Guid = model.Guid;
                clipData.Heading = model.Camera.Heading;
                clipData.Roll = model.Camera.Roll;
                clipData.Tilt = model.Camera.Tilt;
                clipData.X = model.Camera.X;
                clipData.Y = model.Camera.Y;
                clipData.Z = model.Camera.Z;
                clipData.Type = model.Type;
               
                clipData.Name = model.Name;
                //var Remodel=_localWsCfgSrv.ClipDatas.FindOne(t => t.Guid == model.Guid);
                if (_localWsCfgSrv!=null)
                {

                    _localWsCfgSrv.ClipDatas.Delete(t => t.Guid == clipData.Guid);
                    _localWsCfgSrv.ClipDatas.Add(clipData);
                }

            }
            Console.WriteLine(name);
        }

    }




}
