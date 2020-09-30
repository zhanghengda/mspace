using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Mmc.Windows.Attributes;
using Newtonsoft.Json;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x0200000C RID: 12
	public class MovePoliceman : IMoveObject
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public MovePoliceman(Policeman police)
		{
			bool flag = police == null;
			if (flag)
			{
				throw new ArgumentNullException("police");
			}
			this.Police = police;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002C13 File Offset: 0x00000E13
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002C1B File Offset: 0x00000E1B
		public bool IsSelectedIfo { get; protected set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002C24 File Offset: 0x00000E24
		public string Name
		{
			get
			{
				return (this.Police != null) ? this.Police.PolicemanName : string.Empty;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002C50 File Offset: 0x00000E50
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
				bool flag2 = this.Police == null;
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
								object value = propertyInfo.GetValue(this.Police);
								flag3 |= (value != null && StringExtension.ParseTo<string>(value, null).Contains(key));
							}
						}
						result = flag3;
					}
				}
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002CFC File Offset: 0x00000EFC
		public virtual DataTable ToDataTable()
		{
			return (this.Police != null) ? this.Police.ToDataTable() : null;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002D24 File Offset: 0x00000F24
		public virtual string Id
		{
			get
			{
				return (this.Police != null) ? this.Police.POLICE : string.Empty;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002D50 File Offset: 0x00000F50
		public virtual string Coordinate
		{
			get
			{
				return (this.Police != null) ? this.Police.PolicemanCoordinate : string.Empty;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002D7C File Offset: 0x00000F7C
		public virtual MoveObjectType MoveObjectType
		{
			get
			{
				return (this.Police != null) ? MoveObjectType.MovePoliceMan : MoveObjectType.MoveNone;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002D9C File Offset: 0x00000F9C
		public virtual bool Refresh(string jsonStr)
		{
			Policeman policeman = JsonConvert.DeserializeObject<Policeman>(jsonStr);
			bool result = false;
			bool flag = this.Police == null;
			if (flag)
			{
				this.Police = policeman;
				result = true;
			}
			else
			{
				bool flag2 = !this.Police.Equals(policeman);
				if (flag2)
				{
					bool flag3 = this.Police.POLICE.Equals(policeman.POLICE);
					if (flag3)
					{
						this.Police = policeman;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002E10 File Offset: 0x00001010
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
				result = (this.Properties = (from p in typeof(Policeman).GetProperties().ToList<PropertyInfo>()
				where this.ValidProperty(p)
				select p).ToArray<PropertyInfo>());
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002E70 File Offset: 0x00001070
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
						result = propertyInfo.GetValue(this.Police);
					}
					else
					{
						propertyInfo = properties.ToList<PropertyInfo>().FirstOrDefault((PropertyInfo p) => this.IsAliasNameOfProperty(p, propName));
						result = ((propertyInfo == null) ? null : propertyInfo.GetValue(this.Police));
					}
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002F30 File Offset: 0x00001130
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

		// Token: 0x0600009A RID: 154 RVA: 0x00002FBC File Offset: 0x000011BC
		protected bool ValidProperty(PropertyInfo property)
		{
			bool flag = property == null;
            bool result = false;
            if (flag)
			{
				result = false;
			}
			else
			{
                return true;
				//string text = property.PropertyType.Name.ToLower();
				//uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				//if (num <= 2515107422u)
				//{
				//	if (num <= 398550328u)
				//	{
				//		if (num != 64103268u)
				//		{
				//			if (num != 132346577u)
				//			{
				//				if (num != 398550328u)
				//				{
				//					goto IL_21F;
				//				}
				//				if (!(text == "string"))
				//				{
				//					goto IL_21F;
				//				}
				//			}
				//			else if (!(text == "int16"))
				//			{
				//				goto IL_21F;
				//			}
				//		}
				//		else if (!(text == "int64"))
				//		{
				//			goto IL_21F;
				//		}
				//	}
				//	else if (num <= 848563180u)
				//	{
				//		if (num != 520654156u)
				//		{
				//			if (num != 848563180u)
				//			{
				//				goto IL_21F;
				//			}
				//			if (!(text == "uint32"))
				//			{
				//				goto IL_21F;
				//			}
				//		}
				//		else if (!(text == "decimal"))
				//		{
				//			goto IL_21F;
				//		}
				//	}
				//	else if (num != 2133018345u)
				//	{
				//		if (num != 2515107422u)
				//		{
				//			goto IL_21F;
				//		}
				//		if (!(text == "int"))
				//		{
				//			goto IL_21F;
				//		}
				//	}
				//	else if (!(text == "single"))
				//	{
				//		goto IL_21F;
				//	}
				//}
				//else if (num <= 2929723411u)
				//{
				//	if (num != 2699759368u)
				//	{
				//		if (num != 2928590578u)
				//		{
				//			if (num != 2929723411u)
				//			{
				//				goto IL_21F;
				//			}
				//			if (!(text == "uint64"))
				//			{
				//				goto IL_21F;
				//			}
				//		}
				//		else if (!(text == "uint16"))
				//		{
				//			goto IL_21F;
				//		}
				//	}
				//	else if (!(text == "double"))
				//	{
				//		goto IL_21F;
				//	}
				//}
				//else if (num <= 3270303571u)
				//{
				//	if (num != 3122818005u)
				//	{
				//		if (num != 3270303571u)
				//		{
				//			goto IL_21F;
				//		}
				//		if (!(text == "long"))
				//		{
				//			goto IL_21F;
				//		}
				//	}
				//	else if (!(text == "short"))
				//	{
				//		goto IL_21F;
				//	}
				//}
				//else if (num != 3437915536u)
				//{
				//	if (num != 4225688255u)
				//	{
				//		goto IL_21F;
				//	}
				//	if (!(text == "int32"))
				//	{
				//		goto IL_21F;
				//	}
				//}
				//else if (!(text == "datetime"))
				//{
				//	goto IL_21F;
				//}
				//bool flag2 = true;
				//goto IL_223;
				//IL_21F:
				//flag2 = false;
				//IL_223:
				//result = flag2;
			}
			return result;
		}

		// Token: 0x04000030 RID: 48
		protected Policeman Police;

		// Token: 0x04000031 RID: 49
		protected PropertyInfo[] Properties;
	}
}
