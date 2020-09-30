using System;

namespace Mmc.Mspace.Services.HttpService
{
    public interface IHttpService
    {
        string Token { get; set; }

        string HttpPost(string url, string postDataStr, int timeout = 3000);

        void AsyncHttpPost(string url, string postDataStr, Action<string> OnGetResponse);



    }
}