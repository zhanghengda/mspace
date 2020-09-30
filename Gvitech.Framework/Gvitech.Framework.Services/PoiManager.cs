using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System.Collections.Generic;
using System.Linq;

namespace Mmc.Framework.Services
{
    public class PoiManager
    {
        private Dictionary<string, Dictionary<string, IRenderPOI>> _poiHashMap;
        public readonly string DefindMarkId = "DefindMarkId";
        private IPOI _poi;
        public IRenderPOI TempRPoi { get; set; }

        public PoiManager()
        {
            _poiHashMap = new Dictionary<string, Dictionary<string, IRenderPOI>>();
            _poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;

        }

        ///// <summary>
        ///// 飞入POI
        ///// </summary>
        ///// <param name="tag"></param>
        ///// <param name="key"></param>
        //public void FlyTo(string tag, string key)
        //{
        //    var rpoi = GetRPOI(tag, key);
        //    if (rpoi != null)
        //        GviMap.Camera.LookAtEnvelope(rpoi.Envelope);
        //}

        /// <summary>
        /// 飞入POI
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="key"></param>
        public void FlyTo(string key)
        {
            foreach (var tag in _poiHashMap.Keys)
            {
                if (_poiHashMap[tag].ContainsKey(key))
                {
                    var rPoi = _poiHashMap[tag][key];
                    if (rPoi != null)
                    {

                        var poi = rPoi.GetFdeGeometry() as IPoint;

                        GviMap.Camera.GetCamera2(out IPoint Position, out IEulerAngle Angle);

                        GviMap.Camera.LookAt2(poi, 200, Angle);

                        //GviMap.Camera.LookAtEnvelope(rPoi.Envelope);
                        ((IRenderGeometry)rPoi).Glow(1500);


                    }
                }
            }

        }

        /// <summary>
        /// 获取poi渲染对象
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IRenderPOI GetRPOI(string tag, string key)
        {
            if (_poiHashMap.ContainsKey(tag))
                if (_poiHashMap[tag].ContainsKey(key))
                    return _poiHashMap[tag][key];

            return null;
        }

        /// <summary>
        /// 创建临时标注点
        /// </summary>
        public void CreateTempRPoi()
        {
            _poi.SpatialCRS = GviMap.SpatialCrs;
            TempRPoi = GviMap.ObjectManager.CreateRenderPOI(_poi);
            TempRPoi.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;//禁用深度检测（全能看到）
        }
        public bool ContainsKey(string tag, string key)
        {
            return _poiHashMap.ContainsKey(tag) && _poiHashMap[tag].ContainsKey(key);
        }

        public bool ContainsKey(string key)
        {
            if (_poiHashMap.Count == 0) return false;
            foreach (var tag in _poiHashMap.Keys)
            {
                if (_poiHashMap[tag].ContainsKey(key))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 创建渲染Rpoi
        /// </summary>
        /// <param name="poi"></param>
        /// <returns></returns>
        public IRenderPOI CreateRPoi(IPOI poi)
        {
            var rPoi = GviMap.ObjectManager.CreateRenderPOI(poi);
            rPoi.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
            rPoi.Name = poi.Name;
            return rPoi;
        }
        /// <summary>
        /// 新增标注
        /// </summary>
        /// <param name="key">poi标注Id</param>
        /// <param name="tag">标签类型</param>
        /// <param name="rpoi"></param>
        public void AddPoi(string key, string tag, IRenderPOI rpoi)
        {
            Dictionary<string, IRenderPOI> poiMaps;
            if (!_poiHashMap.ContainsKey(tag))
            {
                poiMaps = new Dictionary<string, IRenderPOI>();
                _poiHashMap.Add(tag, poiMaps);
            }
            else
                poiMaps = _poiHashMap[tag];

            if (poiMaps != null && !poiMaps.ContainsKey(key))
                poiMaps?.Add(key, rpoi);
        }

        public IPOI CreatePoi(double x, double y, double z, string iconPath, string title, int size = 32)
        {
            var poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
            poi.SetPostion(x, y, z);
            poi.ImageName = iconPath;
            poi.Size = size;
            poi.ShowName = true;
            poi.Name = title;
            poi.SpatialCRS = GviMap.SpatialCrs;
            return poi;
        }

        public IPOI CreatePoi(string geom, string iconPath, string title, int size = 32)
        {
            var poi1 = GviMap.GeoFactory.CreateFromWKT(geom) as IPoint;
            //var poi = poi1 as IPOI
            var poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
            poi.SetPostion(poi1.X, poi1.Y,poi1.Z);
            poi.ImageName = iconPath;
            poi.Size = size;
            poi.ShowName = true;
            poi.Name = title;
            poi.SpatialCRS = GviMap.SpatialCrs;
            return poi;
        }


        public void SetVisible(string key, string tag, bool isVisible)
        {
            if (_poiHashMap.ContainsKey(tag))
                if (_poiHashMap[tag].ContainsKey(key))
                    _poiHashMap[tag][key].VisibleMask = isVisible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetPoiTags()
        {
            return _poiHashMap.Keys.ToList<string>();
        }
        /// <summary>
        /// 根据标签类别控制poi的显示隐藏
        /// </summary> /// <param name="poiTag"></param>
        /// <param name="visibleMask">
        /// 这个值是以位来表示那些视口是可见的，
        /// 例如对二进制数字0101，表示的就是第一个和第三个视图科技那，对应的mask应设置为十进制的5。
        /// 为了方便理解可以这样记忆，1、2、4、8分别表示第一个到第四个视图的对应数字，
        /// 如果希望第一个和第三个视图的相机相互绑定，就设置mask为5（1+4）；
        /// 如果希望第一、二、四个视图相互绑定时，就设置mask为11（1+2+8）；
        /// 如果希望四个视图都绑定时则设置为15（1+2+4+8）
        /// </param>
        public void SetVisibleByTag(string poiTag, int visibleMask)
        {
            if (_poiHashMap.ContainsKey(poiTag))
            {
                var poiMap = _poiHashMap[poiTag];
                foreach (var key in poiMap.Keys)
                {
                    if (visibleMask == 0)
                        poiMap[key].VisibleMask = gviViewportMask.gviViewNone;
                    else if (visibleMask == 15)
                        poiMap[key].VisibleMask = gviViewportMask.gviViewAllNormalView;
                    else if (visibleMask == 1)
                        poiMap[key].VisibleMask = gviViewportMask.gviView0;
                    else if (visibleMask == 2)
                        poiMap[key].VisibleMask = gviViewportMask.gviView1;
                    else if (visibleMask == 3)
                        poiMap[key].VisibleMask = gviViewportMask.gviView0 | gviViewportMask.gviView1;
                }
            }
        }

        /// <summary>
        /// 设置poi在哪个视图可见
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tag"></param>
        /// <param name="viewPort">视图序号，0代表第一视口，1代表第二视口，依此类推</param>
        /// <param name="isVisible">是否可见</param>
        public void SetVisibleByViewPort(string key, string tag, int viewPort, bool isVisible)
        {
            if (_poiHashMap.ContainsKey(tag))
            {
                if (_poiHashMap[tag].ContainsKey(key))
                {
                    var rpoi = _poiHashMap[tag][key];
                    SetPoiVisibleByViewPort(viewPort, isVisible, rpoi);
                }
            }
        }

        private void SetPoiVisibleByViewPort(int viewPort, bool isVisible, IRenderPOI rpoi)
        {
            if (viewPort == 0)
            {
                if (isVisible)
                {
                    if (rpoi.VisibleMask == gviViewportMask.gviView1)//只有第一屏可见
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView1 | gviViewportMask.gviView0;
                    }
                    else if (rpoi.VisibleMask == gviViewportMask.gviViewNone)//所有屏都不可见
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView0;
                    }
                    else
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView0;
                    }
                }
                else
                {
                    if ((int)(rpoi.VisibleMask) == 3)//第一第二屏都可见
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView1;
                    }
                    else if (rpoi.VisibleMask == gviViewportMask.gviView0)
                    {
                        rpoi.VisibleMask = gviViewportMask.gviViewNone;
                    }
                }
            }
            else if (viewPort == 1)
            {
                if (isVisible)
                {
                    if (rpoi.VisibleMask == gviViewportMask.gviView0)
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView1 | gviViewportMask.gviView0;
                    }
                    else if (rpoi.VisibleMask == gviViewportMask.gviViewNone)
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView1;
                    }
                    else
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView1;
                    }
                }
                else
                {
                    if ((int)(rpoi.VisibleMask) == 3)
                    {
                        rpoi.VisibleMask = gviViewportMask.gviView0;
                    }
                    else if (rpoi.VisibleMask == gviViewportMask.gviView1)
                    {
                        rpoi.VisibleMask = gviViewportMask.gviViewNone;
                    }
                }

            }
        }


        /// <summary>
        /// 根据poi标注id，在缓存中删除某一个标注
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeletePoi(string tag, string key)
        {
            if (_poiHashMap.ContainsKey(tag))
            {
                if (key == null) return false;
                if (_poiHashMap[tag].ContainsKey(key))
                    GviMap.ObjectManager.DeleteObject(_poiHashMap[tag][key].Guid);
                return _poiHashMap[tag].Remove(key);
            }
            return false;
        }

        /// <summary>
        /// 按照标签类别删除标注
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public void ClearPoiByTag(string tag)
        {
            if (_poiHashMap.ContainsKey(tag))
            {
                var keys = _poiHashMap[tag].Keys;
                if (keys?.Count > 0)
                {
                    foreach (var key in keys.ToList())
                    {
                        DeletePoi(tag, key);
                    }
                }
            }
        }
        /// <summary>
        /// 根据poi标注id，在缓存中删除某一个标注
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeletePoi(string key)
        {
            foreach (var tag in _poiHashMap.Keys)
            {
                if (_poiHashMap[tag].ContainsKey(key))
                {
                    GviMap.ObjectManager.DeleteObject(_poiHashMap[tag][key].Guid);
                    return _poiHashMap[tag].Remove(key);
                }
            }
            return false;
        }

        /// <summary>
        /// 更新poi标注的渲染数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="rpoi"></param>
        public void UpdateRPoi(string key, string tag, IRenderPOI rpoi)
        {
            if (_poiHashMap.ContainsKey(tag))
            {
                if (_poiHashMap[tag].ContainsKey(key))
                {
                    GviMap.ObjectManager.DeleteObject(_poiHashMap[tag][key].Guid);
                    _poiHashMap[tag][key] = rpoi;
                }
            }
        }

        /// <summary>
        /// 更新poi标注的几何数据
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="poi">poi几何数据</param>
        public void UpdatePoi(string key, IPOI poi)
        {
            foreach (var tag in _poiHashMap.Keys)
            {
                var poiMap = _poiHashMap[tag];
                if (poiMap.ContainsKey(key))
                    _poiHashMap[tag][key].SetFdeGeometry(poi);
            }

        }

        /// <summary>
        /// 更新poi标注的几何数据
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="tag">类型标签</param>
        /// <param name="poi">poi几何数据</param>
        public void UpdatePoi(string key, string tag, IPOI poi)
        {
            if (_poiHashMap.ContainsKey(tag))
            {
                if (_poiHashMap[tag].ContainsKey(key))
                    _poiHashMap[tag][key].SetFdeGeometry(poi);
            }
        }
        public Dictionary<string, string> GetPoiNameBytags(string tag)
        {
            var dic = new Dictionary<string, string>();
            if (_poiHashMap.ContainsKey(tag))
            {
                foreach (var key in _poiHashMap[tag].Keys)
                {
                    dic.Add(key, _poiHashMap[tag][key].Name);
                }
            }
            return dic;
        }

        /// <summary>
        /// 清空标注缓存
        /// </summary>
        public void Clear()
        {
            var tags = _poiHashMap.Keys;
            if (tags?.Count > 0)
            {
                foreach (var tag in tags.ToList())
                {
                    var keys = _poiHashMap[tag].Keys;
                    foreach (var key in keys.ToList())
                        DeletePoi(tag, key);
                }
                _poiHashMap.Clear();
            }
        }
    }
}
