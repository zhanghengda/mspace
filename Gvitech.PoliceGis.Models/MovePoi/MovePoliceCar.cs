using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Mmc.Windows.Attributes;
using Newtonsoft.Json;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x0200000D RID: 13
	public class MovePoliceCar : IMoveObject
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000031FC File Offset: 0x000013FC
		public MovePoliceCar(PoliceCar policeCar)
		{
			bool flag = policeCar == null;
			if (flag)
			{
				throw new ArgumentNullException("policeCar");
			}
			this.PoliceCar = policeCar;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000322B File Offset: 0x0000142B
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003233 File Offset: 0x00001433
		public bool IsSelectedIfo { get; protected set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000323C File Offset: 0x0000143C
		public string Name
		{
			get
			{
				return (this.PoliceCar != null) ? this.PoliceCar.PoliceCarNumber : string.Empty;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003268 File Offset: 0x00001468
		public virtual bool IsMatchKey(string key)
		{
			bool flag = string.IsNullOrEmpty(key);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.PoliceCar == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = false;
					PropertyInfo[] properties = this.GetProperties();
					bool flag4 = !CollectionExtension.HasValues<PropertyInfo>(properties);
					if (flag4)
					{
						result = false;
					}
					else
					{
						foreach (PropertyInfo propertyInfo in properties)
						{
							bool flag5 = propertyInfo == null;
							if (!flag5)
							{
								object value = propertyInfo.GetValue(this.PoliceCar);
								flag3 |= (value != null && StringExtension.ParseTo<string>(value, null).Contains(key));
							}
						}
						result = flag3;
					}
				}
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003314 File Offset: 0x00001514
		public virtual DataTable ToDataTable()
		{
			return (this.PoliceCar != null) ? this.PoliceCar.ToDataTable() : null;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000333C File Offset: 0x0000153C
		public virtual string Id
		{
			get
			{
				return (this.PoliceCar != null) ? this.PoliceCar.PoliceCarId : string.Empty;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003368 File Offset: 0x00001568
		public virtual string Coordinate
		{
			get
			{
				return (this.PoliceCar != null) ? this.PoliceCar.PoliceCarCoordinate : string.Empty;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003394 File Offset: 0x00001594
		public virtual MoveObjectType MoveObjectType
		{
			get
			{
				return (this.PoliceCar != null) ? MoveObjectType.MovePoliceCar : MoveObjectType.MoveNone;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000033B4 File Offset: 0x000015B4
		public virtual bool Refresh(string jsonStr)
		{
			PoliceCar policeCar = JsonConvert.DeserializeObject<PoliceCar>(jsonStr);
			bool result = false;
			bool flag = this.PoliceCar == null;
			if (flag)
			{
				this.PoliceCar = policeCar;
				result = true;
			}
			else
			{
				bool flag2 = !this.PoliceCar.Equals(policeCar);
				if (flag2)
				{
					bool flag3 = this.PoliceCar.PoliceCarId.Equals(policeCar.PoliceCarId);
					if (flag3)
					{
						this.PoliceCar = policeCar;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003428 File Offset: 0x00001628
		public virtual PropertyInfo[] GetProperties()
		{
			bool flag = CollectionExtension.HasValues<PropertyInfo>(this.Properties);
			PropertyInfo[] result;
			if (flag)
			{
				result = this.Properties;
			}
			else
			{
				result = (this.Properties = (from p in typeof(PoliceCar).GetProperties().ToList<PropertyInfo>()
				where this.ValidProperty(p)
				select p).ToArray<PropertyInfo>());
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003488 File Offset: 0x00001688
		public virtual object GetPropertyValue(string propName)
		{
			bool flag = string.IsNullOrEmpty(propName);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PropertyInfo[] properties = this.GetProperties();
				bool flag2 = !CollectionExtension.HasValues<PropertyInfo>(properties);
				if (flag2)
				{
					result = null;
				}
				else
				{
					PropertyInfo propertyInfo = properties.ToList<PropertyInfo>().FirstOrDefault((PropertyInfo p) => p.Name.ToLower().Equals(propName.ToLower()));
					bool flag3 = propertyInfo != null;
					if (flag3)
					{
						result = propertyInfo.GetValue(this.PoliceCar);
					}
					else
					{
						propertyInfo = properties.ToList<PropertyInfo>().FirstOrDefault((PropertyInfo p) => this.IsAliasNameOfProperty(p, propName));
						result = ((propertyInfo == null) ? null : propertyInfo.GetValue(this.PoliceCar));
					}
				}
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003548 File Offset: 0x00001748
		protected bool IsAliasNameOfProperty(PropertyInfo propInfo, string propName)
		{
			bool flag = propInfo == null || string.IsNullOrEmpty(propName);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				object[] customAttributes = propInfo.GetCustomAttributes(typeof(AliasAttribute), true);
				bool flag2 = !CollectionExtension.HasValues<object>(customAttributes);
				if (flag2)
				{
					result = false;
				}
				else
				{
					AliasAttribute[] array = customAttributes as AliasAttribute[];
					result = (array != null && array.ToList<AliasAttribute>().Exists((AliasAttribute aliasAtt) => aliasAtt.AliasName.Equals(propName)));
				}
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035D4 File Offset: 0x000017D4
		protected bool ValidProperty(PropertyInfo property)
		{
			bool flag = property == null;
			bool result=false;
			if (flag)
			{
				result = false;
			}
			else
			{
                string text = property.PropertyType.Name.ToLower();
                return true;
                //uint num = < PrivateImplementationDetails >.ComputeStringHash(text);
                //	if (num <= 2515107422u)
                //	{
                //		if (num <= 398550328u)
                //		{
                //			if (num != 64103268u)
                //			{
                //				if (num != 132346577u)
                //				{
                //					if (num != 398550328u)
                //					{
                //						goto IL_21F;
                //					}
                //					if (!(text == "string"))
                //					{
                //						goto IL_21F;
                //					}
                //				}
                //				else if (!(text == "int16"))
                //				{
                //					goto IL_21F;
                //				}
                //			}
                //			else if (!(text == "int64"))
                //			{
                //				goto IL_21F;
                //			}
                //		}
                //		else if (num <= 848563180u)
                //		{
                //			if (num != 520654156u)
                //			{
                //				if (num != 848563180u)
                //				{
                //					goto IL_21F;
                //				}
                //				if (!(text == "uint32"))
                //				{
                //					goto IL_21F;
                //				}
                //			}
                //			else if (!(text == "decimal"))
                //			{
                //				goto IL_21F;
                //			}
                //		}
                //		else if (num != 2133018345u)
                //		{
                //			if (num != 2515107422u)
                //			{
                //				goto IL_21F;
                //			}
                //			if (!(text == "int"))
                //			{
                //				goto IL_21F;
                //			}
                //		}
                //		else if (!(text == "single"))
                //		{
                //			goto IL_21F;
                //		}
                //	}
                //	else if (num <= 2929723411u)
                //	{
                //		if (num != 2699759368u)
                //		{
                //			if (num != 2928590578u)
                //			{
                //				if (num != 2929723411u)
                //				{
                //					goto IL_21F;
                //				}
                //				if (!(text == "uint64"))
                //				{
                //					goto IL_21F;
                //				}
                //			}
                //			else if (!(text == "uint16"))
                //			{
                //				goto IL_21F;
                //			}
                //		}
                //		else if (!(text == "double"))
                //		{
                //			goto IL_21F;
                //		}
                //	}
                //	else if (num <= 3270303571u)
                //	{
                //		if (num != 3122818005u)
                //		{
                //			if (num != 3270303571u)
                //			{
                //				goto IL_21F;
                //			}
                //			if (!(text == "long"))
                //			{
                //				goto IL_21F;
                //			}
                //		}
                //		else if (!(text == "short"))
                //		{
                //			goto IL_21F;
                //		}
                //	}
                //	else if (num != 3437915536u)
                //	{
                //		if (num != 4225688255u)
                //		{
                //			goto IL_21F;
                //		}
                //		if (!(text == "int32"))
                //		{
                //			goto IL_21F;
                //		}
                //	}
                //	else if (!(text == "datetime"))
                //	{
                //		goto IL_21F;
                //	}
                //	bool flag2 = true;
                //	goto IL_223;
                //	IL_21F:
                //	flag2 = false;
                //	IL_223:
                //	result = flag2;
            }
			return result;
		}

		// Token: 0x04000033 RID: 51
		protected PoliceCar PoliceCar;

		// Token: 0x04000034 RID: 52
		protected PropertyInfo[] Properties;
	}
}
