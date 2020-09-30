using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "GetSolverParameters2", WrapperNamespace = "http://www.esri.com/schemas/ArcGIS/10.1", IsWrapped = true)]
    public class GetSolverParameters2Request
    {
        public GetSolverParameters2Request()
        {
        }

        public GetSolverParameters2Request(string NALayerName, string LayerToken, bool PopulateNAClasses)
        {
            this.NALayerName = NALayerName;
            this.LayerToken = LayerToken;
            this.PopulateNAClasses = PopulateNAClasses;
        }

        [MessageBodyMember(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1", Order = 0)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string NALayerName;

        [MessageBodyMember(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1", Order = 1)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string LayerToken;

        [MessageBodyMember(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1", Order = 2)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool PopulateNAClasses;
    }
}