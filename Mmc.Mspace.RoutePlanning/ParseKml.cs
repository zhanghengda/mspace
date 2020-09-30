using System;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Gvitech.CityMaker.FdeGeometry;
using Mmc.Framework.Services;
using System.Collections.Generic;

namespace Mmc.Mspace.RoutePlanning
{
    public class ParseKml
    {
        public List<RoutePoint> _routePtList;
        public ParseKml()
        {
            Console.WriteLine("ParseKml init");

        }

        public IPolyline readXmlFile()
        {
            IPolyline polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Please Select The Kml File";
            dialog.Filter = "KML File|*.kml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file);

                XmlElement root = xmlDoc.DocumentElement;
                XmlNode _document = root.GetElementsByTagName("Document")[0];
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);

                if (_document == null || _document.Attributes["xmlns"] == null)
                {
                    nsmgr.AddNamespace("ns", root.Attributes["xmlns"].Value);
                }
                else
                {
                    nsmgr.AddNamespace("ns", _document.Attributes["xmlns"].Value);
                }

                XmlNodeList xmlmark = root.GetElementsByTagName("Placemark");
                for (int m = 0; m < xmlmark.Count; m++)
                {
                    XmlNodeList xmlmarkChilds = xmlmark[m].ChildNodes;
                    for (int n = 0; n < xmlmarkChilds.Count; n++)
                    {
                        XmlNode node = xmlmarkChilds[n];
                        if (node.Name == "LineString" || node.Name == "LineRing")
                        {
                            XmlNode coordsNode = node.FirstChild;
                            while (coordsNode != null && coordsNode.Name != "coordinates")
                            {
                                coordsNode = coordsNode.NextSibling;
                            }
                            if (coordsNode == null)
                                continue;
                            // 用正则表达式去除字符串首位的制表符、换行符等符号，然后用' '来划分为string[]
                            string tt = coordsNode.InnerText;
                            Regex reg = new Regex("\f|\n|\r|\t");
                            string modified = reg.Replace(tt, "");
                            string[] ss = modified.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//清除空格
                            _routePtList = new List<RoutePoint>();
                            for (int cc = 0; cc < ss.Length; cc++)
                            {           
                                //取点生成polyline
                                IPoint tempPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                string[] aa = ss[cc].Split(',');
                                if (aa.Length >= 3)
                                    tempPoint.SetPostion(Convert.ToDouble(aa[0]), Convert.ToDouble(aa[1]), Convert.ToDouble(aa[2]));
                                else if (aa.Length == 2)
                                    tempPoint.SetPostion(Convert.ToDouble(aa[0]), Convert.ToDouble(aa[1]),0);
                                RoutePoint routePt = new RoutePoint(Convert.ToDouble(aa[0]), Convert.ToDouble(aa[1]), Convert.ToDouble(aa[2]), 15, 2, 0, 0);
                                _routePtList.Add(routePt);
                                polyline.AppendPoint(tempPoint);
                            }
                        }
                    }
                }
                Console.WriteLine("读取成功！");               
            }

            return polyline;
        }
    }
}
