using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Models.PlanShowService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mmc.Mspace.Services.PlanShowService
{
    public class PlanShowService : Singleton<PlanShowService>, IPlanShowService
    {
        public void LoadData()
        {
            this.LoadCepLayer();
        }

        public void RemovePlanShow()
        {
            this._isLoaded = false;
            bool flag = this.cepId != Guid.Empty;
            if (flag)
            {
                GviMap.MapControl.ProjectTree.DeleteItem(this.cepId);
                this._previews.Clear();
            }
        }

        public List<Preview> GetPlanShow()
        {
            return this._previews;
        }

        private void LoadByOPen()
        {
            bool flag = !this._isLoaded;
            if (flag)
            {
                string text = "I:\\银川公安数据\\银川公安\\银川平面\\消防预演.cep";
                GviMap.MapControl.Project.Open(text, false, "");
                string scrptPath = "预案演示\\消防预演脚本";
                this.LoadDisplayScript(scrptPath);
                scrptPath = "预案演示\\警力部署脚本";
                this.LoadDisplayScript(scrptPath);
                this._isLoaded = true;
            }
        }

        private void LoadCepLayer1()
        {
            try
            {
                bool flag = !this._isLoaded;
                if (flag)
                {
                    string text = "I:\\银川公安数据\\银川公安\\银川平面\\消防预演.cep";
                    Guid a = GviMap.MapControl.ProjectTree.LoadCepLayer(text, GviMap.MapControl.ProjectTree.RootID);
                    bool flag2 = a == Guid.Empty;
                    if (flag2)
                    {
                        Singleton<AppLogService>.Instance.Log(string.Format("Cep文件加载失败：{0}", text), LogMessageType.ERROR);
                    }
                    else
                    {
                        string scrptPath = "消防预演\\预案演示\\消防预演脚本";
                        this.LoadDisplayScript(scrptPath);
                        scrptPath = "消防预演\\预案演示\\警力部署脚本";
                        this.LoadDisplayScript(scrptPath);
                        this._isLoaded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCepLayer2()
        {
            try
            {
                bool flag = !this._isLoaded;
                if (flag)
                {
                    string text = "I:\\银川公安数据\\银川公安\\银川平面\\Package_预案演示\\预案演示.cep";
                    Guid a = GviMap.MapControl.ProjectTree.LoadCepLayer(text, GviMap.MapControl.ProjectTree.RootID);
                    bool flag2 = a == Guid.Empty;
                    if (flag2)
                    {
                        Singleton<AppLogService>.Instance.Log(string.Format("Cep文件加载失败：{0}", text), LogMessageType.ERROR);
                    }
                    else
                    {
                        string scrptPath = "预案演示\\预案演示\\消防预演脚本";
                        this.LoadDisplayScript(scrptPath);
                        scrptPath = "预案演示\\预案演示\\警力部署脚本";
                        this.LoadDisplayScript(scrptPath);
                        this._isLoaded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCepLayer()
        {
            try
            {
                bool flag = !this._isLoaded;
                if (flag)
                {
                    string text = Application.StartupPath + "\\data\\Package\\预案演示.cep";
                    IProjectTree projectTree = GviMap.MapControl.ProjectTree;
                    this.cepId = projectTree.LoadCepLayer(text, GviMap.MapControl.ProjectTree.RootID);
                    bool flag2 = this.cepId == Guid.Empty;
                    if (flag2)
                    {
                        Singleton<AppLogService>.Instance.Log(string.Format("Cep文件加载失败：{0}", text), LogMessageType.ERROR);
                    }
                    else
                    {
                      
                        //Guid nextItem = projectTree.GetNextItem(this.cepId, 11);
                        //Guid nextItem2 = projectTree.GetNextItem(nextItem, 11);
                        Guid nextItem = projectTree.GetNextItem(this.cepId,  gviItemCode.gviItemCodeFirstVisible);
                        Guid nextItem2 = projectTree.GetNextItem(nextItem,  gviItemCode.gviItemCodeFirstVisible);
                        bool flag3 = !projectTree.IsGroup(nextItem2);
                        if (flag3)
                        {
                            this.LoadPresentation(nextItem2);
                        }
                        string itemName = projectTree.GetItemName(nextItem2);
                        //Guid nextItem3 = projectTree.GetNextItem(nextItem2, 13);
                        Guid nextItem3 = projectTree.GetNextItem(nextItem2, gviItemCode.gviItemCodeNext);
                        while (nextItem3 != Guid.Empty)
                        {
                            bool flag4 = !projectTree.IsGroup(nextItem3);
                            if (flag4)
                            {
                                this.LoadPresentation(nextItem3);
                            }
                            //nextItem3 = projectTree.GetNextItem(nextItem3, 13);
                            nextItem3 = projectTree.GetNextItem(nextItem3,  gviItemCode.gviItemCodeNext);
                        }
                        this._isLoaded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadPresentation(Guid presentID)
        {
            IRObject objectById = GviMap.MapControl.ObjectManager.GetObjectById(presentID);
            bool flag = objectById is IPresentation;
            if (flag)
            {
                IPresentation presentation = objectById as IPresentation;
                Preview item = new Preview(presentation);
                this._previews.Add(item);
            }
        }

        private void LoadDisplayScript(string scrptPath)
        {
            Guid guid = GviMap.MapControl.ProjectTree.FindItem(scrptPath);
            bool flag = guid == Guid.Empty;
            if (flag)
            {
                Singleton<AppLogService>.Instance.Log(string.Format("没找到脚本：{0}", scrptPath), LogMessageType.ERROR);
            }
            else
            {
                IRObject objectById = GviMap.MapControl.ObjectManager.GetObjectById(guid);
                bool flag2 = objectById is IPresentation;
                if (flag2)
                {
                    IPresentation presentation = objectById as IPresentation;
                    Preview item = new Preview(presentation);
                    this._previews.Add(item);
                }
            }
        }

        public static PlanShowService GetDefault(object args = null)
        {
            return Singleton<PlanShowService>.Instance;
        }

        private readonly List<Preview> _previews = new List<Preview>();

        private bool _isLoaded;

        private Guid cepId;
    }
}