using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;

namespace Gvitech.Framework.Services
{
    public interface ISymbolManager<T> where T : IRenderable
    {
        bool ContainTag(string tag);
        bool ContainKey(string key);
        bool ContainKey(string tag, string key);

        List<string> GetRenObjTags();

        Tuple<ILabel, T> GetRenObj(string tag, string key);
        Tuple<ILabel, T> GetRenObj(string key);

        bool AddRenObj(string tag, string key, ILabel geolable, T georender);

        bool DeleteRenObj(string tag, string key);
        bool DeleteRenObj(string key);
        void DeleteRenObjs(string tag);
        void ClearAll();

        bool UpdateRenObj(string tag, string key, ILabel geolable, T georender);
        bool UpdateRenObj(string key, ILabel geolable, T georender);

        //T CreateRender(T georender);
        void SetItemVisible(string tag, string key, bool isvisible);
    }
}