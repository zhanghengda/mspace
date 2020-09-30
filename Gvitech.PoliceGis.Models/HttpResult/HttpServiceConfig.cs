using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Models.HttpResult
{
	// Token: 0x0200001B RID: 27
	public class HttpServiceConfig
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004643 File Offset: 0x00002843
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000464B File Offset: 0x0000284B
		public List<HttpServiceParam> HttpServices { get; set; }

		// Token: 0x06000148 RID: 328 RVA: 0x00004654 File Offset: 0x00002854
		public static HttpServiceConfig CreateConfig()
		{
			return new HttpServiceConfig
			{
				HttpServices = new List<HttpServiceParam>
				{
					new HttpServiceParam
					{
						Name = "警员信息服务",
						Url = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_016",
						Unobstructed = true
					},
					new HttpServiceParam
					{
						Name = "视频信息服务",
						Url = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_017",
						Unobstructed = true
					},
					new HttpServiceParam
					{
						Name = "人口信息服务",
						Url = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_018",
						Unobstructed = true
					},
					new HttpServiceParam
					{
						Name = "案件信息服务",
						Url = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_019",
						Unobstructed = true
					},
					new HttpServiceParam
					{
						Name = "专题案件信息服务",
						Url = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_010",
						Unobstructed = true
					}
				}
			};
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004764 File Offset: 0x00002964
		public HttpServiceParam GetServiceParam(string serviceName)
		{
			bool flag = string.IsNullOrEmpty(serviceName);
			HttpServiceParam result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.HttpServices.Find((HttpServiceParam hs) => !string.IsNullOrEmpty(hs.Name) && hs.Name.ToUpper().Equals(serviceName.ToUpper()));
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000047B0 File Offset: 0x000029B0
		public bool IsValid()
		{
			return IEnumerableExtension.HasValues<HttpServiceParam>(this.HttpServices);
		}
	}
}
