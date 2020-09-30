namespace Mmc.Mspace.Services.JscriptInvokeService
{
    public interface IWebView
    {
        IJsScriptInvokerService JsScriptInvoker { get; }

        void RequestUrl(string url);

        void InvokeScript(string methodName, params object[] obj);
        void InvokeScript(string methodName);
    }
}