using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Models.VideoMonitor;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mmc.Mspace.Services
{
    public class CameraInfoService : Singleton<CameraInfoService>, ICameraInfoService
    {
        public void LoadData()
        {
            IDisplayLayer disPlayLayerByFCAliasName = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
            IFeatureClass fc = disPlayLayerByFCAliasName.Fc;
            IFdeCursor fdeCursor = fc.Search(null, false);
            IFieldInfoCollection fields = fc.GetFields();
            IRowBuffer rowBuffer;
            while ((rowBuffer = fdeCursor.NextRow()) != null)
            {
                CameraInfo cameraInfo = new CameraInfo();
                cameraInfo.Name = rowBuffer.GetValue(fields.IndexOf("MC")).ToString();
                cameraInfo.Fid = rowBuffer.GetValue(fields.IndexOf("BH")).ToString();
                cameraInfo.CType = StringExtension.ParseTo<int>(rowBuffer.GetValue(fields.IndexOf("KSSDLX")), 0);
                cameraInfo.Deepth = (double)StringExtension.ParseTo<int>(rowBuffer.GetValue(fields.IndexOf("KSSD")), 0);
                cameraInfo.Heading = (double)StringExtension.ParseTo<float>(rowBuffer.GetValue(fields.IndexOf("SPJD")), 0f);
                cameraInfo.HeadRange = (double)StringExtension.ParseTo<float>(rowBuffer.GetValue(fields.IndexOf("SPJJJ")), 0f);
                cameraInfo.Tilt = (double)StringExtension.ParseTo<float>(rowBuffer.GetValue(fields.IndexOf("CZJD")), 0f);
                cameraInfo.TiltRange = (double)StringExtension.ParseTo<float>(rowBuffer.GetValue(fields.IndexOf("CZJJJ")), 0f);
                IPoint ptCenter = (IPoint)rowBuffer.GetValue(fields.IndexOf("Geometry"));
                cameraInfo.PtCenter = ptCenter;
                this._lstCamera.Add(cameraInfo);
                FdeCoreExtension.ReleaseComObject(rowBuffer);
            }
            FdeCoreExtension.ReleaseComObject(fdeCursor);
            FdeCoreExtension.ReleaseComObject(fields);
        }

        public List<CameraInfo> GetCameraInfos()
        {
            return this._lstCamera;
        }

        public CameraInfo GetCameraInfo(string cId)
        {
            return this._lstCamera.FirstOrDefault((CameraInfo p) => p.Fid == cId);
        }

        public string GetVideoPath(string cId)
        {
            return Application.StartupPath + "\\data\\video\\Wildlife.mp4";
        }

        public string GetDeviceId(string fcGuid, int fid)
        {
            bool flag = IDictionaryExtension.HasValues<int, string>(this.deviceDny) && this.deviceDny.ContainsKey(fid);
            string result;
            if (flag)
            {
                result = this.deviceDny[fid];
            }
            else
            {
                this.GetVedioFeatureClass();
                bool flag2 = CameraInfoService._videoFC == null || !CameraInfoService._videoFC.Guid.ToString().Equals(fcGuid);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    IFieldInfoCollection fields = CameraInfoService._videoFC.GetFields();
                    int num = fields.IndexOf("bh");
                    bool flag3 = num < 0;
                    if (flag3)
                    {
                        result = string.Empty;
                    }
                    else
                    {
                        QueryFilter queryFilter = new QueryFilter
                        {
                            WhereClause = string.Format("{0}={1}", "oid", fid)
                        };
                        IFdeCursor fdeCursor = CameraInfoService._videoFC.Search(queryFilter, true);
                        IRowBuffer rowBuffer = null;
                        string text = string.Empty;
                        try
                        {
                            while ((rowBuffer = fdeCursor.NextRow()) != null)
                            {
                                bool flag4 = !string.IsNullOrEmpty(text);
                                if (flag4)
                                {
                                    break;
                                }
                                text = rowBuffer.GetValue(num).ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            bool flag5 = rowBuffer != null;
                            if (flag5)
                            {
                                FdeCoreExtension.ReleaseComObject(rowBuffer);
                            }
                            bool flag6 = fdeCursor != null;
                            if (flag6)
                            {
                                FdeCoreExtension.ReleaseComObject(fdeCursor);
                            }
                            bool flag7 = queryFilter != null;
                            if (flag7)
                            {
                                FdeCoreExtension.ReleaseComObject(queryFilter);
                            }
                        }
                        result = text;
                    }
                }
            }
            return result;
        }

        public List<IRenderable> GetDrawCameraViews()
        {
            List<IRenderable> trs = new List<IRenderable>();
            this._lstCamera.ForEach(delegate (CameraInfo P)
            {
                trs.Add(this.GetDrawCameraView(P.Fid.ToString()));
            });
            return trs;
        }

        public static CameraInfoService GetDefault(object args = null)
        {
            return Singleton<CameraInfoService>.Instance;
        }

        public IRenderable GetDrawCameraView(string cId)
        {
            CameraInfo cameraInfo = this.GetCameraInfo(cId);
            return this.CameraInfoTo3DRectBase(cameraInfo);
        }

        private void GetVedioFeatureClass()
        {
            bool flag = CameraInfoService._videoFC == null;
            if (flag)
            {
                IDisplayLayer disPlayLayerByFCAliasName = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
                CameraInfoService._videoFC = ((disPlayLayerByFCAliasName != null) ? disPlayLayerByFCAliasName.Fc : null);
            }
        }

        private IRenderable CameraInfoTo3DRectBase(CameraInfo c)
        {
            IPosition position = new Position();
            position.Heading = c.Heading;
            position.Tilt = c.Tilt;
            IEulerAngle eulerAngle = new EulerAngle();
            eulerAngle.Heading = c.Heading;
            eulerAngle.Tilt = c.Tilt - 90.0;
            IPoint aimingPoint = GviMap.MapControl.Camera.GetAimingPoint2(c.PtCenter, eulerAngle, c.Deepth);
            IPoint point = aimingPoint.Clone() as IPoint;
            point.SpatialCRS = c.PtCenter.SpatialCRS;
            point.Project(GviMap.SpatialCrs);
            position.X = point.X;
            position.Y = point.Y;
            position.Altitude = point.Z;
            FdeGeometryRelease.ReleaseComObject(aimingPoint);
            FdeGeometryRelease.ReleaseComObject(point);
            bool flag = c.CType == 0;
            IRenderable result;
            if (flag)
            {
                double objectWidth = c.Deepth / Math.Cos(this.Angle2Rad(c.HeadRange / 2.0));
                double objectDepth = c.Deepth / Math.Cos(this.Angle2Rad(c.TiltRange / 2.0));
                result = GviMap.MapControl.ObjectManager.CreatePyramid(position, objectWidth, objectDepth, c.Deepth, ColorConvert.UintToColor(4294967040u), ColorConvert.UintToColor(1728053247u), default(Guid));
            }
            else
            {
                double radius = c.Deepth / Math.Tan(c.HeadRange / 2.0);
                result = GviMap.MapControl.ObjectManager.CreateCone(position, radius, c.Deepth, ColorConvert.UintToColor(4294967040u), ColorConvert.UintToColor(1728053247u), 12, default(Guid));
            }
            return result;
        }

        public double Angle2Rad(double angle)
        {
            return angle / 180.0 * 3.1415926535897931;
        }

        private readonly List<CameraInfo> _lstCamera = new List<CameraInfo>();

        private static IFeatureClass _videoFC;

        private readonly Dictionary<int, string> deviceDny = new Dictionary<int, string>();
    }
}