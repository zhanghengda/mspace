using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public enum esriDirectionsManeuverType
    {
        esriDMTUnknown,

        esriDMTStop,

        esriDMTStraight,

        esriDMTBearLeft,

        esriDMTBearRight,

        esriDMTTurnLeft,

        esriDMTTurnRight,

        esriDMTSharpLeft,

        esriDMTSharpRight,

        esriDMTUTurn,

        esriDMTFerry,

        esriDMTRoundabout,

        esriDMTHighwayMerge,

        esriDMTHighwayExit,

        esriDMTHighwayChange,

        esriDMTForkCenter,

        esriDMTForkLeft,

        esriDMTForkRight,

        esriDMTDepart,

        esriDMTTripItem,

        esriDMTEndOfFerry,

        esriDMTRampRight,

        esriDMTRampLeft,

        esriDMTTurnLeftRight,

        esriDMTTurnRightLeft,

        esriDMTTurnRightRight,

        esriDMTTurnLeftLeft
    }
}