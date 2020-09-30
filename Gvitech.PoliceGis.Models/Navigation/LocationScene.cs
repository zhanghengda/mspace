using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.Navigation
{
    public class LocationScene
    {
        private string _comment;

        private double _duration;

        private byte[] _ImageObject;

        private int _index;

        private string _location;

        private int _locationGroupID;

        private int _locationID;

        private string _locationName;

        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public double Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value;
            }
        }

        //public Image ImageLocation
        //{
        //    get
        //    {
        //        return ImageTool.Byte2Image(this._ImageObject);
        //    }
        //    set
        //    {
        //        this._ImageObject = ImageTool.Image2Byte(value);
        //    }
        //}

        public byte[] ImageObject
        {
            get
            {
                return this._ImageObject;
            }
            set
            {
                this._ImageObject = value;
            }
        }

        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        public int LocationGroupID
        {
            get
            {
                return this._locationGroupID;
            }
            set
            {
                this._locationGroupID = value;
            }
        }

        public int LocationID
        {
            get
            {
                return this._locationID;
            }
            set
            {
                this._locationID = value;
            }
        }
        public int LocationIndex
        {
            get
            {
                return this._index;
            }
            set
            {
                this._index = value;
            }
        }

        public string LocationName
        {
            get
            {
                return this._locationName;
            }
            set
            {
                this._locationName = value;
            }
        }
        public void FlyTo()
        {
            try
            {
                if (!string.IsNullOrEmpty(this._location))
                {
                    string[] array = this._location.Split(new char[]
                    {
                        ';'
                    });
                    double x;
                    double.TryParse(array[0], out x);
                    double y;
                    double.TryParse(array[1], out y);
                    double z;
                    double.TryParse(array[2], out z);
                    double heading;
                    double.TryParse(array[3], out heading);
                    double tilt;
                    double.TryParse(array[4], out tilt);
                    double roll;
                    double.TryParse(array[5], out roll);
                    
                    var pt = GviMap.GeoFactory.CreatePoint(Gvitech.CityMaker.FdeGeometry.gviVertexAttribute.gviVertexAttributeZ );
                    pt.SetPostion(x, y, z);
                    var angle = new EulerAngle();
                    angle.Set(heading, tilt, roll);
                    GviMap.Camera.SetCamera(pt.Position, angle, Gvitech.CityMaker.RenderControl.gviSetCameraFlags.gviSetCameraNoFlags);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this._locationName))
            {
                return string.Empty;
            }
            return this._locationName;
        }
    }
}
