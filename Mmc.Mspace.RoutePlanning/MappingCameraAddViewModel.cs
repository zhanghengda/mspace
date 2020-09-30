using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RoutePlanning.Grid;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Framework.Commands;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.RoutePlanning
{
    class MappingCameraAddViewModel: CheckedToolItemModel
    {
        public MappingCameraAddView _mappingCameraAddView;
        public List<MappingCamera> mappingCameraList;

        /// <summary>
        /// 相机名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 焦距
        /// </summary>
        public string Focus { get; set; }
        /// <summary>
        /// 像幅长（我写前端的，所以用width）
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// 像幅宽
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 相机保存
        /// </summary>
        [XmlIgnore]
        public ICommand cmdSaveMappingCamera { get; set; }
        public RoutePlanShowPageViewModel form = null;
        public void getForm(RoutePlanShowPageViewModel f)
        {
            form = f;
        }

        public override void Initialize()
        {
            base.Initialize();
           
            base.ViewType = ViewType.CheckedIcon;
            cmdSaveMappingCamera = new RelayCommand(() =>
            {
                MappingCamera mappingCamera = new MappingCamera();
                if (Name == null) { Messages.ShowMessage("相机名称不能为空！"); return; }
                if (Focus == null) { Messages.ShowMessage("焦距不能为空！"); return; }
                if (Width == null) { Messages.ShowMessage("像幅长不能为空！"); return; }
                if (Height == null) { Messages.ShowMessage("像幅宽不能为空！"); return; }
                mappingCamera.Name = Name;
                mappingCamera.Focus = double.Parse(Focus);
                mappingCamera.Width = double.Parse(Width);
                mappingCamera.Height = double.Parse(Height);
                mappingCameraList.Add(mappingCamera);
                string filePath = AppDomain.CurrentDomain.BaseDirectory + ConfigPath.MappingCameraConfig;
                bool status = JsonUtil.SerializeToFile(filePath, mappingCameraList);             
                if (status)
                {
                    RoutePlanShowPageViewModel viewModel = new RoutePlanShowPageViewModel();
                    viewModel.OnChecked();
                    form.OnUnchecked();
                    releaseWindow();
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("MappingCameraAdded"));
                }
               
               
               // MappingCameraAddViewModel _mappingCameraAddViewModel = new MappingCameraAddViewModel();
               //   _mappingCameraAddView.Close();

                /*this.mappingCameraList.Clear();
                var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.MappingCameraConfig);
                for (int i = 0; i < json.Count; i++)
                {
                    MappingCamera mappingCamera_new = new MappingCamera();
                    mappingCamera_new.Name = json[i].Name;
                    mappingCamera_new.Focus = json[i].Focus;
                    mappingCamera_new.Width = json[i].Width;
                    mappingCamera_new.Height = json[i].Height;
                    this.mappingCameraList.Add(mappingCamera_new);
                }*/
                // this.mappingCameraList.
            });
        }
       
        public override void OnChecked()
        {
            base.OnChecked();
            if (_mappingCameraAddView == null)
            {
                _mappingCameraAddView = new MappingCameraAddView();
                _mappingCameraAddView.Owner = Application.Current.MainWindow;
                _mappingCameraAddView.Closed += (sender, e) =>
                {
                    _mappingCameraAddView = null;
                };
            }
            _mappingCameraAddView.DataContext = this;
            Name = "";
            Focus = "";
            Width = "";
            Height = "";
            if (!_mappingCameraAddView.IsVisible)
            { 
                _mappingCameraAddView.WindowStartupLocation = WindowStartupLocation.Manual;
                _mappingCameraAddView.Left = 450;
                _mappingCameraAddView.Top = 30;
                _mappingCameraAddView.Show();
            }
        }
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            if (_mappingCameraAddView != null)
            {
                _mappingCameraAddView.Hide();
            }
        }
        public void releaseWindow()
        {
            OnUnchecked();
            _mappingCameraAddView = null;
        }
    }
}
