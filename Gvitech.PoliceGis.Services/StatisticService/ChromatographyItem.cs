using System.Xml.Serialization;

namespace Mmc.Mspace.Services.StatisticService
{
    public class ChromatographyItem
    {
        [XmlAttribute]
        public int MinValue { get; set; }

        [XmlAttribute]
        public int MaxValue { get; set; }

        [XmlAttribute]
        public uint Color { get; set; }

        public string Item
        {
            get
            {
                return (!this.MaxValue.Equals(int.MaxValue)) ? string.Format("{0}-{1}", this.MinValue, this.MaxValue) : string.Format("{0}以上", this.MinValue);
            }
        }
    }
}