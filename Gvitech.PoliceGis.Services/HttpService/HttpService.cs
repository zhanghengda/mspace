using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpService : IHttpService
    {
        public string Token { get; set; }

        public string Version { get; set; }

        public HttpService()
        {
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            Version = json1.mspaceVersion;
        }
        /// <summary>
        /// 判断token状态
        /// </summary>
        /// <param name="res"></param>
        private void CheckTokenState(HttpWebResponse webResponse)
        {
            if (!string.IsNullOrEmpty(Token))
            {

                var erCodeHeards = webResponse.Headers["errCode"];
                if (string.IsNullOrEmpty(erCodeHeards))
                    return;
                int code = 0;
                code = int.Parse(erCodeHeards);
                if (code == 1002)
                    throw new LoginExcetiop("1002");
                else if (code == 1011)
                    throw new LoginExcetiop("1011");
                else if (code == 1004)
                    throw new LoginExcetiop("1004");
            }
        }
        public string HttpPost(string url, string postDataStr, int timeout = 10000)
        {
            //  SetToken(ref url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = timeout;
            SetToken(ref httpWebRequest);
            SetVersion(ref httpWebRequest);
            postDataStr = (string.IsNullOrEmpty(postDataStr) ? JsonConvert.SerializeObject(string.Empty) : postDataStr);
            bool flag = !string.IsNullOrEmpty(postDataStr);
            if (flag)
            {
                Stream requestStream = httpWebRequest.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
                streamWriter.Write(postDataStr);
                streamWriter.Close();
                requestStream.Close();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            httpWebResponse.Close();
            CheckTokenState(httpWebResponse);
            return result;
        }

        public string HttpRequest(string url, int timeout = 10000)
        {
            //SetToken(ref url);
            var result = string.Empty;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Timeout = timeout;
                SetToken(ref httpWebRequest);
                SetVersion(ref httpWebRequest);
                HttpWebResponse httpWebResponse = null;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                HttpStatusCode status = httpWebResponse.StatusCode;

                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                result = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                CheckTokenState(httpWebResponse);
                httpWebResponse.Close();
                return result;
            }

            catch (WebException WebEx)
            {
                Console.WriteLine(WebEx);
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = WebEx.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
                return result;
            }
            catch (AggregateException Agex)
            {
                Console.WriteLine(Agex);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return result;
            }
            finally {
                
            }
           
         
        }

        private void SetToken(ref string url)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                if (url.Contains("?"))
                    url = string.Format("{0}&token={1}", url, Token);
                else
                    url = string.Format("{0}?token={1}", url, Token);
            }
        }

        private void SetToken(ref HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(this.Token))
            {
                request.Headers.Add("token", this.Token);
            }
        }

        private void SetVersion(ref HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(this.Version))
            {
                request.Headers.Add("mspace-version", this.Version);
            }
        }

        public string HttpRequestAsync(string url, int timeout = 10000, string method = "GET")
        {
            //SetToken(ref url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = timeout;
            SetToken(ref httpWebRequest);
            SetVersion(ref httpWebRequest);
            try
            {
                var resposeSync = httpWebRequest.GetResponseAsync();
                //resposeSync = (HttpWebResponse)(await httpWebRequest.GetResponseAsync().ConfigureAwait(false));
                //HttpStatusCode status = resposeSync.AsyncState.;
                resposeSync.Wait();

                if (resposeSync.IsCompleted)
                {
                    var response = resposeSync.Result;
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    string result = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();
                    responseStream.Close();
                    responseStream.Dispose();
                    CheckTokenState((HttpWebResponse)response);
                    response.Close();
                    response.Dispose();
                    return result;
                }
            }
            catch (WebException WebEx)
            {
                Console.WriteLine(WebEx);
                Console.WriteLine(WebEx);
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = WebEx.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }

                if (WebEx?.HResult == -2146233088 || WebEx?.HResult == -2146233079)
                    throw WebEx;
            }
            catch (AggregateException Agex)
            {
                var innerEx = Agex.InnerException as WebException;
                if (innerEx?.HResult == -2146233088 || innerEx?.HResult == -2146233079)
                {
                    throw innerEx;
                }
                if (innerEx != null && innerEx.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = innerEx.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }


                var innerExs = Agex.InnerExceptions;
                foreach(var item in innerExs)
                {
                    var WebEx = item as WebException;
                    if (WebEx !=null&& WebEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = WebEx.Response as HttpWebResponse;
                        if (response != null)
                        {
                            Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                        }
                        else
                        {
                            // no http status code available
                        }
                    }
                    else
                    {
                        // no http status code available
                    }
                }

                Console.WriteLine(Agex);
            }
            catch (Exception ex)
            {
                if (ex?.HResult == -2146233088 || ex?.HResult == -2146233079)
                {
                    throw ex;
                }
                Console.WriteLine(ex);
            }

            return string.Empty;
        }

        public void AsyncHttpPost(string url, string postDataStr, Action<string> OnGetResponse)
        {
            // SetToken(ref url);
            Task task = Task.Factory.StartNew(delegate
            {
                Task<HttpWebRequest> task2 = this.RequestCallback(new AsyncRequestObject(url, postDataStr));
                task2.Start();
                task2.Wait();
                Task<string> task3 = this.ResponseCallback(task2.Result);
                task3.Start();
                task3.Wait();
                OnGetResponse(task3.Result);
            });
            // task.Start();
        }
        #region 方法已抽取，废弃
        public string PostFileData(string url, string filepath = "")
        {
            //  SetToken(ref url);
            //var strURL = string.Format("{0}/{1}", _poiUrl, methodName);

            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            SetToken(ref request);
            SetVersion(ref request);
            //创建一个内存流
            Stream memStream = new MemoryStream();
            string boundary = "---------------------------91811647521344";
            request.ContentType = " multipart/form-data; boundary=" + boundary;

            // 构造发送数据
            StringBuilder sb = new StringBuilder();
            // 文件域的数据
            sb.Append("-----------------------------91811647521344");
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"UploadForm[imageFile]\"; filename=\"shootTmp.jpg\"");
            //sb.Append("Content-Disposition: form-data; name=\"UploadForm[imageFile]\"; filename=\"\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append("image/png");
            sb.Append("\r\n\r\n");

            Console.WriteLine(sb);

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            //构造尾部数据
            StringBuilder wsb = new StringBuilder();
            wsb.Append("\r\n" + "-----------------------------91811647521344" + "\r\n");
            wsb.Append("Content-Disposition: form-data; name=\"toJSON\"" + "\r\n");
            wsb.Append("\r\n" + "[object HTMLInputElement]" + "\r\n");
            wsb.Append("-----------------------------91811647521344--");
            byte[] boundaryBytes = Encoding.UTF8.GetBytes(wsb.ToString());
            Console.WriteLine(wsb);
            FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            request.ContentLength = length;

            Stream requestStream = request.GetRequestStream();

            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            int index = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
                index++;
            }

            memStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            fileStream.Close();

            //将内存流数据读取位置归零
            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            //将内存流中的buffer写入到请求写入流
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            WebResponse responce = request.GetResponse();

            Stream s;
            s = responce.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.Default);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            CheckTokenState((HttpWebResponse)responce);
            return strValue;
        }

        public string PostFileData2(string url, string filepath = "")
        {
            //  SetToken(ref url);
            //var strURL = string.Format("{0}/{1}", _poiUrl, methodName);

            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            SetToken(ref request);
            SetVersion(ref request);
            //创建一个内存流
            Stream memStream = new MemoryStream();
            string boundary = "---------------------------91811647521344";
            request.ContentType = " multipart/form-data; boundary=" + boundary;

            // 构造发送数据
            StringBuilder sb = new StringBuilder();
            // 文件域的数据
            sb.Append("-----------------------------91811647521344");
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"UploadForm[imageFile]\"; filename=\"shootTmp.png\"");
            //sb.Append("Content-Disposition: form-data; name=\"UploadForm[imageFile]\"; filename=\"\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append("image/png");
            sb.Append("\r\n\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            //构造尾部数据
            StringBuilder wsb = new StringBuilder();
            wsb.Append("\r\n" + "-----------------------------91811647521344" + "\r\n");
            wsb.Append("Content-Disposition: form-data; name=\"toJSON\"" + "\r\n");
            wsb.Append("\r\n" + "[object HTMLInputElement]" + "\r\n");
            wsb.Append("-----------------------------91811647521344--");
            byte[] boundaryBytes = Encoding.UTF8.GetBytes(wsb.ToString());

            FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            request.ContentLength = length;

            Stream requestStream = request.GetRequestStream();

            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            int index = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
                index++;
            }

            memStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            fileStream.Close();

            //将内存流数据读取位置归零
            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            //将内存流中的buffer写入到请求写入流
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            WebResponse responce = request.GetResponse();

            Stream s;
            s = responce.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.Default);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            CheckTokenState((HttpWebResponse)responce);
            return strValue;
        }

        public string PostReportFileData(string url, string filepath = "")
        {
            //  SetToken(ref url);
            //var strURL = string.Format("{0}/{1}", _poiUrl, methodName);

            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            SetToken(ref request);
            SetVersion(ref request);
            //创建一个内存流
            Stream memStream = new MemoryStream();
            string boundary = "---------------------------91811647521344";
            request.ContentType = " multipart/form-data; boundary=" + boundary;

            // 构造发送数据
            StringBuilder sb = new StringBuilder();
            // 文件域的数据
            sb.Append("-----------------------------91811647521344");
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"UploadForm[file]\"; filename=\"test.doc\"");
            //sb.Append("Content-Disposition: form-data; name=\"UploadForm[imageFile]\"; filename=\"\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append("application/msword");
            sb.Append("\r\n\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            //构造尾部数据
            StringBuilder wsb = new StringBuilder();
            wsb.Append("\r\n" + "-----------------------------91811647521344" + "\r\n");
            wsb.Append("Content-Disposition: form-data; name=\"toJSON\"" + "\r\n");
            wsb.Append("\r\n" + "[object HTMLInputElement]" + "\r\n");
            wsb.Append("-----------------------------91811647521344--");
            byte[] boundaryBytes = Encoding.UTF8.GetBytes(wsb.ToString());

            FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            request.ContentLength = length;

            Stream requestStream = request.GetRequestStream();

            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            int index = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
                index++;
            }

            memStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            fileStream.Close();

            //将内存流数据读取位置归零
            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            //将内存流中的buffer写入到请求写入流
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            WebResponse responce = request.GetResponse();

            Stream s;
            s = responce.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.Default);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            CheckTokenState((HttpWebResponse)responce);
            return strValue;
        }
        #endregion
        public string PostJsonData(string url, string jsonData)
        {
            // SetToken(ref url);
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            SetToken(ref request);
            SetVersion(ref request);
            string paraUrlCoded = jsonData;
            byte[] payload;
            payload = Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer?.Write(payload, 0, payload.Length);
            writer?.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            s?.Close();
            Reader?.Close();
            response?.Close();
            CheckTokenState(response);
            return strValue;
        }

        private Task<HttpWebRequest> RequestCallback(AsyncRequestObject state)
        {
            return new Task<HttpWebRequest>(delegate
            {
                bool flag = state == null && !state.IsValid();
                HttpWebRequest result;
                if (flag)
                {
                    result = null;
                }
                else
                {
                    string requesUrl = state.RequesUrl;
                    string postDataStr = state.PostDataStr;
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requesUrl);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = (long)Encoding.UTF8.GetByteCount(postDataStr);
                    Task<Stream> requestStreamAsync = httpWebRequest.GetRequestStreamAsync();
                    requestStreamAsync.Wait();
                    bool isCompleted = requestStreamAsync.IsCompleted;
                    if (isCompleted)
                    {
                        Stream result2 = requestStreamAsync.Result;
                        StreamWriter streamWriter = new StreamWriter(result2, Encoding.GetEncoding("gb2312"));
                        streamWriter.Write(postDataStr);
                        streamWriter.Close();
                        result2.Close();
                    }
                    result = httpWebRequest;
                }
                return result;
            });
        }

        private Task<string> ResponseCallback(HttpWebRequest request)
        {
            return new Task<string>(delegate
            {
                bool flag = request == null;
                string result;
                if (flag)
                {
                    result = string.Empty;
                }
                else
                {
                    Task<WebResponse> responseAsync = request.GetResponseAsync();
                    if (responseAsync == null)
                        return string.Empty;
                    responseAsync.Wait();
                    string text = string.Empty;
                    bool isCompleted = responseAsync.IsCompleted;
                    if (isCompleted)
                    {
                        WebResponse result2 = responseAsync.Result;
                        Stream responseStream = result2.GetResponseStream();
                        StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                        text = streamReader.ReadToEnd();
                        streamReader.Close();
                        responseStream.Close();
                    }
                    result = text;
                }
                return result;
            });
        }




        private readonly string _boundary = "++++++++++++++++mmc_001_nextpart_let_the_sky_free_to_everyone++++++++++++++++";
        private readonly string _nextLine = "\r\n";
        private HttpWebRequest CreateRequestObj(string url, int timeout, string method)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = method;
            httpRequest.Timeout = timeout;
            
            if (!string.IsNullOrEmpty(this.Token)) httpRequest.Headers.Add("token", this.Token);
            if (!string.IsNullOrEmpty(this.Version)) httpRequest.Headers.Add("mspace-version", (url.Contains("http://mspace.mmcuav.cn/api/grid/getcount")  || url == "http://mspace.mmcuav.cn/api/grid/list" || url == "http://mspace.mmcuav.cn/api/dhcamera/list" || url == "http://mspace.mmcuav.cn/api/griddata/streetsign") ? "v1.6" : this.Version);

            return httpRequest;
        }


        private byte[] CreatePostFileFront(string contentType, string formFileType, string sampleType)
        {
            StringBuilder headerSb = new StringBuilder();
            headerSb.Append("--"+_boundary);
            headerSb.Append(_nextLine);
            headerSb.Append(string.Format("Content-Disposition: form-data; name=\"UploadForm[{0}]\"; filename=\"{1}\"", formFileType, sampleType));
            //headerSb.Append("Content-Disposition: form-data; name=\"UploadForm[imageFile]\"; filename=\"shootTmp.jpg\"");
            headerSb.Append(_nextLine);

            headerSb.Append("--"+_boundary);
            headerSb.Append(_nextLine);
            headerSb.Append(string.Format("Content-Type: {0}", contentType));
            headerSb.Append(_nextLine);
            headerSb.Append(_nextLine);
            Console.WriteLine(headerSb);

            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(headerSb.ToString());
            return postHeaderBytes;
        }

        private byte[] CreatePostFileTail()
        {
            StringBuilder tailSb = new StringBuilder();
            tailSb.Append(_nextLine);
            tailSb.Append("--"+_boundary);
            tailSb.Append(_nextLine);
            tailSb.Append("Content-Disposition: form-data; name=\"toJSON\"");
            tailSb.Append(_nextLine);

            tailSb.Append(_nextLine);
            tailSb.Append("[object HTMLInputElement]");
            tailSb.Append(_nextLine);
            tailSb.Append("--"+_boundary+ "--");
            Console.WriteLine(tailSb);

            byte[] postTailBytes = Encoding.UTF8.GetBytes(tailSb.ToString());
            return postTailBytes;
        }

        public string RequestService(string url, string postDataStr = "", int timeout = 10000, string method = "GET")
        {
            string content = string.Empty;

            HttpWebRequest request = CreateRequestObj(url, timeout, method);
            request.ContentType = "application/json;charset=UTF-8";
            try
            {
                if (method == "POST" && !string.IsNullOrEmpty(postDataStr))
                {
                    Stream requestStream = request.GetRequestStream();
                    byte[] jsonbyte = Encoding.UTF8.GetBytes(postDataStr);
                    requestStream.Write(jsonbyte, 0, jsonbyte.Length);
                    requestStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        content = sr.ReadToEnd();
                        CheckTokenState(response);
                        return content;
                    }
                }

            }
            catch (WebException WebEx)
            {
                string statusCode = GetWebExceptionCode(WebEx);
                throw new HttpException(statusCode);
            }
        }


        public string RequestServiceAsync(string url, string postDataStr = "", int timeout = 20000, string method = "GET")
        {
            string content = string.Empty;

            HttpWebRequest request = CreateRequestObj(url, timeout, method);
            request.ContentType = "application/json;charset=UTF-8";
            try
            {
                if (method == "POST" && !string.IsNullOrEmpty(postDataStr))
                {
                    Stream requestStream = request.GetRequestStream();
                    //StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
                    byte[] jsonbyte = Encoding.UTF8.GetBytes(postDataStr);
                    requestStream.Write(jsonbyte, 0, jsonbyte.Length);
                    //streamWriter.Close();
                    requestStream.Close();
                }
                var responseSync = request.GetResponseAsync();
                responseSync.Wait();

                if (responseSync.IsCompleted)
                {
                    var webResponse = (HttpWebResponse)responseSync.Result;
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            content = sr.ReadToEnd();
                            CheckTokenState(webResponse);
                        }
                    }
                }
                return content;
            }
            catch (AggregateException Agex)
            {
                var WebEx = Agex.InnerException as WebException;
                string statusCode = GetWebExceptionCode(WebEx);
                throw new HttpException(statusCode);
            }
        }

        public string PostFileService(string url, string contentType, string formFileType, string sampleType, string filePath, int timeout = 200000, string method = "POST")
        {
            string content = string.Empty;

            HttpWebRequest request = CreateRequestObj(url, timeout, method);
            request.ContentType = "multipart/form-data; boundary=" + _boundary;
            try
            {
                if (method == "POST" && File.Exists(filePath))
                {
                    //创建一个内存流
                    Stream memStream = new MemoryStream();

                    byte[] postHeaderBytes = CreatePostFileFront(contentType, formFileType, sampleType);

                    byte[] boundaryBytes = CreatePostFileTail();

                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
                    request.ContentLength = length;

                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                    byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
                    int bytesRead = 0;
                    int index = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                        index++;
                    }

                    memStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    fileStream.Close();

                    //将内存流数据读取位置归零
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    //将内存流中的buffer写入到请求写入流
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();

                    WebResponse response = request.GetResponse();

                    Stream responseStream = response.GetResponseStream();
                    string StrDate = "";
                    StreamReader Reader = new StreamReader(responseStream, Encoding.UTF8);
                    while ((StrDate = Reader.ReadLine()) != null)
                    {
                        content += StrDate + _nextLine;
                    }
                    CheckTokenState((HttpWebResponse)response);
                }

                return content;
            }
            catch (WebException WebEx)
            {
                string statusCode = GetWebExceptionCode(WebEx);
                throw new HttpException(statusCode);
            }
        }

        /// <summary>
        /// 以断点续传方式下载文件
        /// </summary>
        /// <param name="url">文件下载地址</param>
        /// <param name="filePath">下载文件的保存路径</param>
        /// <param name="OnFinish">回调函数</param>
        public void DownloadFile(string url, string filePath,  Action<bool> OnFinish)
        {
            //打开上次下载的文件或新建文件
            long SPosition = 0;
            FileStream FStream;
            if (File.Exists(filePath))
            {
                FStream = File.OpenWrite(filePath);
                SPosition = FStream.Length;
                FStream.Seek(SPosition, SeekOrigin.Current); //移动文件流中的当前指针
            }
            else
            {
                FStream = new FileStream(filePath, FileMode.Create);
                SPosition = 0;
            }
            //打开网络连接
            try
            {
                HttpWebRequest myRequest = CreateRequestObj(url,200000,"GET");
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);//设置Range值
                //向服务器请求，获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                FStream.Close();
                myStream.Close();
                OnFinish(true);
            }
            catch(WebException WebEx)
            {
                FStream.Close();
                OnFinish(false);
                string statusCode = GetWebExceptionCode(WebEx);
                throw new HttpException(statusCode);
            }
        }
        public void DownloadFileWithVersion(string url, string filePath, string version, Action<bool> OnFinish)
        {
            //打开上次下载的文件或新建文件
            long SPosition = 0;
            FileStream FStream;
            if (File.Exists(filePath))
            {
                FStream = File.OpenWrite(filePath);
                SPosition = FStream.Length;
                FStream.Seek(SPosition, SeekOrigin.Current); //移动文件流中的当前指针
            }
            else
            {
                FStream = new FileStream(filePath, FileMode.Create);
                SPosition = 0;
            }
            //打开网络连接
            try
            {
                HttpWebRequest myRequest = CreateRequestObj(url, 200000, "GET");
                SetHeaderValue(myRequest.Headers, "mspace-version", version);
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);//设置Range值
                //向服务器请求，获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                FStream.Close();
                myStream.Close();
                OnFinish(true);
            }
            catch (WebException WebEx)
            {
                FStream.Close();
                OnFinish(false);
                string statusCode = GetWebExceptionCode(WebEx);
                throw new HttpException(statusCode);
            }
        }
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)//请求报头设置
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
        private string GetWebExceptionCode(WebException WebEx)
        {
            string statusCode = string.Empty;
            if (WebEx != null)
            {
                if (WebEx.Status == WebExceptionStatus.Timeout)
                {
                    statusCode = WebEx.Status.ToString();
                }
                else if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    var WeResponse = WebEx.Response as HttpWebResponse;
                    if (WeResponse != null)
                    {
                        statusCode = ((int)WeResponse.StatusCode).ToString();
                    }
                    else
                    {
                        statusCode = "-1";
                    }
                }
                else
                {
                    statusCode = "-1";
                }
            }
            else
            {
                statusCode = "-1";
            }
            return statusCode;
        }
    }
}