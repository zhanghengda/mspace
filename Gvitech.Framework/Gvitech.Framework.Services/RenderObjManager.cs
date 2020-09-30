using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;

using System.Linq;

namespace Gvitech.Framework.Services
{
    public class RenderObjManager : ISymbolManager<IRenderable> 
    {
        private Dictionary<string, Dictionary<string, Tuple<ILabel, IRenderable>>> _renderObjMaps;

        public RenderObjManager()
        {
            
            _renderObjMaps = new Dictionary<string, Dictionary<string, Tuple<ILabel, IRenderable>>>();
        }


        public bool AddRenObj(string tag, string key, ILabel geolable, IRenderable georender)
        {
            try
            {
                bool result = false;
                var tempItem = new Tuple<ILabel, IRenderable>(geolable, georender);
                if (_renderObjMaps.ContainsKey(tag))
                {
                    if (!ContainKey(tag, key))
                    {
                        _renderObjMaps[tag].Add(key, tempItem);
                        result = true;
                    }
                }else
                {
                     Dictionary<string, Tuple<ILabel, IRenderable>> tempDic = new  Dictionary<string, Tuple<ILabel, IRenderable>>();
                    tempDic.Add(key, tempItem);
                    _renderObjMaps.Add(tag, tempDic);
                    result = true;
                }
                return result;
            } catch (Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }




        public void ClearAll()
        {
            try
            {
                var tags = GetRenObjTags();
                if(tags?.Count>0)
                {
                    foreach(var tag in tags)
                    {
                        DeleteRenObjs(tag);
                    }
                }
            }catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public bool ContainKey(string key)
        {
            try
            {
                bool isExist = false;
                var tags = GetRenObjTags();
                if (tags.Count > 0)
                {
                    foreach (var tag in tags)
                    {
                        isExist = ContainKey(tag, key);
                        if (isExist)
                        {
                            break;
                        }
                    }
                }
                return isExist;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }
        public bool ContainKey(string tag, string key)
        {
            try
            {
                bool isExist = false;
                if(ContainTag(tag))
                {
                    isExist = _renderObjMaps[tag].ContainsKey(key);
                }
                return isExist;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }

        public bool ContainTag(string tag)
        {
            try
            {
                return  _renderObjMaps.ContainsKey(tag);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }

        public bool DeleteRenObj(string tag, string key)
        {
            try
            {
                bool result = false;
                if(ContainKey(tag,key))
                {
                    var item = GetRenObj(tag, key);
                    if (item == null) return result;
                    if(item.Item1!=null)
                    {
                        GviMap.ObjectManager.DeleteObject(item.Item1.Guid);
                        item.Item1.Position.Dispose();
                    }
                    if (item.Item2 != null)
                    {
                        GviMap.ObjectManager.DeleteObject(item.Item2.Guid);
                        _renderObjMaps[tag].Remove(key);
                        result = true;
                    }
                    return result;
                }
                return result;
            }catch(Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }

        public bool DeleteRenObj(string key)
        {
            try
            {
                bool result = false;
                var tags = GetRenObjTags();
                if(tags.Count>0)
                {
                    foreach(var tag in tags)
                    {
                        result = DeleteRenObj(tag, key);
                        if (result) break;
                    }
                }
                return result;
            }catch(Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }

        public void DeleteRenObjs(string tag)
        {
            try
            {
                if(ContainTag(tag))
                {
                    var keys = _renderObjMaps[tag].Keys.ToList();
                    foreach(var key in keys)
                    {
                        DeleteRenObj(tag, key);
                    }
                    if(_renderObjMaps[tag].Values.Count<=0)
                    {
                        _renderObjMaps.Remove(tag);
                    }
                }
            }catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public Tuple<ILabel, IRenderable> GetRenObj(string tag, string key)
        {
            try
            {
                if(ContainKey(tag,key))
                {
                    return _renderObjMaps[tag][key];
                }
                else
                {
                    return null;
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
                return null;
            }
        }

        public Tuple<ILabel, IRenderable> GetRenObj(string key)
        {
            Tuple<ILabel, IRenderable> result = null;
            var tags = GetRenObjTags();
            if(tags?.Count>0)
            {
                foreach(var tag in tags)
                {
                    result = GetRenObj(tag, key);
                    if (result != null) break;
                }
            }
            return result;
        }

        public List<string> GetRenObjTags()
        {
            
            return _renderObjMaps.Keys.ToList();
            
        }

        public void SetItemVisible(string tag, string key, bool isvisible)
        {
            try
            {
                if(ContainKey(tag,key))
                {
                    var item = GetRenObj(tag, key);
                    item.Item1.VisibleMask = isvisible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                    item.Item2.VisibleMask = isvisible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                }
            }catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public bool UpdateRenObj(string tag, string key, ILabel geolable, IRenderable georender)
        {
            try
            {
                bool result = false;
                if(ContainKey(tag,key))
                {
                    _renderObjMaps[tag][key] = new Tuple<ILabel, IRenderable>(geolable, georender);
                    result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }

        public bool UpdateRenObj(string key, ILabel geolable, IRenderable georender)
        {
            try
            {
                bool result = false;
                var tags = GetRenObjTags();
                if (tags?.Count > 0)
                {
                    foreach(var tag in tags)
                    {
                        result=UpdateRenObj(tag, key, geolable, georender);
                        if (result) break;
                    }
                }
                return result;
            }catch(Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }
    }
}
