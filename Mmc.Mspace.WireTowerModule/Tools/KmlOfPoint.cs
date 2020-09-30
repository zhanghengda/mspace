using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Mmc.Mspace.WireTowerModule.Tools
{
    public class Document
    {

    }

    public class Placemark
    {
        public Placemark() { }
        public Placemark(double x, double y, double z, int order)
        {
            Point = new Point()
            {
                coordinates = string.Join(",", x, y, z),
                drawOrder = order
            };
        }
        public string name { get; set; }
        public string styleUrl { get; set; }
        public Point Point { get; set; }
    }

    public class Point
    {
        public string coordinates { get; set; }
        public int drawOrder { get; set; }
    }

    public class kml
    {
        [XmlIgnore]
        public List<Placemark> PlacemarkList { get; set; } = new List<Placemark>();
        public string Document { get; set; } = string.Empty;

        private XmlNode AddNameNode(XmlDocument xmlDoc,string name)
        {
            XmlNode nameNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", "");
            nameNode.InnerText = name;

            return nameNode;
        }

        private XmlNode AddMapStyleNode(XmlDocument xmlDoc)
        {
            XmlNode styleMap = xmlDoc.CreateNode(XmlNodeType.Element, "StyleMap", "");
            XmlAttribute attr = styleMap.OwnerDocument.CreateAttribute("id");
            attr.Value = "m_ylw-pushpin";
            styleMap.Attributes.Append(attr);

            XmlNode iconStyle1 = xmlDoc.CreateNode(XmlNodeType.Element, "Pair", "");
            XmlNode key1 = xmlDoc.CreateNode(XmlNodeType.Element, "key", "");
            key1.InnerText = "normal";
            XmlNode styleUrl1 = xmlDoc.CreateNode(XmlNodeType.Element, "styleUrl", "");
            styleUrl1.InnerText = "#s_ylw-pushpin";
            iconStyle1.AppendChild(key1);
            iconStyle1.AppendChild(styleUrl1);

            XmlNode iconStyle2 = xmlDoc.CreateNode(XmlNodeType.Element, "Pair", "");
            XmlNode key2 = xmlDoc.CreateNode(XmlNodeType.Element, "key", "");
            key2.InnerText = "highlight";
            XmlNode styleUrl2 = xmlDoc.CreateNode(XmlNodeType.Element, "styleUrl", "");
            styleUrl2.InnerText = "#s_ylw-pushpin_hl";
            iconStyle2.AppendChild(key2);
            iconStyle2.AppendChild(styleUrl2);

            styleMap.AppendChild(iconStyle1);
            styleMap.AppendChild(iconStyle2);

            return styleMap;
        }

        private XmlNode AddStyleNode(XmlDocument xmlDoc, string colorStr= "ff1987ff", string scaleStr ="1.2")
        {
            XmlNode style = xmlDoc.CreateNode(XmlNodeType.Element, "Style", "");
            XmlAttribute attr = style.OwnerDocument.CreateAttribute("id");
            attr.Value = "s_ylw-pushpin_hl";
            style.Attributes.Append(attr);

            XmlNode iconStyle = xmlDoc.CreateNode(XmlNodeType.Element, "IconStyle", "");
            XmlNode color = xmlDoc.CreateNode(XmlNodeType.Element, "color", "");
            color.InnerText = colorStr;
            XmlNode scale = xmlDoc.CreateNode(XmlNodeType.Element, "scale", "");
            scale.InnerText = scaleStr;
            
            XmlNode icon = xmlDoc.CreateNode(XmlNodeType.Element, "Icon", "");
            XmlNode href = xmlDoc.CreateNode(XmlNodeType.Element, "href", "");
            href.InnerText = "http://maps.google.com/mapfiles/kml/shapes/target.png";
                        
            iconStyle.AppendChild(color);
            iconStyle.AppendChild(scale);
            icon.AppendChild(href);
            iconStyle.AppendChild(icon);
            style.AppendChild(iconStyle);

            return style;
        }

        private XmlNode AddPlacemarkNode(XmlDocument xmlDoc, string name, string coordinate)
        {
            XmlNode PlacemarkNode = xmlDoc.CreateNode(XmlNodeType.Element, "Placemark", "");

            XmlNode nameNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", "");
            nameNode.InnerText = name;

            XmlNode styleUrl1 = xmlDoc.CreateNode(XmlNodeType.Element, "styleUrl", "");
            styleUrl1.InnerText = "#s_ylw-pushpin";

            XmlNode pointNode = xmlDoc.CreateNode(XmlNodeType.Element, "Point", "");
            XmlNode pointOrder = xmlDoc.CreateNode(XmlNodeType.Element, "gx:drawOrder", "");
            pointOrder.InnerText = name;
            XmlNode pointCoor = xmlDoc.CreateNode(XmlNodeType.Element, "coordinates", "");
            pointCoor.InnerText = coordinate;

            pointNode.AppendChild(pointOrder);
            pointNode.AppendChild(pointCoor);

            PlacemarkNode.AppendChild(nameNode);
            PlacemarkNode.AppendChild(styleUrl1);
            PlacemarkNode.AppendChild(pointNode);

            return PlacemarkNode;
        }

        public void SaveToFile(string filePath)
        {
            if (PlacemarkList?.Count <= 0) return;

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(sw, this);
                }
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);


            XmlNode kmlNode = xmlDoc.SelectSingleNode(@"/kml");
            kmlNode.Attributes.RemoveAll();

            XmlAttribute attr1 = kmlNode.OwnerDocument.CreateAttribute("xmlns:gx");
            attr1.Value = "http://earth.google.com/kml/ext/2.2";
            kmlNode.Attributes.Append(attr1);
            XmlAttribute attr2 = kmlNode.OwnerDocument.CreateAttribute("xmlns:kml");
            attr2.Value = "http://www.opengis.net/kml/2.2";
            kmlNode.Attributes.Append(attr2);
            XmlAttribute attr3 = kmlNode.OwnerDocument.CreateAttribute("xmlns:atom");
            attr3.Value = "http://www.w3.org/2005/Atom";
            kmlNode.Attributes.Append(attr3);

            XmlNode documentNode = xmlDoc.SelectSingleNode(@"/kml/Document");


            //XmlNode nameNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", "");
            //nameNode.InnerText = Path.GetFileName(filePath);

            XmlNode nameNode = documentNode.AppendChild(AddNameNode(xmlDoc, Path.GetFileName(filePath)));

            XmlNode mapStyleNode = documentNode.AppendChild(AddMapStyleNode(xmlDoc));
            XmlNode styleNode = documentNode.AppendChild(AddStyleNode(xmlDoc));

            XmlNode placemarkNode;
            for (int i = 0; i < PlacemarkList.Count; i++)
            {
                var item = PlacemarkList[i];
                placemarkNode = documentNode.AppendChild(AddPlacemarkNode(xmlDoc, item.Point.drawOrder.ToString(), item.Point.coordinates));
            }

            xmlDoc.Save(filePath);
        }

        public List<Placemark> ReadkmlFile(string filePath)
        {
            List<Placemark> tempList = new List<Placemark>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode documentNode = xmlDoc.SelectSingleNode(@"/kml/Document");

            if (documentNode.HasChildNodes)
            {
                string tempStr = string.Empty;
                string markStr = string.Empty;
                dynamic dynamic;
                Placemark model = new Placemark();
                foreach (var child in documentNode.ChildNodes)
                {
                    var temp = child as XmlNode;
                    if (temp.Name.ToLower().Equals("placemark"))
                    {
                        tempStr = Newtonsoft.Json.JsonConvert.SerializeXmlNode(temp);
                        dynamic = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(tempStr);
                        try
                        {
                            markStr = Newtonsoft.Json.JsonConvert.SerializeObject(dynamic.Placemark);
                            model = Newtonsoft.Json.JsonConvert.DeserializeObject<Placemark>(markStr);
                            tempList.Add(model);
                        }
                        catch { }
                    }
                }
            }

            return tempList;
        }
    }
}
