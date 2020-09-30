using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Common;
using Mmc.Windows.Utils;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.DataSourceAccess
{
    
    public class DisplayLayer : IRenderLayer, IDisplayLayer, IShowLayer
    {
        
        public DisplayLayer(IFeatureClass fc, IFeatureLayer flyer)
        {
            bool flag = fc == null;
            if (flag)
            {
                throw new ArgumentNullException("fc");
            }
            bool flag2 = flyer == null;
            if (flag2)
            {
                throw new ArgumentNullException("flyer");
            }
            this.Fc = fc;
            this.FLyers = new List<IFeatureLayer>
            {
                flyer
            };
        }

        
        public DisplayLayer(IFeatureClass fc, List<IFeatureLayer> flyers)
        {
            bool flag = fc == null;
            if (flag)
            {
                throw new ArgumentNullException("fc");
            }
            this.Fc = fc;
            this.FLyers = flyers;
        }

        
        // (get) Token: 0x06000022 RID: 34 RVA: 0x0000293C File Offset: 0x00000B3C
        // (set) Token: 0x06000023 RID: 35 RVA: 0x00002944 File Offset: 0x00000B44
        public IFeatureClass Fc { get; private set; }

        
        // (get) Token: 0x06000024 RID: 36 RVA: 0x0000294D File Offset: 0x00000B4D
        // (set) Token: 0x06000025 RID: 37 RVA: 0x00002955 File Offset: 0x00000B55
        public List<IFeatureLayer> FLyers { get; private set; }

        
        // (get) Token: 0x06000026 RID: 38 RVA: 0x00002960 File Offset: 0x00000B60
        public string Name
        {
            get
            {
                return (this.Fc != null) ? this.Fc.Name : string.Empty;
            }
            set { }
        }

        
        // (get) Token: 0x06000027 RID: 39 RVA: 0x0000298C File Offset: 0x00000B8C
        public string AliasName
        {
            get
            {
                return (this.Fc != null) ? ((!string.IsNullOrEmpty(this.Fc.Alias)) ? this.Fc.Alias : this.Fc.Name) : string.Empty;
            }
            set { }
        }

        
        // (get) Token: 0x06000028 RID: 40 RVA: 0x000029D8 File Offset: 0x00000BD8
        public string Guid
        {
            get
            {
                return (this.Fc != null) ? this.Fc.Guid.ToString() : string.Empty;
            }
            set { }
        }

        public RenderLayerType LayerType { get => RenderLayerType.FeatureLayer; set { } }
        public IRenderable Renderable { get { return this.FLyers?.Count > 0 ? this.FLyers[0] : null; } set { } }

        public bool IsLocal { get; set; }

        public bool AddFeatureLayer(IFeatureLayer fly)
        {
            bool flag = fly == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.FLyers == null;
                if (flag2)
                {
                    this.FLyers = new List<IFeatureLayer>();
                }
                result = CollectionExtension.AddEx<IFeatureLayer>(this.FLyers, fly);
            }
            return result;
        }

        
        public bool AddFeatureLayers(List<IFeatureLayer> flys)
        {
            bool flag = !IEnumerableExtension.HasValues<IFeatureLayer>(flys);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.FLyers == null;
                if (flag2)
                {
                    this.FLyers = new List<IFeatureLayer>();
                }
                flys.ForEach(delegate (IFeatureLayer f)
                {
                    CollectionExtension.AddEx<IFeatureLayer>(this.FLyers, f);
                });
                result = true;
            }
            return result;
        }

        
        public bool FlyToFeature(string id, ICamera camera)
        {
            bool flag = string.IsNullOrEmpty(id) || camera == null;
            return !flag && this.Fc.FlyToFeatue(StringExtension.ParseTo<int>(id, 0), camera);
        }

        
        public DataTable FuzzySearch(string key, string fields = null, IGeometry geo = null)
        {
            DataTable result;
            try
            {
                result = ((this.Fc != null) ? this.Fc.FuzzySearch(key, fields, geo) : null);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        
        public bool HighLightFeature(string id, IFeatureManager fm = null, uint color = 4294901760u)
        {
            bool flag = fm == null || string.IsNullOrEmpty(id);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                result = (!flag2 && fm.HighlightFeature(this.Fc, StringExtension.ParseTo<int>(id, -1), ColorConvert.UintToColor(color)));
            }
            return result;
        }

        
        public bool HighLightFeatures(string[] ids, IFeatureManager fm = null, uint color = 4294901760u)
        {
            bool flag = fm == null || !CollectionExtension.HasValues<string>(ids);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    IEnumerable<int> source = from id in ids
                                              select StringExtension.ParseTo<int>(id, -1);
                    result = fm.HighlightFeatures(this.Fc, source.ToArray<int>(), ColorConvert.UintToColor(color));
                }
            }
            return result;
        }

        
        public bool UnHighLightFeature(string id, IFeatureManager fm = null)
        {
            bool flag = fm == null || string.IsNullOrEmpty(id);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                result = (!flag2 && fm.UnhighlightFeature(this.Fc, StringExtension.ParseTo<int>(id, -1)));
            }
            return result;
        }

        
        public bool UnHighLightFeatures(string[] ids, IFeatureManager fm = null)
        {
            bool flag3 = fm == null || !CollectionExtension.HasValues<string>(ids);
            bool result;
            if (flag3)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag = false;
                    IEnumerableExtension.ForEach<string>(ids, delegate (string id)
                    {
                        flag &= this.UnHighLightFeature(id, fm);
                    });
                    result = flag;
                }
            }
            return result;
        }

        
        public bool UnHighLightFeatureClass(IFeatureManager fm = null)
        {
            bool flag = fm == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                result = (!flag2 && fm.UnhighlightFeatureClass(this.Fc));
            }
            return result;
        }

        
        public bool SetVisibleMask(bool visible, gviViewportMask vmask = gviViewportMask.gviViewAllNormalView)
        {
            bool flag = !IEnumerableExtension.HasValues<IFeatureLayer>(this.FLyers);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                IFeatureLayerExtension.SetVisibleMask(this.FLyers.First<IFeatureLayer>(), visible, vmask);
                result = true;
            }
            return result;
        }

        
        public bool ContainObject(string id)
        {
            bool flag = string.IsNullOrEmpty(id);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                result = (!flag2 && this.Fc.Guid.ToString().Equals(id));
            }
            return result;
        }

        
        public DataTable GetInfoTable(string id)
        {
            bool flag = string.IsNullOrEmpty(id);
            DataTable result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = this.Fc == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    IRowBuffer row = this.Fc.GetRow(StringExtension.ParseTo<int>(id, -1));
                    bool flag3 = row == null;
                    if (flag3)
                    {
                        result = null;
                    }
                    else
                    {
                        DataTable dataTable = row.ToDataTable();
                        bool flag4 = dataTable.Columns.Count >= 2;
                        if (flag4)
                        {
                            dataTable.Columns[0].ColumnName = "类别";
                            dataTable.Columns[1].ColumnName = "信息";
                        }
                        bool flag5 = dataTable != null && dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0 && dataTable.Rows[0][0].ToString().ToLower().Equals("oid");
                        if (flag5)
                        {
                            dataTable.Rows.RemoveAt(0);
                        }
                        result = dataTable;
                    }
                }
            }
            return result;
        }

        
        public bool IsFc(string fcGuid)
        {
            bool flag = string.IsNullOrEmpty(fcGuid);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Fc == null;
                result = (!flag2 && fcGuid.Equals(this.Fc.Guid.ToString()));
            }
            return result;
        }

        
        public bool HasFeaturLayer(string flyGuid)
        {
            bool flag = string.IsNullOrEmpty(flyGuid);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = !IEnumerableExtension.HasValues<IFeatureLayer>(this.FLyers);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    result = (from fly in this.FLyers
                              select fly.Guid.ToString()).Contains(flyGuid);
                }
            }
            return result;
        }

        public string GetFid()
        {
            return Fc?.FidFieldName;
        }
    }
}
