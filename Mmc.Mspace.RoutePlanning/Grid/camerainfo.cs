using System.Collections.Generic;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    public struct camerainfo
    {
        public string name;
        public float focallen;
        public float sensorwidth;
        public float sensorheight;
        public float imagewidth;
        public float imageheight;
    }

    public class CameraInfo
    {
        public List<camerainfo> CameraInfos;




    }

    //    ListElement {
    //            text:           qsTr("Sony A7R") //http://www.sony.co.uk/electronics/interchangeable-lens-cameras/ilce-qx1-body-kit/specifications
    //            sensorWidth:    35.9                 //http://www.sony.com/electronics/camera-lenses/sel16f28/specifications
    //            sensorHeight:   24.0
    //            imageWidth:     7360
    //            imageHeight:    4912
    //            focalLength:    35.0
    //        }
    //ListElement {
    //            text:           qsTr("MMC OPE")
    //            sensorWidth:    23.2
    //            sensorHeight:   15.4
    //            imageWidth:     5456
    //            imageHeight:    3632
    //            focalLength:    20.0
    //        }
    //        ListElement {
    //            text:           qsTr("MMC 3DCAM-1")
    //            sensorWidth:    13.2
    //            sensorHeight:   8.8
    //            imageWidth:     4800
    //            imageHeight:    3200
    //            focalLength:    24.0
    //        }

    //        ListElement {
    //            text:           qsTr("MMC 5100")
    //            sensorWidth:    23.5
    //            sensorHeight:   15.6
    //            imageWidth:     6000
    //            imageHeight:    4000
    //            focalLength:    35.0
    //        }
    //        ListElement {
    //            text:           qsTr("MMC 20X")
    //            sensorWidth:    36
    //            sensorHeight:   24
    //            imageWidth:     3840
    //            imageHeight:    2160
    //            focalLength:    30.8
    //        }
    //        ListElement {
    //            text:           qsTr("FLIR VUE Pro")
    //            sensorWidth:    32
    //            sensorHeight:   26
    //            imageWidth:     640
    //            imageHeight:    512
    //            focalLength:    35.0
    //        }

}