using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Models.HttpResult;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mmc.Mspace.PoiManagerModule
{
    public class MarkerHelper : Singleton<MarkerHelper>
    {
        private MarksModelsConverter _marksModelsConverter;

        public Action<string> RequestCompleted;
        public Action<int> AccountCount;
        private Dictionary<string, MarkerNew> _markerDic;
        private Dictionary<int, PoiType> _poiTypeDic;
        private Dictionary<string, object> _tagsDic;
        private HttpService _httpService;
        private string _poiHost;

        private int _totalMarkerCounts;
        private bool preIsFirst;
        private string prevLevel;
        private string prevTitle;
        private string prevStartTime;
        private string prevEndTime;
        private string prevGeom;
        private string prepage;
        private string prevTags = "FirstTimeRequest";

        private readonly int REQUESTNUMLIMIT = 50;
        private readonly int MAXDISPLAYNUM = 3000;

        private readonly object _objRequestMarker = new object();
        private readonly object _objGetRequestMarker = new object();

        public int TotalMarkerCounts
        {
            get { return _totalMarkerCounts; }
            private set { _totalMarkerCounts = value; }
        }
        public Dictionary<string, MarkerNew> MarkerDic
        {
            get { return _markerDic; }
            private set { _markerDic = value; }
        }
        public Dictionary<int, PoiType> PoiTypeDic
        {
            get { return _poiTypeDic; }
            private set { _poiTypeDic = value; }
        }
        public Dictionary<string, object> TagsDic
        {
            get { return _tagsDic; }
            private set { _tagsDic = value; }
        }

        public void Initialize()
        {
            _httpService = new HttpService();
            _httpService.Token = HttpServiceUtil.Token;
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            _poiHost = json.poiUrl;
            _markerDic = new Dictionary<string, MarkerNew>();
            _poiTypeDic = this.GetPoiTypeDic();
            _tagsDic = this.GetTagsList();
            _marksModelsConverter = new MarksModelsConverter();
        }
        //(LabelSelectedText.ToString(),searchText, _startDate.ToString(), _endDate.ToString(),WktPoly, LevelSelectedItem);
        public List<MarkerNew> RequestMarkerListBySearchFilter(string tags = "", string searchText = "", string startTime = "", string endTime = "", string geom = "", string level = "", bool loadStatus = false)
        {
            List<MarkerNew> DataList = new List<MarkerNew>();
            try
            {
                Monitor.Enter(_objGetRequestMarker);

                prevLevel = level;
                prevTitle = searchText;
                prevGeom = geom;
                prevTags = tags;
                prevStartTime = startTime;
                prevEndTime = endTime;

                DataList = FreshFilterMarkerData(prevTitle, prevTags, prevStartTime, prevEndTime, prevLevel, prevGeom, loadStatus);
                OnShowMaker(DataList, false);
                return DataList;
            }
            catch (Exception ex)
            {
                return DataList;
            }
            finally
            {
                Monitor.Exit(_objGetRequestMarker);
            }
        }

        public List<MarkerNew> RequestMarkerList(string tags = "", string startTime = "", string endTime = "", string geom = "", bool loadStatus = false)
        {
            List<MarkerNew> DataList = new List<MarkerNew>();
            try
            {
                Monitor.Enter(_objGetRequestMarker);
                this.HideAllRMarkers();
                //GviMap.PoiManager.Clear();
                //GviMap.LinePolyManager.Clear();
                //_markerDic.Clear();
                prevGeom = geom;
                prevTags = tags;
                prevStartTime = startTime;
                prevEndTime = endTime;
                DataList = FreshMarkerData(prevTags, prevStartTime, prevEndTime, prevGeom, loadStatus);
                this.ShowRMarkers(DataList);
                return DataList;
            }
            catch (Exception ex)
            {
                return DataList;
            }
            finally
            {
                Monitor.Exit(_objGetRequestMarker);
            }
        }

        public void ClearDicCache()
        {
            GviMap.PoiManager.Clear();
            GviMap.LinePolyManager.Clear();
            _markerDic.Clear();
            _totalMarkerCounts = 0;
        }
        //(prevTitle, prevTags, prevStartTime, prevEndTime, prevLevel, prevGeom, loadStatus);
        public List<MarkerNew> FreshFilterMarkerData(string titles, string tags, string startTime, string endTime, string level, string geom, bool loadStatus = false)
        {
            List<MarkerNew> DataList = new List<MarkerNew>();
            try
            {
                string lastMemberId = string.Empty;
                int currentPage = 0;
                for (int i = 0; i < MAXDISPLAYNUM; i += REQUESTNUMLIMIT)
                {
                    currentPage++;
                    //if (_markerDic.Count > 0)
                    //    lastMemberId = _markerDic.Keys.Last<string>();
                    if (_totalMarkerCounts < i) return DataList;//?????
                    DataList.AddRange(this.GetFilterMarkerList(lastid: lastMemberId, titles: titles, tags: tags, startTime: startTime, endTime: endTime, level: level, geom: geom, page: currentPage.ToString()));
                    if (loadStatus && i == 0) return DataList;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
            return DataList;
        }

        public List<MarkerNew> FreshMarkerData(string tags, string startTime, string endTime, string geom, bool loadStatus = false)
        {
            List<MarkerNew> DataList = new List<MarkerNew>();
            try
            {
                string lastMemberId = string.Empty;

                for (int i = 0; i < MAXDISPLAYNUM; i += REQUESTNUMLIMIT)
                {
                    //if (_markerDic.Count > 0)
                    //    lastMemberId = _markerDic.Keys.Last<string>();
                    if (_totalMarkerCounts < i) return DataList;
                    DataList.AddRange(this.GetMarkerList(lastid: lastMemberId, tags: tags, startTime: startTime, endTime: endTime, geom: geom));
                    if (loadStatus && i == 0) return DataList;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
            return DataList;
        }

        public string GetCurrentRange()
        {
            string geom = string.Empty;
            try
            {
                var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
                int x = Convert.ToInt32(mapView.ActualWidth / 2);
                int y = Convert.ToInt32(mapView.ActualHeight / 2);

                var LeftTopPoi = GviMap.Camera.ScreenToWorld(1, 1, out IPoint ltp);
                var LeftMiddlePoi = GviMap.Camera.ScreenToWorld(1, y, out IPoint lmp);
                var LeftBottomPoi = GviMap.Camera.ScreenToWorld(1, 2 * y - 1, out IPoint lbp);

                var RightTopPoi = GviMap.Camera.ScreenToWorld(2 * x - 1, 1, out IPoint rtp);
                var RightMiddlePoi = GviMap.Camera.ScreenToWorld(2 * x - 1, y, out IPoint rmp);
                var RightBottomPoi = GviMap.Camera.ScreenToWorld(2 * x - 1, 2 * y - 1, out IPoint rbp);

                var MiddleTopPoi = GviMap.Camera.ScreenToWorld(x, 1, out IPoint mtp);
                var CenterPoi = GviMap.Camera.ScreenToWorld(x, y, out IPoint cp);
                var MiddleBottomPoi = GviMap.Camera.ScreenToWorld(x, 2 * y - 1, out IPoint mbp);

                if (LeftBottomPoi == null || RightBottomPoi == null || MiddleBottomPoi == null) return "";

                var yy = mbp.Y - lbp.Y + rbp.Y - mbp.Y;
                var xx = mbp.X - lbp.X + rbp.X - mbp.X;
                if ((Math.Abs(yy) + Math.Abs(xx)) > 0.08) return "";

                var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
    gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon.SpatialCRS = GviMap.SpatialCrs;
                var exteriorRing = polygon.ExteriorRing;

                if (LeftTopPoi == null || RightTopPoi == null)
                {
                    var ltpNew = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    var rtpNew = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);

                    ltpNew.SetPostion(lbp.X, (lbp.Y - yy));
                    rtpNew.SetPostion(rbp.X, (rbp.Y - yy));

                    exteriorRing.AppendPoint(ltpNew);
                    exteriorRing.AppendPoint(rtpNew);
                    exteriorRing.AppendPoint(rbp);
                    exteriorRing.AppendPoint(lbp);
                    exteriorRing.AppendPoint(ltpNew);
                }
                else
                {
                    exteriorRing.AppendPoint(ltp);
                    exteriorRing.AppendPoint(rtp);
                    exteriorRing.AppendPoint(rbp);
                    exteriorRing.AppendPoint(lbp);
                    exteriorRing.AppendPoint(ltp);
                }

                geom = polygon.AsWKT();
                Console.WriteLine(geom);
            }
            catch (Exception)
            {
            }
            return geom;
        }

        private List<MarkerNew> GetFilterMarkerList(string lastid = "", string titles = "", string tags = "", string startTime = "", string endTime = "", string level = "", string page = "", string geom = "")
        {
            var postMarkers = new List<PostMarkerNew>();
            List<MarkerNew> DataList = new List<MarkerNew>();

            //string api = string.Format("{0}?limit={1}", MarkInterface.GetMarksListInf, REQUESTNUMLIMIT);
            string api = string.Format("{0}?limit={1}", MarkInterface.GetFilterMarksListInf, REQUESTNUMLIMIT);

            if (!string.IsNullOrEmpty(titles))
                api += string.Format("&title={0}", titles);
            if (!string.IsNullOrEmpty(level))
                api += string.Format("&level={0}", level);
            if (!string.IsNullOrEmpty(geom))
                api += string.Format("&geom={0}", geom);
            if (!string.IsNullOrEmpty(lastid))
                api += string.Format("&marker_id={0}", lastid);
            if (!string.IsNullOrEmpty(tags))
                api += string.Format("&tags={0}", tags);
            if (!string.IsNullOrEmpty(startTime))
                api += string.Format("&startTime={0}", startTime);
            if (!string.IsNullOrEmpty(endTime))
                api += string.Format("&endTime={0}", endTime);
            if (!string.IsNullOrEmpty(page))
                api += string.Format("&page={0}", page);
            string resStr = HttpServiceHelper.Instance.GetRequest(api);
            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);

            if (string.IsNullOrEmpty(lastid))
                _totalMarkerCounts = resDyn.total;

            //var resDataStr = JsonUtil.SerializeToString(resDyn.list);
            //DataList = JsonUtil.DeserializeFromString<List<MarkerModel>>(resDataStr);
            //foreach (var item in DataList)
            //{
            //    if (item.type == 1 && _poiTypeDic.ContainsKey(item.cat_id))
            //        item.cat_Name = _poiTypeDic[item.cat_id].cat_name;
            //    if (!_markerDic.ContainsKey(item.id.ToString()))
            //    {
            //        _markerDic.Add(item.id.ToString(), item);
            //        RenderMarker(item);
            //    }
            //}

            var resDataStr = JsonUtil.SerializeToString(resDyn.list);
            postMarkers = JsonUtil.DeserializeFromString<List<PostMarkerNew>>(resDataStr);
            foreach (var marker in postMarkers)
            {
                var markerNew = _marksModelsConverter.MarkerConverting(marker);
                DataList.Add(markerNew);

                if (!_markerDic.ContainsKey(markerNew.MarkerId.ToString()))
                {
                    _markerDic.Add(markerNew.MarkerId.ToString(), markerNew);
                    //RenderMarker(markerNew);
                }
            }



            return DataList;
        }

        private List<MarkerNew> GetMarkerList(string lastid = "", string tags = "", string startTime = "", string endTime = "", string geom = "")
        {
            var postMarkers = new List<PostMarkerNew>();
            List<MarkerNew> DataList = new List<MarkerNew>();

            //string api = string.Format("{0}?limit={1}", MarkInterface.GetMarksListInf, REQUESTNUMLIMIT);
            string api = string.Format("{0}?limit={1}", MarkInterface.GetFilterMarksListInf, REQUESTNUMLIMIT);

            if (!string.IsNullOrEmpty(geom))
                api += string.Format("&geom={0}", geom);
            if (!string.IsNullOrEmpty(lastid))
                api += string.Format("&marker_id={0}", lastid);
            if (!string.IsNullOrEmpty(tags))
                api += string.Format("&tags={0}", tags);
            if (!string.IsNullOrEmpty(startTime))
                api += string.Format("&startTime={0}", startTime);
            if (!string.IsNullOrEmpty(endTime))
                api += string.Format("&endTime={0}", endTime);
            string resStr = HttpServiceHelper.Instance.GetRequest(api);
            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);

            if (string.IsNullOrEmpty(lastid))
                _totalMarkerCounts = resDyn.total;

            var resDataStr = JsonUtil.SerializeToString(resDyn.list);
            postMarkers = JsonUtil.DeserializeFromString<List<PostMarkerNew>>(resDataStr);
            foreach (var marker in postMarkers)
            {
                var markerNew = _marksModelsConverter.MarkerConverting(marker);
                DataList.Add(markerNew);
                //if (marker.Type == 1 && _poiTypeDic.ContainsKey(marker.CatId))
                //    marker.cat_Name = _poiTypeDic[marker.cat_id].cat_name;
                if (!_markerDic.ContainsKey(markerNew.MarkerId.ToString()))
                {
                    _markerDic.Add(markerNew.MarkerId.ToString(), markerNew);
                    RenderMarker(markerNew);
                }
            }
            return DataList;
        }


        public void UpdateMarksTages()
        {
            string resStr = HttpServiceHelper.Instance.GetRequest(MarkInterface.GetMarksTagsUpdateInf);
        }



        public void UpdateMarkerList(MarkerNew marker)
        {
            marker.IsChecked = true;
            if (!_markerDic.ContainsKey(marker.MarkerId.ToString()))
                _markerDic.Add(marker.MarkerId.ToString(), marker);
            else
                _markerDic[marker.MarkerId.ToString()] = marker;

            RenderMarker(marker);
        }

        public bool DeleteMarker(List<string> markersList)
        {
            bool isSuccess = false;
            //从服务中删除
            var api = MarkInterface.DeleteMarkInf;
            var postStr = JsonUtil.SerializeToString(new { marker_id = markersList });
            bool success = HttpServiceHelper.Instance.PostRequestForStatus(api, postStr);
            if (success)
            {
                isSuccess = true;
                _totalMarkerCounts--;
                foreach (var ii in markersList)
                    _markerDic.Remove(ii);
            }
            return isSuccess;
        }

        /// <summary>
        /// 获取点标注类型
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, PoiType> GetPoiTypeDic()
        {
            var dic = new Dictionary<int, PoiType>();
            string url = MarkInterface.PoiTypesListInf;

            string data = HttpServiceHelper.Instance.GetRequestAsync(url);
            if (!string.IsNullOrEmpty(data))
            {
                var poiTypesList = JsonUtil.DeserializeFromString<List<PoiType>>(data);
                foreach (var item in poiTypesList)
                {
                    dic.Add(item.cat_id, item);
                }
            }
            return dic;
        }

        public AccountNew GetAccountByMarkerId(int markerId)
        {
            AccountNew AccountNew = new AccountNew();

            int result = 0;
            string url = string.Format("{0}/api/account/row?", _poiHost);

            url += string.Format("id={0}", markerId);
            string resStr = _httpService.HttpRequestAsync(url);
            var resDyn = JsonUtil.DeserializeFromFile<HttpResultModel>(resStr);
            if (resDyn.message == "ok")
            {
                result = Convert.ToInt32(resDyn.status);
                //PostAccountNew re = JsonConvert.DeserializeObject<AccountModel>(JsonUtil.SerializeToString(resDyn.data)); 
                //return AccountModel = _marksModelsConverter.AccountConverting(re);
            }
            return AccountNew;
        }


        public bool UpdateAccountListOfMark(int accountId, AccountNew AccountNew)
        {
            List<AccountNew> result = new List<AccountNew>();

            //if (markerId < 0) return null;
            string url = string.Format("{0}?id={1}", MarkInterface.UpdateAccountInf, accountId);

            var postaccount = _marksModelsConverter.AccountConverting(AccountNew);
            var jsonData = JsonUtil.SerializeToString(postaccount);
            bool isSuccess = HttpServiceHelper.Instance.PostRequestForStatus(url, jsonData);


            //string resStr = HttpServiceHelper.Instance.GetRequest(url);

            //var list = JsonUtil.DeserializeFromString<List<PostAccountNew>>(resStr);

            //if (list?.Count > 0)
            //{
            //    foreach (var account in list)
            //    {
            //        result.Add(_marksModelsConverter.AccountConverting(account));
            //    }
            //}

            return isSuccess;
        }

        public List<AccountNew> GetAccountListOfMark(int markerId, int pageSize, int page)
        {
            List<AccountNew> result = new List<AccountNew>();

            if (markerId < 0) return null;
            string url = string.Format("{0}?marker_id={1}&limit={2}&page={3}", MarkInterface.GetAccountsListInf,
                markerId, pageSize, page);

            //string paras = JsonUtil.SerializeToString(new { marker_id = markerId ,pageSize =pageSize,page=page});
            string resStr = HttpServiceHelper.Instance.GetRequest(url);
            //if (!string.IsNullOrEmpty(resStr))
            //{
            var list = JsonUtil.DeserializeFromString<List<PostAccountNew>>(resStr);

            if (list?.Count > 0)
            {
                foreach (var account in list)
                {
                    result.Add(_marksModelsConverter.AccountConverting(account));
                }
            }
            //}

            return result;
        }

        /// <summary>
        ///json解析对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象实体</returns>
        //public static T JsonDe<T>(string json)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(json))
        //            return default(T);
        //        return JsonConvert.DeserializeObject<T>(json);
        //    }
        //    catch (Exception e)
        //    {
        //        return default(T);
        //    }
        //}
        //public static string JsonSe( object obj)
        //{
        //    try
        //    {
        //        return JsonConvert.SerializeObject(obj);
        //   }
        //    catch (Exception e)
        //    {
        //        return string.Empty;
        //    }
        //}
        public bool AddAccount(AccountNew AccountNew)
        {
            string addAccountApi = MarkInterface.AddAccountInf;
            //var postaccount = AccountConvert(AccountModel);
            var postaccount = _marksModelsConverter.AccountConverting(AccountNew);
            var jsonData = JsonUtil.SerializeToString(postaccount);
            //string resStr = this._httpService.PostJsonData(addAccountApi, jsonData);

            bool isSuccess = HttpServiceHelper.Instance.PostRequestForStatus(addAccountApi, jsonData);
            //var resDyn = JsonDe<HttpResultModel>(resStr);
            return isSuccess;
        }

        public bool DeleteAccount(int id)
        {
            string deleteAccountApi = string.Format("{0}?id={1}", MarkInterface.DeleteAccountInf, id);

            var jsonData = JsonUtil.SerializeToString(new { id });

            bool isSuccess = HttpServiceHelper.Instance.PostRequestForStatus(deleteAccountApi, jsonData);
            return isSuccess;
        }
        //private AccountModel AccountConvert(PostAccountModel AccountModel)
        //{
        //    AccountModel postAccountModel = new AccountModel();
        //    postAccountModel.Id = AccountModel.id;
        //    postAccountModel.MarkerId = AccountModel.marker_id;
        //    postAccountModel.Title = AccountModel.title;
        //    postAccountModel.Status = AccountModel.status;
        //    postAccountModel.Site = AccountModel.site;
        //    postAccountModel.Img = AccountModel.img;
        //    postAccountModel.Video = AccountModel.video;
        //    postAccountModel.Operator = AccountModel._operator;
        //    postAccountModel.Area = AccountModel.area;
        //    postAccountModel.AddedTime = AccountModel.addtime;
        //    postAccountModel.OperatorPhone = AccountModel.operator_phone;
        //    return postAccountModel;
        //}
        //private PostAccountModel AccountConvert(AccountModel AccountModel)
        //{
        //    PostAccountModel postAccountModel = new PostAccountModel();
        //    postAccountModel.id = AccountModel.Id;
        //    postAccountModel.marker_id = AccountModel.MarkerId;
        //    postAccountModel.title = AccountModel.Title;
        //    postAccountModel.status = AccountModel.Status;
        //    postAccountModel.site = AccountModel.Site;
        //    postAccountModel.img = AccountModel.Img;
        //    postAccountModel.video = AccountModel.Video;
        //    postAccountModel._operator = AccountModel.Operator;
        //    postAccountModel.area = AccountModel.Area;
        //    postAccountModel.addtime = AccountModel.AddedTime;
        //    postAccountModel.operator_phone = AccountModel.OperatorPhone;
        //    return postAccountModel;
        //}

        public int AddOrUpdateMarkerNew(PostMarkerNew markerNew, string api, out string title)
        {
            string imgpath = string.Format("{0}/resource", WebConfig.MspaceHostUrl);
            if (markerNew != null && markerNew.img != null && markerNew.img.ToLower().Contains(imgpath))
            {
                markerNew.img = markerNew.img.Replace(imgpath, "");
            }

            int markerId = 0;
            var jsonData = JsonUtil.SerializeToString(markerNew);
            string resStr = HttpServiceHelper.Instance.PostRequestForData(api, jsonData);
            var resDyn = JsonUtil.DeserializeFromString<PostMarkerNew>(resStr);
            if (resDyn != null)
            {
                markerId = Convert.ToInt32(resDyn.marker_id);
                title = resDyn.title;
            }
            else { title = string.Empty; }
            return markerId;
        }



        public string updateCaptureImg(string _imgPath)
        {
            string img = string.Empty;
            string resStr = HttpServiceHelper.Instance.PostImageFile("/api/upload-form/getloadfile", _imgPath);
            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
            img = resDyn.img_url;
            return img;
        }

        public Dictionary<string, object> GetTagsList(string name = "")
        {
            Dictionary<string, object> temp = new Dictionary<string, object>();
            string tagListApi = MarkInterface.GetTagsListInf;
            if (!string.IsNullOrEmpty(name))
                tagListApi += string.Format("&name={0}", name);
            string resAlltags = HttpServiceHelper.Instance.GetRequestAsync(tagListApi);
            var taglist = JsonConvert.DeserializeObject<List<TagItem>>(resAlltags);

            foreach (var tag in taglist)
                temp.Add(tag.id.ToString(), (object)tag.name);
            return temp;
        }


        public List<TagItem> GetTagsSource(string name = "")
        {
            List<TagItem> temp = new List<TagItem>();
            string tagListApi = MarkInterface.GetTagsListInf;

            if (!string.IsNullOrEmpty(name))
                tagListApi += string.Format("?name={0}", name);
            string resAlltags = HttpServiceHelper.Instance.GetRequestAsync(tagListApi);

            temp = JsonConvert.DeserializeObject<List<TagItem>>(resAlltags);
            return temp;
        }

        public dynamic GetTagTypesSource(int pageSize = 10, int page = 0)
        {
            string tagTypeApi = "/api/tag/taggrouplist";

            var dic = new Dictionary<string, int>();
            dic.Add("page_size", pageSize);
            dic.Add("page", page);

            string json = JsonUtil.SerializeToString(dic);

            string res = HttpServiceHelper.Instance.PostRequestForData(tagTypeApi, json);

            return JsonConvert.DeserializeObject<dynamic>(res);
        }

        public bool AddTag(string name)
        {
            bool result = false;
            string addTagUrl = MarkInterface.AddTagInf;
            if (!string.IsNullOrEmpty(name))
                addTagUrl += string.Format("?name={0}", name);

            result = HttpServiceHelper.Instance.GetRequestForStatus(addTagUrl, name);

            return result;
        }

        public bool EditTag(string id, string tag)
        {
            bool result = false;
            string editTagApi = MarkInterface.UpdateTagInf;
            if (!string.IsNullOrEmpty(id))
                editTagApi += string.Format("?id={0}", id);
            if (!string.IsNullOrEmpty(tag))
                editTagApi += string.Format("&name={0}", tag);

            //result = HttpServiceHelper.Instance.PostRequestForStatus(editTagApi, tag);
            result = HttpServiceHelper.Instance.GetBoolRequest(editTagApi);

            return result;
        }

        public bool DeleteTag(string id)
        {
            bool result = false;
            string deleteTag = MarkInterface.DeleteTagInf;
            deleteTag += string.Format("?id={0}", id);

            result = HttpServiceHelper.Instance.PostRequestForStatus(deleteTag, id);
            return result;
        }

        public void PostMarkerTags(string jsonStr)
        {
            var addTagToPoi = MarkInterface.PoiAddTagInf;
            HttpServiceHelper.Instance.PostRequestForData(addTagToPoi, jsonStr);
        }
        public bool DeleteMarkerTag(string markerId, string tagId)
        {
            string dropPoiTag = MarkInterface.PoiDeleteTagInf;

            string postStr = JsonUtil.SerializeToString(new { marker_id = markerId, tag_id = tagId });
            bool success = HttpServiceHelper.Instance.PostRequestForStatus(dropPoiTag, postStr);

            if (success)
            {
                var marker = _markerDic[markerId];
                var tag = marker.Tags.ToList().Find(p => p.id.ToString() == tagId);
                marker.Tags.Remove(tag);
                RenderMarker(marker);
            }
            return success;
        }

        public void OnShowMaker(List<MarkerNew> DataList, bool ischecked)
        {
            try
            {
                if (DataList.Count > 0)
                    foreach (var marker in DataList)
                    {
                        if (ischecked && !GviMap.PoiManager.ContainsKey(marker.MarkerId.ToString()) && !GviMap.LinePolyManager.ContainsKey(marker.MarkerId.ToString()))
                        {
                            RenderMarker(marker);


                        }

                        if (marker.Type == 1)
                        {

                            GviMap.PoiManager.SetVisible(marker.MarkerId.ToString(), PoiTypeDic[marker.CatId].cat_name, ischecked);
                        }
                        else if (marker.Type == 2 || marker.Type == 3)
                        {
                            GviMap.LinePolyManager.SetPoiVisible(marker.MarkerId.ToString(), ischecked);
                        }
                    }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void HideAllRMarkers()
        {
            // 全部隐藏
            if (MarkerDic.Count > 0)
                foreach (var marker in MarkerDic.Values.ToList())
                {
                    //if (marker.CatId == 0) marker.CatId = 1;
                    GviMap.PoiManager.SetVisible(marker.MarkerId.ToString(), this._poiTypeDic[marker.CatId].cat_name, false);
                    GviMap.LinePolyManager.SetPoiVisible(marker.MarkerId.ToString(), false);
                }
        }

        public void ShowRMarkers(List<MarkerNew> markers)
        {
            if (markers.Count > 0)
                foreach (var marker in markers)
                {
                    if (marker.CatId == 0) marker.CatId = 1;
                    GviMap.PoiManager.SetVisible(marker.MarkerId.ToString(), this._poiTypeDic[marker.CatId].cat_name, true);
                    GviMap.LinePolyManager.SetPoiVisible(marker.MarkerId.ToString(), true);
                }
        }


        public void RenderMarker(MarkerNew marker)
        {
            string markerId = marker.MarkerId.ToString();
            string title = marker.Title;
            int type = marker.Type;
            string style = marker.Style;
            string geom = marker.Geom;
            int cat_id = marker.CatId == 0 ? 1 : marker.CatId;
            // 存在先删除
            if (GviMap.PoiManager.ContainsKey(markerId))
                GviMap.PoiManager.DeletePoi(markerId);
            // 存在先删除
            if (GviMap.LinePolyManager.ContainsKey(markerId))
                GviMap.LinePolyManager.DeletePoi(markerId);

            //if (GviMap.PoiManager.ContainsKey(markerId)||GviMap.LinePolyManager.ContainsKey(markerId)) return;
            try
            {
                Monitor.Enter(_objRequestMarker);
                RenderGeometryStyle styleRender = null;
                if (type == 1 && geom.Contains("POINT"))
                {

                    var poi = GviMap.PoiManager.CreatePoi(geom, this._poiTypeDic[cat_id].cat_url, title);
                    var rPoi = GviMap.PoiManager.CreateRPoi(poi);
                    rPoi.MaxVisibleDistance = WebConfig.LabelMaxDistance;
                    GviMap.PoiManager.AddPoi(markerId.ToString(), this._poiTypeDic[cat_id].cat_name, rPoi);

                    //var  label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
                    //label.VisibleMask = gviViewportMask.gviViewNone;
                    //var textSym = label.TextSymbol;
                    //textSym.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomLeft;
                    //label.TextSymbol = textSym;
                    if (_markerDic.Count == 0) return;
                    _markerDic[markerId].Guid = rPoi.Guid.ToString();
                }
                else if (type == 2 && geom.Contains("LINESTRING"))
                {

                    var polyLine = GviMap.GeoFactory.CreatePolyline(geom, GviMap.SpatialCrs);
                    ICurveSymbol CurveSym = null;
                    if (string.IsNullOrEmpty(style))
                        CurveSym = GviMap.LinePolyManager.CurveSym;
                    else
                    {
                        if (style.Contains("HeightStyle"))
                        {
                            styleRender = JsonUtil.DeserializeFromString<RenderGeometryStyle>(style);
                            CurveSym = GviMap.ObjectManager.CreateGeometrySymbolFromXML(styleRender.GeoSymbolXml) as ICurveSymbol;
                        }
                        else
                        {
                            CurveSym = GviMap.ObjectManager.CreateGeometrySymbolFromXML(style) as ICurveSymbol;
                        }

                    }
                    var rLine = GviMap.ObjectManager.CreateRenderPolyline(polyLine, CurveSym);
                    rLine.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                    if (styleRender != null)
                        rLine.HeightStyle = styleRender.HeightStyle;
                    var label = GviMap.ObjectManager.CreateLabel(polyLine.Midpoint);
                    label.DepthTestMode = gviDepthTestMode.gviDepthTestAlways;
                    label.Text = title;
                    label.MaxVisibleDistance = WebConfig.LabelMaxDistance;
                    GviMap.LinePolyManager.AddPoi(markerId.ToString(), type, label, rLine);
                    if (_markerDic.Count == 0) return;
                    _markerDic[markerId].Guid = rLine.Guid.ToString();
                }
                else if (type == 3 && geom.Contains("POLYGON"))
                {
                    var polygon = GviMap.GeoFactory.CreatePolygon(geom, GviMap.SpatialCrs);
                    ISurfaceSymbol surSym = null;
                    if (string.IsNullOrEmpty(style))
                        surSym = GviMap.LinePolyManager.SurfaceSym;
                    else
                    {
                        if (style.Contains("HeightStyle"))
                        {
                            styleRender = JsonUtil.DeserializeFromString<RenderGeometryStyle>(style);
                            surSym = GviMap.ObjectManager.CreateGeometrySymbolFromXML(styleRender.GeoSymbolXml) as ISurfaceSymbol;
                        }
                        else
                        {
                            surSym = GviMap.ObjectManager.CreateGeometrySymbolFromXML(style) as ISurfaceSymbol;
                        }
                    }
                    var rPolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, surSym);
                    rPolygon.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                    if (styleRender != null)
                        rPolygon.HeightStyle = styleRender.HeightStyle;

                    var label = GviMap.ObjectManager.CreateLabel(polygon.Envelope.Center.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs));
                    label.DepthTestMode = gviDepthTestMode.gviDepthTestAlways;
                    label.MaxVisibleDistance = WebConfig.LabelMaxDistance;
                    label.Text = title;
                    GviMap.LinePolyManager.AddPoi(markerId.ToString(), type, label, rPolygon);
                    if (_markerDic.Count == 0) return;
                    _markerDic[markerId].Guid = rPolygon.Guid.ToString();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            finally
            {
                Monitor.Exit(_objRequestMarker);
            }
        }

    }
}
