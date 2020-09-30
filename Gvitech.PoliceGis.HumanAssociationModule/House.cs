using System;
using System.Collections.ObjectModel;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.HumanAssociationModule
{
	// Token: 0x02000005 RID: 5
	public class House : BindableBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002879 File Offset: 0x00000A79
		public House()
		{
			this.Peoples = new ObservableCollection<People>();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000288F File Offset: 0x00000A8F
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002897 File Offset: 0x00000A97
		public string ID { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000028A8 File Offset: 0x00000AA8
		public DateTime RecordDate { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000028B1 File Offset: 0x00000AB1
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000028B9 File Offset: 0x00000AB9
		public string Owner { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000028C2 File Offset: 0x00000AC2
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000028CA File Offset: 0x00000ACA
		public string OwnerID { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000028D3 File Offset: 0x00000AD3
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000028DB File Offset: 0x00000ADB
		public string Sex { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000028E4 File Offset: 0x00000AE4
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000028EC File Offset: 0x00000AEC
		public string Address { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000028F5 File Offset: 0x00000AF5
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000028FD File Offset: 0x00000AFD
		public string Tel { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002906 File Offset: 0x00000B06
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000290E File Offset: 0x00000B0E
		public string Recorder { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002917 File Offset: 0x00000B17
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000291F File Offset: 0x00000B1F
		public DateTime StartDate { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002930 File Offset: 0x00000B30
		public DateTime EndDate { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002939 File Offset: 0x00000B39
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002941 File Offset: 0x00000B41
		public string ManagerAddress { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000294A File Offset: 0x00000B4A
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002952 File Offset: 0x00000B52
		public string ManagerName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000295B File Offset: 0x00000B5B
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002963 File Offset: 0x00000B63
		public string ManagerTel { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000296C File Offset: 0x00000B6C
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002974 File Offset: 0x00000B74
		public string Name { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000297D File Offset: 0x00000B7D
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002985 File Offset: 0x00000B85
		public ObservableCollection<People> Peoples { get; set; }
	}
}
