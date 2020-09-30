/*
public void loadKmlFile()
{
    kml loadkml = new kml();
            
    GeoCoordinate coord1 = new GeoCoordinate(22.7065325, 113.9616394,  1); 
    GeoCoordinate coord2 = new GeoCoordinate(22.7065325, 113.9616394,  15);
    GeoCoordinate coord3 = new GeoCoordinate(22.73195122, 113.97943554,  20);
    GeoCoordinate coord4 = new GeoCoordinate(22.7308599900000, 114.0127601700000,  25);
    GeoCoordinate coord5 = new GeoCoordinate(22.6952994700000, 114.0001401900000,  30);
    GeoCoordinate coord6 = new GeoCoordinate(22.7036674800000, 113.9547871500000,  35);


    List<GeoCoordinate> coordinates = new List<GeoCoordinate>();
    coordinates.Add(coord1);
    coordinates.Add(coord2);
    coordinates.Add(coord3);
    coordinates.Add(coord4);
    coordinates.Add(coord5);
    coordinates.Add(coord6);

    loadkml.Document.Add(new Placemark("", "", "colorID",coordinates));
    loadkml.SaveFileDialog();            
}  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Device.Location;
using System.Windows.Forms;

namespace Mmc.Mspace.RoutePlanning
{
    
    public class Placemark //Line
    {
        public class KMLPoint
        {
            private string _coordinateList;

            public KMLPoint()
            {

            }

            public KMLPoint(List<GeoCoordinate> coordList)
            {
                SetCoordinates(coordList);
            }            

            public void SetCoordinates(List<GeoCoordinate> coordList)
            {
                StringBuilder points = new StringBuilder();

                for (int i = 0; i < coordList.Count(); i++)
                {
                    string coordStr =  coordList[i].Longitude.ToString() + "," + coordList[i].Latitude.ToString() + "," + coordList[i].Altitude.ToString() + " ";
                    points.Append(coordStr);
                }
                _coordinateList = points.ToString();

            }
            public int extrude
            {
                get { return 1; }
                set { }
            }

            public int tessellate
            {
                get { return 1; }
                set { }
            }

            public string altitudeMode
            {
                get { return "absolute"; }
                set { }
            }

            public string coordinates
            {
                get
                {
                    return _coordinateList;
                }

                set
                {
                    _coordinateList = value;
                }
            }

        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("styleUrl")]
        public string StyleUrl { get; set; } 

        public KMLPoint LineString { get; set; }

        public Placemark()
        {

        }

        public Placemark(string name, string description, string styleUrl,
            List<GeoCoordinate> coordList)
        {
            Name = name;
            Description = description;
            StyleUrl = styleUrl;

            LineString = new KMLPoint(coordList);
        }

    }


    public class kml
    {
        [XmlIgnore]
        string Name { get; set; }
        string _fileNameExt { get; set; }

        List<Placemark> _Placemarks = new List<Placemark>();
        //List<PlacemarkLine> _PlacemarkLines = new List<PlacemarkLine>();

        [XmlArray()]
        // [!001]
        public List<Placemark> Document
        {
            get
            {
                return _Placemarks;
            }

            set
            {
                _Placemarks = value;
            }
        }

        public kml()
        {
        }

        public kml(string name)
        {
            Name = name;
        }

        private XmlNode GetColorStyle(XmlDocument xmlDoc, string color)
        {
            XmlNode style = xmlDoc.CreateNode(XmlNodeType.Element, "Style", "");
            XmlAttribute attr = style.OwnerDocument.CreateAttribute("id");
            attr.Value = color;
            style.Attributes.Append(attr);

            XmlNode iconStyle = xmlDoc.CreateNode(XmlNodeType.Element, "IconStyle", "");
            XmlNode icon = xmlDoc.CreateNode(XmlNodeType.Element, "Icon", "");
            XmlNode href = xmlDoc.CreateNode(XmlNodeType.Element, "href", "");
            href.InnerText = string.Format("http://www.google.com/intl/en_us/mapfiles/ms/icons/{0}-dot.png",
                color);
            style.AppendChild(iconStyle);
            iconStyle.AppendChild(icon);
            icon.AppendChild(href);

            return style;
        }
        public void SaveFileDialog()
        {
            string localFilePath, filepath;
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = " Save to KML Files(*.kml)|*.*";//*.kml|All files(*.*)|
            fileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".kml";
            fileDialog.FilterIndex = 2;
            fileDialog.AddExtension = true;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                localFilePath = fileDialog.FileName.ToString();
                _fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                filepath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                this.SaveToFile(localFilePath);
            }
        }

        //@"E:\abc.kml"
        public void SaveToFile(string xml)
        {
            using (FileStream fs = new FileStream(xml, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(sw, this);
                }
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xml);
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            XmlNode documentNode = xmlDoc.SelectSingleNode(@"/kml/Document");//[!001]

            XmlNode nameNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", "");
            //nameNode.InnerText = this.Name;
            nameNode.InnerText = this._fileNameExt;

            XmlNode placeMarkNode = documentNode.FirstChild;
            documentNode.InsertBefore(nameNode, placeMarkNode);

            documentNode.InsertBefore(GetColorStyle(xmlDoc, "red"), placeMarkNode);
            documentNode.InsertBefore(GetColorStyle(xmlDoc, "green"), placeMarkNode);
            documentNode.InsertBefore(GetColorStyle(xmlDoc, "blue"), placeMarkNode);
            documentNode.InsertBefore(GetColorStyle(xmlDoc, "yellow"), placeMarkNode);

            documentNode.InsertBefore(GetColorStyle(xmlDoc, "yellow"), placeMarkNode);


            XmlNode kmlNode = xmlDoc.SelectSingleNode(@"/kml");

            XmlAttribute attr = kmlNode.OwnerDocument.CreateAttribute("xmlns");

            attr.Value = "http://earth.google.com/kml/2.0";

            kmlNode.Attributes.Append(attr);

            xmlDoc.Save(xml);
        }

    }

}
