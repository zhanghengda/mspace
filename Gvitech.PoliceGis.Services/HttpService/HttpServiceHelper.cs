using Mmc.Mspace.Models.HttpResult;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpServiceHelper : Singleton<HttpServiceHelper>
    {
        private string _hostUrl;
        private string _token;
        private string _version;
        public HttpService HttpService;
        public HttpServiceHelper()
        {
            _hostUrl = WebConfig.MspaceHostUrl;
            _version = WebConfig.MspaceVersion;
            _token = HttpServiceUtil.Token;
            HttpService = new HttpService()
            {
                Version = _version,
                Token = _token
            };
        }

        public string CreateUrl(string dataInterface)
        {
            return string.Format("{0}{1}", _hostUrl, dataInterface);
        }

        public string GetRequest(string dataInterface)
        {
            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url);
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    data = JsonUtil.SerializeToString(resultModel.data);
                }
                return data;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetBoolRequest(string dataInterface)
        {
            bool status = false;
            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url);
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    status = true;
                }
                return status;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetRequestAsync(string dataInterface)
        {

            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestServiceAsync(url);
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    data = JsonUtil.SerializeToString(resultModel.data);
                }
                return data;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void GetRequestAsyncForDelegate(string dataInterface, Action<string> onFinish)
        {

            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            string result = string.Empty;
            try
            {
                await Task.Run(() =>
                {
                    result= HttpService.RequestServiceAsync(url);
                    HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                    if (resultModel?.status == "1")
                    {
                        data = JsonUtil.SerializeToString(resultModel.data);
                    }
                });

                onFinish(data);
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PostRequestForData(string dataInterface, string jsonStr)
        {
            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url, postDataStr: jsonStr, method: "POST");
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    data = JsonUtil.SerializeToString(resultModel.data);
                }
                return data;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResultModel PostRequestForResultModel(string dataInterface, string jsonStr)
        {
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url, postDataStr: jsonStr, method: "POST");
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                return resultModel;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PostRequestForMessage(string dataInterface, string jsonStr)
        {
            string message = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url, postDataStr: jsonStr, method: "POST");
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    message = JsonUtil.SerializeToString(resultModel.message);
                }
                return message;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PostRequestForStatus(string dataInterface, string jsonStr)
        {
            bool status = false;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url, postDataStr: jsonStr, method: "POST");
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    status = true;
                }
                return status;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetRequestForStatus(string dataInterface, string jsonStr)
        {
            bool status = false;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestService(url, jsonStr);
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    status = true;
                }

                return status;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PostRequestAsync(string dataInterface)
        {

            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.RequestServiceAsync(url, method: "POST");
                return result;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PostImageFileOfMark(string dataInterface, string filepath)
        {
            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                data = HttpService.PostFileService(url, "image/png", "imageFile", "sample.jpg", filepath);

                return data;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string PostImageFile(string dataInterface, string filepath)
        {
            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.PostFileService(url, "image/png", "imageFile", "sample.jpg", filepath);

                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    data = JsonUtil.SerializeToString(resultModel.data);
                }
                return data;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string PostDocFile(string dataInterface, string filepath)
        {
            string data = string.Empty;
            string url = CreateUrl(dataInterface);
            try
            {
                string result = HttpService.PostFileService(url, "application/msword", "file", "sample.doc", filepath);
                HttpResultModel resultModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);
                if (resultModel?.status == "1")
                {
                    data = JsonUtil.SerializeToString(resultModel.data);
                }
                return data;
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DownloadFile(string dataInterface, string filePath,Action<bool> OnFinish)
        {
            string url = CreateUrl(dataInterface);
            try
            {
                HttpService.DownloadFile(url,  filePath, OnFinish);
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void DownloadWithVersion(string dataInterface, string filePath, Action<bool> OnFinish)
        {
            string url = CreateUrl(dataInterface);
            try
            {
                HttpService.DownloadFileWithVersion(url, filePath, "v1.5", OnFinish);
            }
            catch (HttpException ex)
            {
                throw new HttpException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
