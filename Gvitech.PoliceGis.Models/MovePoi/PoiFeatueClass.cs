using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Common;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000012 RID: 18
	public class PoiFeatueClass : IPOIFeatureClass, IShowLayer
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000397C File Offset: 0x00001B7C
		public PoiFeatueClass(string name, string alias)
		{
			this.Name = name;
			this.AliasName = alias;
			this.GuidString = Guid.NewGuid().ToString();
			this.ViewportMask = gviViewportMask.gviViewAllNormalView;
			this.MovePoiLst = new SynchronizedCollection<IMovePoi>();
			this.OverduMovePoiLst = new SynchronizedCollection<IMovePoi>();
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000039DC File Offset: 0x00001BDC
		public bool Visible
		{
			get
			{
				return this.ViewportMask > gviViewportMask.gviViewNone;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003A00 File Offset: 0x00001C00
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000039F7 File Offset: 0x00001BF7
		public gviViewportMask ViewportMask { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003A08 File Offset: 0x00001C08
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003A10 File Offset: 0x00001C10
		public string AliasName { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003A19 File Offset: 0x00001C19
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003A21 File Offset: 0x00001C21
		public string Name { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003A2A File Offset: 0x00001C2A
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003A32 File Offset: 0x00001C32
		public string GuidString { get; private set; }

		// Token: 0x060000DD RID: 221 RVA: 0x00003A3C File Offset: 0x00001C3C
		public void UpdateData(IMoveObject moveObject)
		{
			bool flag = moveObject == null;
			if (!flag)
			{
				IMovePoi movePoi = this.MovePoiLst.FirstOrDefault((IMovePoi poi) => poi.Id.Equals(moveObject.Id));
				bool flag2 = movePoi != null;
				if (flag2)
				{
					movePoi.UpdatePoi(moveObject);
				}
				else
				{
					bool flag3 = IEnumerableExtension.HasValues<IMovePoi>(this.OverduMovePoiLst);
					if (flag3)
					{
						movePoi = this.OverduMovePoiLst[0];
						movePoi.UpdatePoi(moveObject);
						this.OverduMovePoiLst.RemoveAt(0);
						this.MovePoiLst.Add(movePoi);
					}
					else
					{
						movePoi = new MovePoi(moveObject);
						movePoi.UpdatePoi(moveObject);
						this.MovePoiLst.Add(movePoi);
					}
				}
				movePoi.Show(this.Visible, gviViewportMask.gviViewAllNormalView);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003B1C File Offset: 0x00001D1C
		public void UpdateData(List<IMoveObject> moveObjects)
		{
			bool flag = !IEnumerableExtension.HasValues<IMoveObject>(moveObjects);
			if (!flag)
			{
				moveObjects.ForEach(new Action<IMoveObject>(this.UpdateData));
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003B50 File Offset: 0x00001D50
		public void UpdateData(IMoveObject[] moveObjects)
		{
			bool flag = !CollectionExtension.HasValues<IMoveObject>(moveObjects);
			if (!flag)
			{
				this.UpdateData(moveObjects.ToList<IMoveObject>());
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003B7C File Offset: 0x00001D7C
		public void Close()
		{
			bool flag = IEnumerableExtension.HasValues<IMovePoi>(this.MovePoiLst);
			if (flag)
			{
				IEnumerableExtension.ForEach<IMovePoi>(this.MovePoiLst, delegate(IMovePoi mp)
				{
					mp.Release();
				});
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public void HideOutTimePoi()
		{
			DateTime dt = DateTime.Now.AddMinutes(-10.0);
			this.CollectMovePoi((IMovePoi p) => dt > p.LastUpdateTime);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003C0C File Offset: 0x00001E0C
		public void CollectMovePoi(Predicate<IMovePoi> filter)
		{
			bool flag = this.MovePoiLst == null;
			if (!flag)
			{
				object syncRoot = this.MovePoiLst.SyncRoot;
				lock (syncRoot)
				{
					IEnumerable<IMovePoi> enumerable = from mp in this.MovePoiLst
					where mp != null && filter(mp)
					select mp;
					IList<IMovePoi> list = (enumerable as IList<IMovePoi>) ?? enumerable.ToList<IMovePoi>();
					IEnumerableExtension.ForEach<IMovePoi>(list, delegate(IMovePoi mp)
					{
						mp.Show(false, gviViewportMask.gviViewAllNormalView);
						this.MovePoiLst.Remove(mp);
						this.OverduMovePoiLst.Add(mp);
					});
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public bool HighLightFeature(string id, IFeatureManager fm = null, uint color = 4294901760u)
		{
			bool flag = string.IsNullOrEmpty(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IMovePoi movePoi = this.SearchById(id);
				bool flag2 = movePoi == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					movePoi.HightLight(color);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public bool HighLightFeatures(string[] ids, IFeatureManager fm = null, uint color = 4294901760u)
		{
			bool flag2 = !CollectionExtension.HasValues<string>(ids);
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag = true;
				IEnumerableExtension.ForEach<string>(ids, delegate(string id)
				{
					flag &= this.HighLightFeature(id, fm, color);
				});
				result = flag;
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003D50 File Offset: 0x00001F50
		public bool UnHighLightFeature(string id, IFeatureManager fm = null)
		{
			bool flag = string.IsNullOrEmpty(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IMovePoi movePoi = this.SearchById(id);
				bool flag2 = movePoi == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					movePoi.UnHightLight();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003D8C File Offset: 0x00001F8C
		public bool UnHighLightFeatures(string[] ids, IFeatureManager fm = null)
		{
			bool flag2 = !CollectionExtension.HasValues<string>(ids);
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag = true;
				IEnumerableExtension.ForEach<string>(ids, delegate(string id)
				{
					flag &= this.UnHighLightFeature(id, fm);
				});
				result = flag;
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public bool UnHighLightFeatureClass(IFeatureManager fm = null)
		{
			bool flag = !IEnumerableExtension.HasValues<IMovePoi>(this.MovePoiLst);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IEnumerableExtension.ForEach<IMovePoi>(this.MovePoiLst, delegate(IMovePoi mp)
				{
					mp.UnHightLight();
				});
				result = true;
			}
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003E38 File Offset: 0x00002038
		public bool SetVisibleMask(bool visible, gviViewportMask gvMask = gviViewportMask.gviViewAllNormalView)
		{
			bool flag = !IEnumerableExtension.HasValues<IMovePoi>(this.MovePoiLst);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool showen = false;
				IEnumerableExtension.ForEach<IMovePoi>(this.MovePoiLst, delegate(IMovePoi mp)
				{
					showen &= mp.Show(visible, gvMask);
				});
				this.ViewportMask = (visible ? gvMask : gviViewportMask.gviViewNone);
				result = showen;
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003EB4 File Offset: 0x000020B4
		public IMovePoi SearchByRenderId(string guid)
		{
			return string.IsNullOrEmpty(guid) ? null : this.Search((IMovePoi mp) => guid.Equals(mp.RenderId));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003EF8 File Offset: 0x000020F8
		public IMovePoi SearchById(string id)
		{
			return string.IsNullOrEmpty(id) ? null : this.Search((IMovePoi mp) => id.Equals(mp.Id));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003F3C File Offset: 0x0000213C
		public bool FlyToFeature(string id, ICamera camera)
		{
			bool flag = string.IsNullOrEmpty(id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !IEnumerableExtension.HasValues<IMovePoi>(this.MovePoiLst);
					if (flag3)
					{
						result = false;
					}
					else
					{
						IMovePoi movePoi = this.MovePoiLst.FirstOrDefault((IMovePoi mp) => id.Equals(mp.Id));
						result = (movePoi != null && movePoi.FlyTo(camera));
					}
				}
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003FBC File Offset: 0x000021BC
		public DataTable FuzzySearch(string key, string fields = null, IGeometry geo = null)
		{
			bool flag = !IEnumerableExtension.HasValues<IMovePoi>(this.MovePoiLst);
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IEnumerable<IMovePoi> enumerable;
				if (!string.IsNullOrEmpty(key))
				{
					enumerable = from mp in this.MovePoiLst
					where mp.IsMatchKey(key)
					select mp;
				}
				else
				{
					IEnumerable<IMovePoi> movePoiLst = this.MovePoiLst;
					enumerable = movePoiLst;
				}
				IEnumerable<IMovePoi> enumerable2 = enumerable;
				bool flag2 = enumerable2 == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					PropertyInfo[] moveObjectProperties = this.MovePoiLst.First<IMovePoi>().GetMoveObjectProperties();
					string[] array = string.IsNullOrEmpty(fields) ? null : fields.ToLower().Split(new char[]
					{
						',',
						';'
					}, StringSplitOptions.RemoveEmptyEntries);
					bool flag3 = array != null;
					if (flag3)
					{
						List<string> list = array.ToList<string>();
						CollectionExtension.AddEx<string>(list, "oid");
						array = list.ToArray();
					}
					DataTable dt = PropertyInfoArrayExtension.CreateEmptyDataTable(moveObjectProperties, array);
					bool flag4 = dt == null;
					if (flag4)
					{
						result = null;
					}
					else
					{
						int columnCount = dt.Columns.Count;
						bool flag5 = columnCount < 1;
						if (flag5)
						{
							result = null;
						}
						else
						{
							DataRow dr;
							enumerable2.ToList<IMovePoi>().ForEach(delegate(IMovePoi mp)
							{
								bool flag6 = geo != null;
								if (flag6)
								{
									IPolygon polygon = geo as IPolygon;
									IPoint pointValue = mp.Position.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPoint;
									bool flag7 = polygon == null || !polygon.IsPointOnSurface(pointValue);
									if (!flag7)
									{
										dr = dt.NewRow();
										int num;
										for (int i = 0; i < columnCount; i = num + 1)
										{
											dr[i] = mp.GetPropertyValue(dt.Columns[i].ColumnName);
											num = i;
										}
										dt.Rows.Add(dr);
									}
								}
								else
								{
									dr = dt.NewRow();
									int num;
									for (int j = 0; j < columnCount; j = num + 1)
									{
										dr[j] = mp.GetPropertyValue(dt.Columns[j].ColumnName);
										num = j;
									}
									dt.Rows.Add(dr);
								}
							});
							result = dt;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004110 File Offset: 0x00002310
		private IMovePoi Search(Predicate<IMovePoi> filter)
		{
			bool flag = !IEnumerableExtension.HasValues<IMovePoi>(this.MovePoiLst);
			IMovePoi result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.MovePoiLst.FirstOrDefault((IMovePoi mp) => filter(mp));
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000415C File Offset: 0x0000235C
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
				IMovePoi movePoi = this.SearchByRenderId(id);
				result = (movePoi != null);
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004188 File Offset: 0x00002388
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
				IMovePoi movePoi = this.SearchByRenderId(id);
				result = ((movePoi == null) ? null : movePoi.ShowDataTable);
			}
			return result;
		}

        public string GetFid()
        {
            return "oid";
        }

        // Token: 0x04000048 RID: 72
        protected SynchronizedCollection<IMovePoi> MovePoiLst;

		// Token: 0x04000049 RID: 73
		protected SynchronizedCollection<IMovePoi> OverduMovePoiLst;
	}
}
