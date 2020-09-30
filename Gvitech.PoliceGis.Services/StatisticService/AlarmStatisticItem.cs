using System.Xml.Serialization;

namespace Mmc.Mspace.Services.StatisticService
{
    public class AlarmStatisticItem
    {
        [XmlAttribute]
        public string PoliceStationName { get; set; }

        [XmlAttribute]
        public int TotalAlarm { get; set; }

        [XmlAttribute]
        public int EffectiveAlarm { get; set; }

        [XmlAttribute]
        public int InvalidAlarm { get; set; }

        [XmlAttribute]
        public int SecurityAlarm { get; set; }

        [XmlAttribute]
        public int CriminalAlarm { get; set; }

        [XmlAttribute]
        public int RescueAlarm { get; set; }

        [XmlAttribute]
        public int TrafficAlarm { get; set; }

        [XmlAttribute]
        public int FireAlarm { get; set; }

        [XmlAttribute]
        public int OthorAlarm { get; set; }

        [XmlAttribute]
        public int DisputeAlarm { get; set; }

        [XmlAttribute]
        public int ReportAlarm { get; set; }

        [XmlAttribute]
        public int IncidentAlarm { get; set; }

        [XmlAttribute]
        public int DisasterAlarm { get; set; }

        [XmlAttribute]
        public int OtherAdministrativeAlarm { get; set; }
    }
}