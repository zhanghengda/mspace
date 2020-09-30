using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1", ConfigurationName = "Mmc.Mspace.Services.NetRouteAnalysisService.NAServerPort")]
    public interface NAServerPort
    {
        [OperationContract(Action = "", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [return: MessageParameter(Name = "Result")]
        GetNALayerNamesResponse GetNALayerNames(GetNALayerNamesRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        Task<GetNALayerNamesResponse> GetNALayerNamesAsync(GetNALayerNamesRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(Patch))]
        [ServiceKnownType(typeof(MapTableDescription))]
        [ServiceKnownType(typeof(Element))]
        [ServiceKnownType(typeof(NAClassCandidateFieldMap[]))]
        [ServiceKnownType(typeof(NACandidateFieldMap[]))]
        [return: MessageParameter(Name = "Result")]
        GetSolverParametersResponse GetSolverParameters(GetSolverParametersRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        Task<GetSolverParametersResponse> GetSolverParametersAsync(GetSolverParametersRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(Patch))]
        [ServiceKnownType(typeof(MapTableDescription))]
        [ServiceKnownType(typeof(Element))]
        [ServiceKnownType(typeof(NAClassCandidateFieldMap[]))]
        [ServiceKnownType(typeof(NACandidateFieldMap[]))]
        [return: MessageParameter(Name = "Result")]
        GetSolverParameters2Response GetSolverParameters2(GetSolverParameters2Request request);

        [OperationContract(Action = "", ReplyAction = "*")]
        Task<GetSolverParameters2Response> GetSolverParameters2Async(GetSolverParameters2Request request);

        [OperationContract(Action = "", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(Patch))]
        [ServiceKnownType(typeof(MapTableDescription))]
        [ServiceKnownType(typeof(Element))]
        [ServiceKnownType(typeof(NAClassCandidateFieldMap[]))]
        [ServiceKnownType(typeof(NACandidateFieldMap[]))]
        [return: MessageParameter(Name = "Result")]
        GetNetworkDescriptionResponse GetNetworkDescription(GetNetworkDescriptionRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        Task<GetNetworkDescriptionResponse> GetNetworkDescriptionAsync(GetNetworkDescriptionRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(Patch))]
        [ServiceKnownType(typeof(MapTableDescription))]
        [ServiceKnownType(typeof(Element))]
        [ServiceKnownType(typeof(NAClassCandidateFieldMap[]))]
        [ServiceKnownType(typeof(NACandidateFieldMap[]))]
        [return: MessageParameter(Name = "Result")]
        SolveResponse Solve(SolveRequest request);

        [OperationContract(Action = "", ReplyAction = "*")]
        Task<SolveResponse> SolveAsync(SolveRequest request);
    }
}