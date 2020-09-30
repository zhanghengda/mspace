using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    class QueryEnvelopeManagementVModel:CheckedToolItemModel
    {
        public Action freshCollection;
        public QueryEnvelopeManagementView queryEnvelopeManagementView = new QueryEnvelopeManagementView();
        public ICommand CreateQueryAreaCmd { get; set; }
        public ICommand CloseCmd { get; set; }
        public ICommand DeleteQueryCmd { get; set; }
        public ICommand VisualCmd { get; set; }
        public ICommand EditCmd { get; set; }       
        public ICommand FirstPageCmd { get; set; }
        public ICommand EndPageCmd { get; set; }
        public ICommand LastPageCmd { get; set; }
        public ICommand NextPageCmd { get; set; }
        public ICommand CancelCmd { get; set; }
        
        readonly int PageSizeInt = 10;
        string MaxPageNum;
        Dictionary<string, List<IRenderPolygon>> myDictionary = new Dictionary<string, List<IRenderPolygon>>();
        private ObservableCollection<QueryWktGroup> _queryListCollection = new ObservableCollection<QueryWktGroup>();
        public ObservableCollection<QueryWktGroup> QueryListCollection
        {
            get { return _queryListCollection; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<QueryWktGroup>>(ref this._queryListCollection, value, "QueryListCollection");
            }
        }
        // List<List<IRenderPolygon>> renderPolygons = new List<List<IRenderPolygon>>();
        public void FreshCombox()
        {
            freshCollection();
        }
        public QueryEnvelopeManagementVModel()
        {
            queryEnvelopeManagementView.DataContext = this;
            QueryWktGroupVModel queryWktGroupVModel = null;
           
            this.CreateQueryAreaCmd = new RelayCommand(() =>
            {               
                if (queryWktGroupVModel == null)
                {
                    queryWktGroupVModel = new QueryWktGroupVModel(this);
                    queryWktGroupVModel.freshMainComBox -= new Action (FreshCombox);
                    queryWktGroupVModel.freshMainComBox += new Action(FreshCombox);
                    queryWktGroupVModel.ClearEnvopleDic -= new Action(ClearDictionary);
                    queryWktGroupVModel.ClearEnvopleDic += new Action(ClearDictionary);
                    queryWktGroupVModel.queryWktGroupView.Show();
                   // QueryListCollection = new ObservableCollection<QueryWktGroup>();
                }
                else
                {
                    queryWktGroupVModel.freshMainComBox -= new Action(FreshCombox);
                    queryWktGroupVModel.freshMainComBox += new Action(FreshCombox); 
                    queryWktGroupVModel.ClearEnvopleDic -= new Action(ClearDictionary);
                    queryWktGroupVModel.ClearEnvopleDic += new Action(ClearDictionary);
                    queryWktGroupVModel.queryWktGroupView.Show();
                   // QueryListCollection = new ObservableCollection<QueryWktGroup>();
                }                
            });
            this.CloseCmd = new RelayCommand(() =>
            {
                QueryClose();
            });
            this.DeleteQueryCmd = new RelayCommand<QueryWktGroup>((_delQuery) => DeleteQueryWktGroup(_delQuery));
            this.VisualCmd = new RelayCommand<QueryWktGroup>((_visualQuery) => VisuaQueryWktGroup(_visualQuery));
            this.EditCmd = new RelayCommand<QueryWktGroup>((_editlQuery) => EditName(_editlQuery));            
            this.FirstPageCmd = new RelayCommand(OnFirstPage);
            this.EndPageCmd = new RelayCommand(OnEndPage);
            this.LastPageCmd = new RelayCommand(OnLastPage);
            this.NextPageCmd = new RelayCommand(OnNextPage);
            this.CancelCmd = new RelayCommand(() =>
            {
                QueryClose();
            });
            RefreshCollection(PageSizeInt, PageNum);
        }
        private string _pageCount;
        public string PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; NotifyPropertyChanged("PageCount"); }
        }
        private string _pageNum = "1";
        public string PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; NotifyPropertyChanged("PageNum"); }
        }
        public void QueryClose()
        {
            ClearDictionary();
            queryEnvelopeManagementView.Hide();

        }
        public void QueryOpen()
        {
            GetMaxPageNum();
            OnFirstPage();
            queryEnvelopeManagementView.Show();          
        } 
        public void AddQueryTable(QueryWktGroup _queryWktGroup)
        {
            QueryListCollection.Add(_queryWktGroup);
        }
        public void DelQueryTable(QueryWktGroup _queryWktGroup)
        {
            QueryListCollection.Remove(_queryWktGroup);
        }



    
        public void RefreshCollection(int pageSize, string page)
        {
           // Task.Run(() =>
           // {
                List<QueryWktGroup> areaList = new List<QueryWktGroup>();
                string url = MarkInterface.GetMarkQueryAreaCollectionInf;
                var json = @"{""page_size"":"+ pageSize + @",""page"":"+ page + "}";
                string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
               try
                 {
                    GetQueryMessage(resStr, areaList);
                 }
               catch(Exception e)
                 {
                    SystemLog.WriteLog(e.ToString());
                 }              
                if (areaList?.Count > 0)
                {
                    QueryListCollection?.Clear();
                    foreach (var area in areaList)
                    {                      
                       QueryListCollection?.Add(area);
                    }
                }           
        }      
        public void GetQueryMessage(string _input,List<QueryWktGroup> _areaList)
        {
            if (_input != null && _input != "")
            {
                using (JsonTextReader reader = new JsonTextReader(new StringReader(_input)))
                {

                    JArray jArray = (JArray)JToken.ReadFrom(reader);
                    List<string> wktList = new List<string>();
                    foreach (JObject obj in jArray)
                    {
                        wktList = new List<string>();
                        string objID = obj["id"].ToString();
                        string objName = obj["name"].ToString();
                        JArray objContent = (JArray)obj["content"];
                        //var data = objContent["content"];
                        foreach (JObject enm in objContent)
                        {
                            var content = enm["content"];
                            wktList.Add(content.ToString());
                        }
                        QueryWktGroup queryWktGroup = new QueryWktGroup(objID, objName, wktList);
                        _areaList.Add(queryWktGroup);
                    }
                }
            }
        }
        private void DeleteQueryWktGroup(QueryWktGroup _queryWktGroup)
        {
           var del =  Messages.ShowMessageDialog("删除", "确定删除该区域?");
            if(_queryWktGroup != null&& del)
            {
                DeleteQueryWktGroupOnServer(_queryWktGroup);
                QueryListCollection?.Remove(_queryWktGroup);
                DeleteAllTempObj(myDictionary[_queryWktGroup.ID]);
                myDictionary.Remove(_queryWktGroup.ID);
                OnFirstPage();
            }          
        }
        private void VisuaQueryWktGroup(QueryWktGroup _queryWktGroup)
        {
            List<IRenderPolygon> renderPolygons = new List<IRenderPolygon>();
            if (_queryWktGroup != null)
            {              
                if(myDictionary.ContainsKey(_queryWktGroup.ID))
                {                   
                    DeleteAllTempObj(myDictionary[_queryWktGroup.ID]);                    
                    myDictionary.Remove(_queryWktGroup.ID);
                }
                else
                {                   
                    var tempPolygonNum = _queryWktGroup?.WktStringList.Count;
                    for (int i = 0; i < tempPolygonNum; i++)
                    {
                        var wktString = _queryWktGroup.WktStringList[i];
                        var rPolygon = WktDrawRenderpolygon(wktString);
                        renderPolygons.Add(rPolygon);
                    }
                    myDictionary.Add(_queryWktGroup.ID, renderPolygons);
                    GviMap.Camera.FlyToEnvelope(renderPolygons[0].Envelope,GviMap.SpatialCrs,400000);
                }               
            }            
        }
        EditNameVModel edit = null;
        private void EditName(QueryWktGroup _queryWktGroup)
        {          
                var index = QueryListCollection.IndexOf(_queryWktGroup);
           if(edit!=null)
            {
                edit?.editName.Close();
            }
                edit = new EditNameVModel(index, _queryWktGroup);
                edit.UpdateName -= EditQueryName;
                edit.UpdateName += EditQueryName;
                edit.OpenEditName();
            
          
              
        }
        private void EditQueryName(int _index, QueryWktGroup _queryWktGroup)
        {
            QueryListCollection.RemoveAt(_index);
            QueryListCollection.Insert(_index, _queryWktGroup);
            edit.CloseEditName();
            freshCollection();
        }
        private IRenderPolygon WktDrawRenderpolygon(string _wkt)
        {
            //var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
            //             gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            IPolygon polygon1 = GviMap.GeoFactory.CreateFromWKT(_wkt) as IPolygon;           
            //var render = GviMap.ObjectManager.CreateRenderPolygon(polygon, GviMap.LinePolyManager.SurfaceSym,
            //    GviMap.ProjectTree.RootID);
            //
            var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                         gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            polygon1.SpatialCRS = GviMap.SpatialCrs;
            var render = GviMap.ObjectManager.CreateRenderPolygon(polygon1, GviMap.LinePolyManager.SurfaceSym,
                GviMap.ProjectTree.RootID);
            //
            //polygon.SpatialCRS = GviMap.SpatialCrs;
            render?.SetFdeGeometry(polygon1);
            render.VisibleMask = gviViewportMask.gviViewAllNormalView;
            render.Symbol.BoundarySymbol.Color = Color.FromArgb(255, 0, 255, 255);
            polygon1 = render.GetFdeGeometry() as IPolygon;
            polygon1.SpatialCRS = GviMap.SpatialCrs;
           // GviMap.Camera.FlyToEnvelope(render.Envelope);
            return render;
        }
        private void DeleteAllTempObj(List<IRenderPolygon> _renderPolygons)
        {
            if (_renderPolygons?.Count != 0)
            {
                foreach (var item in _renderPolygons)
                {
                    GviMap.ObjectManager.DeleteObject(item.Guid);
                }
            }
            _renderPolygons?.Clear();
        }
        private void DeleteQueryWktGroupOnServer(QueryWktGroup _queryWktGroup)
        {
            string url = MarkInterface.DeleteQueryWktGroup;
            var json = @"{""id"":" + _queryWktGroup.ID + "}";
            string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
            //var json = @"{""id"":" + _queryWktGroup.ID + "}";
            //string resStr = HttpServiceHelper.Instance.PostRequestForMessage(url, json);          
            JsonTextReader reader = new JsonTextReader(new StringReader(resStr));
            JObject jobj = (JObject)JToken.ReadFrom(reader);
            if (jobj["status"].ToString() == "0")
            {
                Messages.ShowMessage("删除成功");
                freshCollection();
            }
            else
            {
                Messages.ShowMessage("删除失败");
            }
        }
        private void ClearDictionary()
        {
            Dictionary<string, List<IRenderPolygon>>.KeyCollection keyCol = myDictionary.Keys;                           
                 foreach (string s in keyCol)
                {
                DeleteAllTempObj(myDictionary[s]);
                }
            myDictionary.Clear();
        }
        public void OnFirstPage()
        {
            try
            {
                RefreshCollection(PageSizeInt, "1");
                PageNum = "1";
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void OnEndPage()
        {
            try
            {
                if (MaxPageNum != null)
                {
                    RefreshCollection(PageSizeInt, MaxPageNum);
                    PageNum = MaxPageNum.ToString();
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        public void OnLastPage()
        {
            try
            {
                int pageNow = Convert.ToInt32(PageNum);
                if (pageNow > 1)
                {
                    RefreshCollection(PageSizeInt, Convert.ToString(pageNow - 1));
                    PageNum = Convert.ToString(pageNow - 1);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        public void OnNextPage()
        {
            try
            {
                int pageNow = Convert.ToInt32(PageNum);
                if (pageNow < Convert.ToInt32( MaxPageNum))
                {
                    RefreshCollection(PageSizeInt,Convert.ToString(pageNow + 1));
                    PageNum = Convert.ToString(pageNow + 1);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void GetMaxPageNum()
        {
            //string url = MarkInterface.QueryWktGroupListCount;

            //var json = @"{""page_size"":" + PageSizeInt.ToString() + "}";
            //string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
            List<QueryWktGroup> areaList = new List<QueryWktGroup>();           
            string url = MarkInterface.QueryWktGroupListCount;
            var json = @"{""page_size"":" + PageSizeInt + @",""page"":" + "1" + "}";
            string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
            using (JsonTextReader reader = new JsonTextReader(new StringReader(resStr)))
            {
                JObject obj = (JObject)JToken.ReadFrom(reader);
                MaxPageNum = obj["pageNum"].ToString();
                PageCount = Convert.ToString(MaxPageNum);
            }
        }

    }
}
