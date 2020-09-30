using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Framework.Services
{
   public class ImageLabelManager
    {
        private Dictionary<string, Tuple<ILabel, ITerrainImageLabel>> _imageLabelDic = new Dictionary<string, Tuple<ILabel, ITerrainImageLabel>>();

        public IPosition GetPosition(double x, double y, double z, gviAltitudeType altitudeType = gviAltitudeType.gviAltitudeTerrainAbsolute)
        {
            return new Position() { X = x, Y = y, Altitude = z, AltitudeType = altitudeType };
        }

        public ILabelStyle GetLabelStyle(Color bgcolor, Color iconColor, gviLineToGroundType lineToGroundType= gviLineToGroundType.gviLineTypeNone,int fontsize = 30)
        {
            return new LabelStyle() { BackgroundColor = bgcolor, FontSize = fontsize, IconColor = iconColor,  LineToGround = lineToGroundType };
        }

        public ITerrainImageLabel CreateImageLabel(IPosition position, string imagePath, ILabelStyle style)
        {
            return GviMap.ObjectManager.CreateImageLabel(position, imagePath, style, GviMap.ProjectTree.RootID);
        }

        public bool DeleteImageLabel(ITerrainImageLabel imageLabel)
        {
            if (imageLabel != null)
            {
                GviMap.ObjectManager.DeleteObject(imageLabel.Guid);
                return true;
            }
            else
                return false;
        }

        public bool DeleteLabel(ILabel label)
        {
            if (label != null)
            {
                GviMap.ObjectManager.DeleteObject(label.Guid);
                return true;
            }
            else
                return false;
        }

        public Tuple<ILabel,ITerrainImageLabel> GetImageLabelItem(string key)
        {
            if (_imageLabelDic.ContainsKey(key))
                return _imageLabelDic[key];
            return null;
        }

        public bool UpdateImageLabelItem(string key, ILabel label, ITerrainImageLabel imageLabel)
        {
            if (_imageLabelDic.ContainsKey(key))
            {
                _imageLabelDic[key] = new Tuple<ILabel, ITerrainImageLabel>(label, imageLabel);
                return true;
            }else
                return false;
        }

        public bool DeleteImageLabelItem(string key)
        {
            var item = GetImageLabelItem(key);
            if (item == null) return false;
            bool res1 = DeleteLabel(item.Item1);
            bool res2 = DeleteImageLabel(item.Item2);
            if (res1 && res2)
            {
                _imageLabelDic.Remove(key);
                return true;
            }
            else
                return false;
        }

        public bool AddImageLabelItem(string key, ILabel label, ITerrainImageLabel imageLabel)
        {
            if (!_imageLabelDic.ContainsKey(key))
            {
                _imageLabelDic.Add(key, new Tuple<ILabel, ITerrainImageLabel>(label, imageLabel));
                return true;
            }
            else
                return false;
        }

        public void Clear()
        {
            var keys = _imageLabelDic.Keys.ToList();
            if(keys.Count>0)
                foreach (var key in keys)
                    DeleteImageLabelItem(key);
            keys.Clear();
            keys = null;
            _imageLabelDic.Clear();
        }

    }
}
