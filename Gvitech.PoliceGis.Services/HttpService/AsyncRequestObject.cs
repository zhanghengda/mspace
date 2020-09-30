namespace Mmc.Mspace.Services.HttpService
{
    public class AsyncRequestObject
    {
        public string RequesUrl { get; private set; }

        public string PostDataStr { get; private set; }

        public AsyncRequestObject(string url, string data)
        {
            this.RequesUrl = url;
            this.PostDataStr = data;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.RequesUrl);
        }
    }
}