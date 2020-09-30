using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class NAServerPortClient : ClientBase<NAServerPort>, NAServerPort
    {
        public NAServerPortClient()
        {
        }

        public NAServerPortClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public NAServerPortClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public NAServerPortClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public NAServerPortClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetNALayerNamesResponse NAServerPort.GetNALayerNames(GetNALayerNamesRequest request)
        {
            return base.Channel.GetNALayerNames(request);
        }

        public string[] GetNALayerNames(esriNAServerLayerType LayerType)
        {
            GetNALayerNamesResponse nalayerNames = ((NAServerPort)this).GetNALayerNames(new GetNALayerNamesRequest
            {
                LayerType = LayerType
            });
            return nalayerNames.Result;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        Task<GetNALayerNamesResponse> NAServerPort.GetNALayerNamesAsync(GetNALayerNamesRequest request)
        {
            return base.Channel.GetNALayerNamesAsync(request);
        }

        public Task<GetNALayerNamesResponse> GetNALayerNamesAsync(esriNAServerLayerType LayerType)
        {
            return ((NAServerPort)this).GetNALayerNamesAsync(new GetNALayerNamesRequest
            {
                LayerType = LayerType
            });
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetSolverParametersResponse NAServerPort.GetSolverParameters(GetSolverParametersRequest request)
        {
            return base.Channel.GetSolverParameters(request);
        }

        public NAServerSolverParams GetSolverParameters(string NALayerName)
        {
            GetSolverParametersResponse solverParameters = ((NAServerPort)this).GetSolverParameters(new GetSolverParametersRequest
            {
                NALayerName = NALayerName
            });
            return solverParameters.Result;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        Task<GetSolverParametersResponse> NAServerPort.GetSolverParametersAsync(GetSolverParametersRequest request)
        {
            return base.Channel.GetSolverParametersAsync(request);
        }

        public Task<GetSolverParametersResponse> GetSolverParametersAsync(string NALayerName)
        {
            return ((NAServerPort)this).GetSolverParametersAsync(new GetSolverParametersRequest
            {
                NALayerName = NALayerName
            });
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetSolverParameters2Response NAServerPort.GetSolverParameters2(GetSolverParameters2Request request)
        {
            return base.Channel.GetSolverParameters2(request);
        }

        public NAServerSolverParams GetSolverParameters2(string NALayerName, string LayerToken, bool PopulateNAClasses)
        {
            GetSolverParameters2Response solverParameters = ((NAServerPort)this).GetSolverParameters2(new GetSolverParameters2Request
            {
                NALayerName = NALayerName,
                LayerToken = LayerToken,
                PopulateNAClasses = PopulateNAClasses
            });
            return solverParameters.Result;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        Task<GetSolverParameters2Response> NAServerPort.GetSolverParameters2Async(GetSolverParameters2Request request)
        {
            return base.Channel.GetSolverParameters2Async(request);
        }

        public Task<GetSolverParameters2Response> GetSolverParameters2Async(string NALayerName, string LayerToken, bool PopulateNAClasses)
        {
            return ((NAServerPort)this).GetSolverParameters2Async(new GetSolverParameters2Request
            {
                NALayerName = NALayerName,
                LayerToken = LayerToken,
                PopulateNAClasses = PopulateNAClasses
            });
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        GetNetworkDescriptionResponse NAServerPort.GetNetworkDescription(GetNetworkDescriptionRequest request)
        {
            return base.Channel.GetNetworkDescription(request);
        }

        public NAServerNetworkDescription GetNetworkDescription(string NALayerName)
        {
            GetNetworkDescriptionResponse networkDescription = ((NAServerPort)this).GetNetworkDescription(new GetNetworkDescriptionRequest
            {
                NALayerName = NALayerName
            });
            return networkDescription.Result;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        Task<GetNetworkDescriptionResponse> NAServerPort.GetNetworkDescriptionAsync(GetNetworkDescriptionRequest request)
        {
            return base.Channel.GetNetworkDescriptionAsync(request);
        }

        public Task<GetNetworkDescriptionResponse> GetNetworkDescriptionAsync(string NALayerName)
        {
            return ((NAServerPort)this).GetNetworkDescriptionAsync(new GetNetworkDescriptionRequest
            {
                NALayerName = NALayerName
            });
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        SolveResponse NAServerPort.Solve(SolveRequest request)
        {
            return base.Channel.Solve(request);
        }

        public NAServerSolverResults Solve(NAServerSolverParams SolverParams)
        {
            SolveResponse solveResponse = ((NAServerPort)this).Solve(new SolveRequest
            {
                SolverParams = SolverParams
            });
            return solveResponse.Result;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        Task<SolveResponse> NAServerPort.SolveAsync(SolveRequest request)
        {
            return base.Channel.SolveAsync(request);
        }

        public Task<SolveResponse> SolveAsync(NAServerSolverParams SolverParams)
        {
            return ((NAServerPort)this).SolveAsync(new SolveRequest
            {
                SolverParams = SolverParams
            });
        }
    }
}