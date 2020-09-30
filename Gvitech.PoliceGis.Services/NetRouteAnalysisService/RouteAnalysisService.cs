using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    public class RouteAnalysisService : Singleton<RouteAnalysisService>, IRouteAnalysisService
    {
        public RouteAnalysisService()
        {
            this.ConnectToWebService();
        }

        public IRenderPolyline RouteAnalysis(List<IVector3> points, IObjectManager om, ISpatialCRS crs)
        {
            bool flag = !IEnumerableExtension.HasValues<IVector3>(points);
            if (flag)
            {
                throw new ArgumentNullException("points");
            }
            bool flag2 = om == null;
            if (flag2)
            {
                throw new ArgumentNullException("om");
            }
            bool flag3 = crs == null;
            if (flag3)
            {
                crs = GviMap.SpatialCrs;
            }
            return Singleton<HttpRouteAnalysisService>.Instance.RouteAnalysis(points, om, crs);
        }

        public static IRouteAnalysisService GetDefault(object args = null)
        {
            return Singleton<RouteAnalysisService>.Instance;
        }

        private void ConnectToWebService()
        {
            this.roadNAServer = null;
        }

        private void SetNASolverSettings(NAServerSolverParams solverSettings)
        {
        }

        private void SetSolverSpecificInterface(NAServerSolverParams solverParams)
        {
            NAServerRouteParams naserverRouteParams = solverParams as NAServerRouteParams;
            bool flag = naserverRouteParams != null;
            if (flag)
            {
                naserverRouteParams.OutputSpatialReference = new ProjectedCoordinateSystem
                {
                    WKID = 2385
                };
                naserverRouteParams.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;
                naserverRouteParams.UseStartTime = false;
                bool useStartTime = naserverRouteParams.UseStartTime;
                if (useStartTime)
                {
                    naserverRouteParams.StartTime = Convert.ToDateTime(DateTime.Now.ToString());
                }
            }
        }

        private void SetServerSolverParams(NAServerSolverParams solverParams)
        {
            solverParams.ReturnMap = false;
            solverParams.SnapTolerance = 50.0;
            solverParams.MaxSnapTolerance = 500.0;
            solverParams.SnapToleranceUnits = esriUnits.esriMeters;
            NAServerRouteParams naserverRouteParams = solverParams as NAServerRouteParams;
            bool flag = naserverRouteParams != null;
            if (flag)
            {
                naserverRouteParams.ReturnRouteGeometries = true;
                naserverRouteParams.ReturnRoutes = true;
                naserverRouteParams.ReturnStops = true;
                naserverRouteParams.ReturnBarriers = false;
                naserverRouteParams.ReturnDirections = false;
                naserverRouteParams.DirectionsLengthUnits = this.GetstringToesriUnits("米");
            }
        }

        private esriNetworkAttributeUnits GetstringToesriUnits(string stresriUnits)
        {
            //string text = stresriUnits.ToLower();
            //uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
            //if (num <= 2256549424u)
            //{
            //	if (num <= 1467682869u)
            //	{
            //		if (num <= 75346828u)
            //		{
            //			if (num != 56299513u)
            //			{
            //				if (num == 75346828u)
            //				{
            //					if (text == "码")
            //					{
            //						return esriNetworkAttributeUnits.esriNAUYards;
            //					}
            //				}
            //			}
            //			else if (text == "分")
            //			{
            //				return esriNetworkAttributeUnits.esriNAUMinutes;
            //			}
            //		}
            //		else if (num != 842557787u)
            //		{
            //			if (num == 1467682869u)
            //			{
            //				if (text == "秒")
            //				{
            //					return esriNetworkAttributeUnits.esriNAUSeconds;
            //				}
            //			}
            //		}
            //		else if (text == "毫米")
            //		{
            //			return esriNetworkAttributeUnits.esriNAUMillimeters;
            //		}
            //	}
            //	else if (num <= 1620744800u)
            //	{
            //		if (num != 1544048658u)
            //		{
            //			if (num == 1620744800u)
            //			{
            //				if (text == "日")
            //				{
            //					return esriNetworkAttributeUnits.esriNAUDays;
            //				}
            //			}
            //		}
            //		else if (text == "英尺")
            //		{
            //			return esriNetworkAttributeUnits.esriNAUFeet;
            //		}
            //	}
            //	else if (num != 1939519561u)
            //	{
            //		if (num == 2256549424u)
            //		{
            //			if (text == "英里")
            //			{
            //				return esriNetworkAttributeUnits.esriNAUMiles;
            //			}
            //		}
            //	}
            //	else if (text == "时")
            //	{
            //		return esriNetworkAttributeUnits.esriNAUHours;
            //	}
            //}
            //else if (num <= 3330539388u)
            //{
            //	if (num <= 2739377369u)
            //	{
            //		if (num != 2584467372u)
            //		{
            //			if (num == 2739377369u)
            //			{
            //				if (text == "度")
            //				{
            //					return esriNetworkAttributeUnits.esriNAUDecimalDegrees;
            //				}
            //			}
            //		}
            //		else if (text == "英寸")
            //		{
            //			return esriNetworkAttributeUnits.esriNAUInches;
            //		}
            //	}
            //	else if (num != 3216173843u)
            //	{
            //		if (num == 3330539388u)
            //		{
            //			if (text == "厘米")
            //			{
            //				return esriNetworkAttributeUnits.esriNAUCentimeters;
            //			}
            //		}
            //	}
            //	else if (text == "千米")
            //	{
            //		return esriNetworkAttributeUnits.esriNAUKilometers;
            //	}
            //}
            //else if (num <= 3494511758u)
            //{
            //	if (num != 3489675016u)
            //	{
            //		if (num == 3494511758u)
            //		{
            //			if (text == "海里")
            //			{
            //				return esriNetworkAttributeUnits.esriNAUNauticalMiles;
            //			}
            //		}
            //	}
            //	else if (text == "未知")
            //	{
            //		return esriNetworkAttributeUnits.esriNAUUnknown;
            //	}
            //}
            //else if (num != 3539395134u)
            //{
            //	if (num == 4135014786u)
            //	{
            //		if (text == "米")
            //		{
            //			return esriNetworkAttributeUnits.esriNAUMeters;
            //		}
            //	}
            //}
            //else if (text == "分米")
            //{
            //	return esriNetworkAttributeUnits.esriNAUDecimeters;
            //}
            return esriNetworkAttributeUnits.esriNAUUnknown;
        }

        private bool LoadLocations(NAServerSolverParams solverParams, List<IVector3> points)
        {
            bool flag = !IEnumerableExtension.HasValues<IVector3>(points);
            if (flag)
            {
                throw new ArgumentNullException("points");
            }
            int count = points.Count;
            bool flag2 = count < 1;
            bool result;
            if (flag2)
            {
                result = false;
            }
            else
            {
                PropertySet[] array = new PropertySet[count];
                PropertySetProperty[] array2 = new PropertySetProperty[2];
                int num;
                for (int i = 0; i < count; i = num + 1)
                {
                    IVector3 vect = points[i];
                    array[i] = this.GetAddressPropertySet(vect);
                    num = i;
                }
                NAServerRouteParams naserverRouteParams = solverParams as NAServerRouteParams;
                naserverRouteParams.Stops = new NAServerPropertySets
                {
                    PropertySets = array
                };
                result = true;
            }
            return result;
        }

        private PropertySet GetAddressPropertySet(IVector3 vect)
        {
            bool flag = vect == null;
            PropertySet result;
            if (flag)
            {
                result = null;
            }
            else
            {
                PointN value = new PointN
                {
                    X = vect.X,
                    Y = vect.Y,
                    SpatialReference = new ProjectedCoordinateSystem
                    {
                        WKID = 2385
                    }
                };
                PropertySet propertySet = new PropertySet();
                PropertySetProperty[] propertyArray = new PropertySetProperty[2];
                propertySet.PropertyArray = propertyArray;
                propertySet.PropertyArray[0] = this.CreatePropertySetProperty("Shape", value);
                propertySet.PropertyArray[1] = this.CreatePropertySetProperty("Name", Guid.NewGuid().ToString());
                result = propertySet;
            }
            return result;
        }

        private PropertySetProperty CreatePropertySetProperty(string key, object value)
        {
            bool flag = string.IsNullOrEmpty(key);
            PropertySetProperty result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = new PropertySetProperty
                {
                    Key = key,
                    Value = value
                };
            }
            return result;
        }

        private PropertySet CreateLocationPropertySet(string name, string X, string Y, SpatialReference sr)
        {
            PropertySet propertySet = new PropertySet();
            SpatialReference spatialReference = new ProjectedCoordinateSystem();
            spatialReference.WKID = 2385;
            propertySet.PropertyArray = this.CreateLocationPropertyArray(name, X, Y, null);
            return propertySet;
        }

        private PropertySetProperty[] CreateLocationPropertyArray(string name, string X, string Y, SpatialReference sr)
        {
            bool flag = sr == null;
            PropertySetProperty[] array;
            if (flag)
            {
                array = new PropertySetProperty[3];
            }
            else
            {
                array = new PropertySetProperty[4];
            }
            array[0] = new PropertySetProperty();
            array[0].Key = "Name";
            array[0].Value = name;
            array[1] = new PropertySetProperty();
            array[1].Key = "X";
            array[1].Value = X;
            array[2] = new PropertySetProperty();
            array[2].Key = "Y";
            array[2].Value = Y;
            return array;
        }

        private void OutputResults(NAServerSolverParams solverParams, NAServerSolverResults solverResults)
        {
            string text = "";
            GPMessages solveMessages = solverResults.SolveMessages;
            GPMessage[] gpmessages = solveMessages.GPMessages1;
            bool flag = gpmessages != null;
            if (flag)
            {
                int num;
                for (int i = 0; i < gpmessages.GetLength(0); i = num + 1)
                {
                    GPMessage gpmessage = gpmessages[i];
                    text = text + "\n" + gpmessage.MessageDesc;
                    num = i;
                }
            }
            bool flag2 = text.Length > 0;
            if (flag2)
            {
                Console.WriteLine(text, "NAServer Route Results");
            }
            bool flag3 = solverParams is NAServerRouteParams && solverResults is NAServerRouteResults;
            if (flag3)
            {
                this.OutputRouteResults(solverParams as NAServerRouteParams, solverResults as NAServerRouteResults);
            }
        }

        private void OutputRouteResults(NAServerRouteParams solverParams, NAServerRouteResults solverResults)
        {
            bool returnDirections = solverParams.ReturnDirections;
            if (returnDirections)
            {
            }
            bool returnRouteGeometries = solverParams.ReturnRouteGeometries;
            if (returnRouteGeometries)
            {
            }
            bool returnRoutes = solverParams.ReturnRoutes;
            if (returnRoutes)
            {
            }
            bool returnStops = solverParams.ReturnStops;
            if (returnStops)
            {
            }
            bool returnBarriers = solverParams.ReturnBarriers;
            if (returnBarriers)
            {
            }
        }

        private NAServerPortClient roadNAServer;
    }
}