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
    [MessageContract(WrapperName = "GetNetworkDescriptionResponse", WrapperNamespace = "http://www.esri.com/schemas/ArcGIS/10.1", IsWrapped = true)]
    public class GetNetworkDescriptionResponse
    {
        public GetNetworkDescriptionResponse()
        {
        }

        public GetNetworkDescriptionResponse(NAServerNetworkDescription Result)
        {
            this.Result = Result;
        }

        [MessageBodyMember(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1", Order = 0)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public NAServerNetworkDescription Result;
    }
}