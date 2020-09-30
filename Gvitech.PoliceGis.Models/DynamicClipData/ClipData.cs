using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Models;
using Gvitech.CityMaker.RenderControl;

using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mmc.Mspace.Models.DynamicClipData
{
    public class ClipData : IUserInfo
    {
        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000007 RID: 7 RVA: 0x000020BD File Offset: 0x000002BD
        // (set) Token: 0x06000008 RID: 8 RVA: 0x000020C5 File Offset: 0x000002C5
        public VectorData Angle { get; set; }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000009 RID: 9 RVA: 0x000020CE File Offset: 0x000002CE
        // (set) Token: 0x0600000A RID: 10 RVA: 0x000020D6 File Offset: 0x000002D6
        public VectorData Position { get; set; }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x0600000B RID: 11 RVA: 0x000020DF File Offset: 0x000002DF
        // (set) Token: 0x0600000C RID: 12 RVA: 0x000020E7 File Offset: 0x000002E7
        public VectorData BoxCenter { get; set; }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x0600000D RID: 13 RVA: 0x000020F0 File Offset: 0x000002F0
        // (set) Token: 0x0600000E RID: 14 RVA: 0x000020F8 File Offset: 0x000002F8
        public VectorData BoxSize { get; set; }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x0600000F RID: 15 RVA: 0x00002101 File Offset: 0x00000301
        // (set) Token: 0x06000010 RID: 16 RVA: 0x00002109 File Offset: 0x00000309
       
        public string Name
        {
            get;
            set;
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000011 RID: 17 RVA: 0x00002112 File Offset: 0x00000312
        // (set) Token: 0x06000012 RID: 18 RVA: 0x0000211A File Offset: 0x0000031A
        public int Type { get; set; }

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x06000013 RID: 19 RVA: 0x00002123 File Offset: 0x00000323
        // (set) Token: 0x06000014 RID: 20 RVA: 0x0000212B File Offset: 0x0000032B
        public string Guid { get; set; }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x06000015 RID: 21 RVA: 0x00002134 File Offset: 0x00000334
        // (set) Token: 0x06000016 RID: 22 RVA: 0x0000213C File Offset: 0x0000033C
        public string ImageName { get; set; }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000017 RID: 23 RVA: 0x00002145 File Offset: 0x00000345
        // (set) Token: 0x06000018 RID: 24 RVA: 0x0000214D File Offset: 0x0000034D
        public double Heading { get; set; }

        public double Roll { get; set; }

        public double Tilt { get; set; }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x0600001D RID: 29 RVA: 0x000022B0 File Offset: 0x000004B0
        // (set) Token: 0x0600001E RID: 30 RVA: 0x000022B8 File Offset: 0x000004B8
        public double X { get; set; }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x0600001F RID: 31 RVA: 0x000022C1 File Offset: 0x000004C1
        // (set) Token: 0x06000020 RID: 32 RVA: 0x000022C9 File Offset: 0x000004C9
        public double Y { get; set; }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000021 RID: 33 RVA: 0x000022D2 File Offset: 0x000004D2
        // (set) Token: 0x06000022 RID: 34 RVA: 0x000022DA File Offset: 0x000004DA
        public double Z { get; set; }
      
      //  public CameraProperty Camera { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        /* [LiteDB.BsonIgnore]
        // Token: 0x06000019 RID: 25 RVA: 0x00002156 File Offset: 0x00000356
        public static ClipData GetClipOperaToData(IClipPlaneOperation cpo, CameraProperty camera)
        {
            if (cpo == null)
            {
                return null;
            }
            ClipData clipOperaToData = ClipData.GetClipOperaToData(cpo);
            clipOperaToData.Camera = camera;
            return clipOperaToData;
        }
        
       [LiteDB.BsonIgnore]
        // Token: 0x0600001A RID: 26 RVA: 0x0000216C File Offset: 0x0000036C
       public static ClipData GetClipOperaToData(IClipPlaneOperation cpo)
        {
            if (cpo == null)
            {
                return null;
            }
            ClipData clipData = new ClipData();
            if (cpo.ClipPlaneOperationType == gviClipPlaneOperation.gviSingleClipOperation)
            {
                IVector3 vector = null;
                IEulerAngle eulerAngle = null;
                cpo.GetSingleClip(out vector, out eulerAngle);
                clipData.Position = new VectorData
                {
                    X = vector.X,
                    Y = vector.Y,
                    Z = vector.Z
                };
                clipData.Angle = new VectorData
                {
                    X = eulerAngle.Heading,
                    Y = eulerAngle.Tilt,
                    Z = eulerAngle.Roll
                };
                clipData.Type = 0;
            }
            else
            {
                IVector3 vector = null;
                IVector3 vector2 = null;
                IEulerAngle eulerAngle = null;
                cpo.GetBoxClip(out vector, out vector2, out eulerAngle);
                clipData.Position = new VectorData
                {
                    X = vector.X,
                    Y = vector.Y,
                    Z = vector.Z
                };
                clipData.BoxSize = new VectorData
                {
                    X = vector2.X,
                    Y = vector2.Y,
                    Z = vector2.Z
                };
                clipData.Angle = new VectorData
                {
                    X = eulerAngle.Heading,
                    Y = eulerAngle.Tilt,
                    Z = eulerAngle.Roll
                };
                clipData.Type = 1;
            }
            clipData.Name = cpo.Name;
            clipData.Guid = cpo.Guid.ToString();
            return clipData;
        }
        [LiteDB.BsonIgnore]
        // Token: 0x0600001B RID: 27 RVA: 0x00002230 File Offset: 0x00000430
        public static void ClipDataToOpera(ref IClipPlaneOperation cpo, ClipData cd)
        {
            if (cpo == null || cd == null)
            {
                return;
            }
            if (cd.Type == 0)
            {
                IVector3 vector = new Vector3();
                vector.Set(cd.Position.X, cd.Position.Y, cd.Position.Z);
                IEulerAngle eulerAngle = new EulerAngle();
                eulerAngle.Set(cd.Angle.X, cd.Angle.Y, cd.Angle.Z);
                cpo.SetSingleClip(vector, eulerAngle);
            }
            if (cd.Type == 1)
            {
                IVector3 vector = new Vector3();
                IVector3 vector2 = new Vector3();
                vector.Set(cd.Position.X, cd.Position.Y, cd.Position.Z);
                vector2.Set(cd.BoxSize.X, cd.BoxSize.Y, cd.BoxSize.Z);
                IEulerAngle eulerAngle = new EulerAngle();
                eulerAngle.Set(cd.Angle.X, cd.Angle.Y, cd.Angle.Z);
                cpo.SetBoxClip(vector, vector2, eulerAngle);
            }
        }*/
    }
  
}
