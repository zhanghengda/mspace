using System;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;


namespace Mmc.Mspace.Models.VideoMonitor
{
    // Token: 0x02000005 RID: 5
    public class CameraInfo
    {
        // Token: 0x1700000E RID: 14
        // (get) Token: 0x0600001E RID: 30 RVA: 0x0000214C File Offset: 0x0000034C
        // (set) Token: 0x0600001F RID: 31 RVA: 0x00002154 File Offset: 0x00000354
        public string Fid { get; set; }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x06000020 RID: 32 RVA: 0x0000215D File Offset: 0x0000035D
        // (set) Token: 0x06000021 RID: 33 RVA: 0x00002165 File Offset: 0x00000365
        public int CType { get; set; }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x06000022 RID: 34 RVA: 0x0000216E File Offset: 0x0000036E
        // (set) Token: 0x06000023 RID: 35 RVA: 0x00002176 File Offset: 0x00000376
        public string Name { get; set; }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000024 RID: 36 RVA: 0x0000217F File Offset: 0x0000037F
        // (set) Token: 0x06000025 RID: 37 RVA: 0x00002187 File Offset: 0x00000387
        public string IPAdress { get; set; }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000026 RID: 38 RVA: 0x00002190 File Offset: 0x00000390
        // (set) Token: 0x06000027 RID: 39 RVA: 0x00002198 File Offset: 0x00000398
        public string Port { get; set; }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000028 RID: 40 RVA: 0x000021A1 File Offset: 0x000003A1
        // (set) Token: 0x06000029 RID: 41 RVA: 0x000021A9 File Offset: 0x000003A9
        public string StreamId { get; set; }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x0600002A RID: 42 RVA: 0x000021B2 File Offset: 0x000003B2
        // (set) Token: 0x0600002B RID: 43 RVA: 0x000021BA File Offset: 0x000003BA
        public string PipeName { get; set; }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x0600002C RID: 44 RVA: 0x000021C3 File Offset: 0x000003C3
        // (set) Token: 0x0600002D RID: 45 RVA: 0x000021CB File Offset: 0x000003CB
        public string Coor { get; set; }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x0600002E RID: 46 RVA: 0x000021D4 File Offset: 0x000003D4
        // (set) Token: 0x0600002F RID: 47 RVA: 0x000021DC File Offset: 0x000003DC
        public double Heading { get; set; }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x06000030 RID: 48 RVA: 0x000021E5 File Offset: 0x000003E5
        // (set) Token: 0x06000031 RID: 49 RVA: 0x000021ED File Offset: 0x000003ED
        public double HeadRange { get; set; }

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x06000032 RID: 50 RVA: 0x000021F6 File Offset: 0x000003F6
        // (set) Token: 0x06000033 RID: 51 RVA: 0x000021FE File Offset: 0x000003FE
        public double Tilt { get; set; }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x06000034 RID: 52 RVA: 0x00002207 File Offset: 0x00000407
        // (set) Token: 0x06000035 RID: 53 RVA: 0x0000220F File Offset: 0x0000040F
        public double TiltRange { get; set; }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x06000036 RID: 54 RVA: 0x00002218 File Offset: 0x00000418
        // (set) Token: 0x06000037 RID: 55 RVA: 0x00002220 File Offset: 0x00000420
        public double Deepth { get; set; }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x06000038 RID: 56 RVA: 0x00002229 File Offset: 0x00000429
        // (set) Token: 0x06000039 RID: 57 RVA: 0x00002231 File Offset: 0x00000431
        public IEulerAngle Angle { get; set; }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x0600003A RID: 58 RVA: 0x0000223A File Offset: 0x0000043A
        // (set) Token: 0x0600003B RID: 59 RVA: 0x00002242 File Offset: 0x00000442
        public IPoint PtCenter { get; set; }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x0600003C RID: 60 RVA: 0x0000224B File Offset: 0x0000044B
        // (set) Token: 0x0600003D RID: 61 RVA: 0x00002253 File Offset: 0x00000453
        public Guid GuidViewErea { get; set; }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x0600003E RID: 62 RVA: 0x0000225C File Offset: 0x0000045C
        // (set) Token: 0x0600003F RID: 63 RVA: 0x00002264 File Offset: 0x00000464
        public Guid GuidCamera { get; set; }

        // Token: 0x06000040 RID: 64 RVA: 0x00002270 File Offset: 0x00000470
        public static CameraInfo GetTestCameraInfo()
        {
            var pt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
            pt.SetPostion(106.222, 38.48789, 100);
            return new CameraInfo
            {
                PtCenter = pt,
                Heading = 100.0,
                Tilt = 45.0,
                HeadRange = 120.0,
                TiltRange = 90.0,
                Deepth = 100.0
            };
        }
    }
}
