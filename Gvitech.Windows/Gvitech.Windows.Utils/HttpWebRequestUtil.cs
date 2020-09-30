using Mmc.Windows.Services;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Mmc.Windows.Utils
{
	public class HttpWebRequestUtil
	{
		private static volatile HttpWebRequestUtil _instance;

		private static readonly object SyncRoot = new object();

		private readonly CookieContainer _cookie;

		public static HttpWebRequestUtil Instance
		{
			get
			{
				bool flag = HttpWebRequestUtil._instance == null;
				if (flag)
				{
					object syncRoot = HttpWebRequestUtil.SyncRoot;
					lock (syncRoot)
					{
						bool flag3 = HttpWebRequestUtil._instance == null;
						if (flag3)
						{
							HttpWebRequestUtil._instance = new HttpWebRequestUtil();
						}
					}
				}
				return HttpWebRequestUtil._instance;
			}
		}

		public int TimeOut
		{
			get;
			set;
		}

		private HttpWebRequestUtil()
		{
			this.TimeOut = 30000;
			this._cookie = new CookieContainer();
		}

		public StreamReader GetStreamReaderFromUrl(string url)
		{
			return this.GetStreamReaderFromUrl(url, HttpRequestMethod.GET, null);
		}

		public StreamReader GetStreamReaderFromUrl(string url, int timeOut)
		{
			return this.GetStreamReaderFromUrl(url, timeOut, HttpRequestMethod.POST, null);
		}

		public StreamReader GetStreamReaderFromUrl(string url, int timeOut, NameValueCollection parameters, string fileName)
		{
			Stream fileStream = null;
			bool flag = File.Exists(fileName);
			if (flag)
			{
				try
				{
					fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				}
				catch (Exception ex)
				{
					SystemLog.Log(string.Format("读取{0}失败,原因:{1}", fileName, ex.Message), LogMessageType.INFO);
				}
			}
			fileName = fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
			return this.GetStreamReaderFromUrl(url, timeOut, parameters, fileStream, fileName);
		}

		public StreamReader GetStreamReaderFromUrl(string url, int timeout, NameValueCollection parameters, Stream fileStream, string fileName)
		{
			StreamReader result = null;
			try
			{
				string str;
				byte[] uploadBytes = this.GetUploadBytes(parameters, fileStream, fileName, out str);
				HttpWebRequest httpWebRequest = this.CreateRequest(url, this.TimeOut);
				httpWebRequest.ContentType = "\r\nmultipart/form-data;charset=UTF-8; boundary=" + str;
				httpWebRequest.Method = "Post";
				httpWebRequest.KeepAlive = true;
				httpWebRequest.ContentLength = (long)uploadBytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(uploadBytes, 0, uploadBytes.Length);
				requestStream.Close();
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				bool flag = httpWebResponse.StatusCode == HttpStatusCode.OK;
				if (flag)
				{
					Stream responseStream = httpWebResponse.GetResponseStream();
					bool flag2 = responseStream != null;
					if (flag2)
					{
						result = new StreamReader(responseStream, Encoding.UTF8);
					}
				}
			}
			catch (Exception ex)
			{
				SystemLog.Log(string.Format("读取{0}失败,原因:{1}", url, ex.Message), LogMessageType.INFO);
			}
			finally
			{
				bool flag3 = fileStream != null;
				if (flag3)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		public StreamReader GetStreamReaderFromUrl(string url, int timeout, NameValueCollection parameters, Stream fileStream)
		{
			return this.GetStreamReaderFromUrl(url, this.TimeOut, parameters, fileStream, "unknownfile");
		}

		public StreamReader GetStreamReaderFromUrl(string url, HttpRequestMethod httpRequestMethod, NameValueCollection parameters)
		{
			return this.GetStreamReaderFromUrl(url, this.TimeOut, httpRequestMethod, parameters);
		}

		public StreamReader GetStreamReaderFromUrl(string url, int timeout, HttpRequestMethod httpRequestMethod, NameValueCollection parameters)
		{
			StreamReader streamReader = null;
			Stream streamFromUrl = this.GetStreamFromUrl(url, this.TimeOut, httpRequestMethod, parameters);
			bool flag = streamFromUrl == null;
			StreamReader result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					streamReader = new StreamReader(streamFromUrl, Encoding.UTF8);
				}
				catch (Exception ex)
				{
					SystemLog.Log(string.Format("获取{0}失败,原因:{1}", url, ex.Message), LogMessageType.INFO);
				}
				result = streamReader;
			}
			return result;
		}

		public Stream GetStreamFromUrl(string url, int timeout, HttpRequestMethod httpRequestMethod, NameValueCollection parameters)
		{
			Stream result = null;
			try
			{
				bool flag = httpRequestMethod == HttpRequestMethod.POST;
				HttpWebRequest httpWebRequest;
				if (flag)
				{
					httpWebRequest = this.CreateRequest(url, timeout);
					httpWebRequest.Method = "POST";
					httpWebRequest.KeepAlive = true;
					httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
					byte[] array = this.EncodePars(parameters);
					httpWebRequest.ContentLength = (long)array.Length;
					Stream requestStream = httpWebRequest.GetRequestStream();
					requestStream.Write(array, 0, array.Length);
					requestStream.Close();
				}
				else
				{
					bool flag2 = url.IndexOf("?", StringComparison.Ordinal) > -1;
					if (flag2)
					{
						url = url + "&" + this.ParsToString(parameters);
					}
					else
					{
						url = url + "?" + this.ParsToString(parameters);
					}
					httpWebRequest = this.CreateRequest(url, timeout);
					httpWebRequest.Method = "GET";
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				bool flag3 = httpWebResponse.StatusCode == HttpStatusCode.OK;
				if (flag3)
				{
					result = httpWebResponse.GetResponseStream();
				}
			}
			catch (WebException ex)
			{
				SystemLog.Log(string.Format("获取{0}失败,原因:{1}", url, ex.Message), LogMessageType.INFO);
			}
			catch (Exception ex2)
			{
				SystemLog.Log(string.Format("获取{0}失败,原因:{1}", url, ex2.Message), LogMessageType.INFO);
			}
			return result;
		}

		private HttpWebRequest CreateRequest(string url)
		{
			return this.CreateRequest(url, this.TimeOut);
		}

		private HttpWebRequest CreateRequest(string url, int timeOut)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Timeout = timeOut;
			httpWebRequest.CookieContainer = this._cookie;
			return httpWebRequest;
		}

		private byte[] EncodePars(NameValueCollection parameters)
		{
			return Encoding.UTF8.GetBytes(this.ParsToString(parameters));
		}

		private string ParsToString(NameValueCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = parameters != null;
			if (flag)
			{
				foreach (string text in parameters)
				{
					bool flag2 = stringBuilder.Length > 0;
					if (flag2)
					{
						stringBuilder.Append("&");
					}
					stringBuilder.Append(string.Format("{0}={1}", HttpUtility.UrlEncode(text), HttpUtility.UrlEncode((parameters[text] == null) ? "" : parameters[text])));
				}
			}
			return stringBuilder.ToString();
		}

		private byte[] GetUploadBytes(NameValueCollection parameters, Stream fileStream, string fileName, out string boundaryValue)
		{
			MemoryStream memoryStream = null;
			boundaryValue = DateTime.Now.Ticks.ToString("x");
			byte[] array;
			byte[] result;
			try
			{
				bool flag = string.IsNullOrEmpty(fileName);
				if (flag)
				{
					fileName = "unknownfile";
				}
				string text = "--" + boundaryValue;
				StringBuilder stringBuilder = new StringBuilder();
				bool flag2 = parameters != null;
				if (flag2)
				{
					foreach (string text2 in parameters)
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							text,
							"\r\nContent-Disposition: form-data; name=\"",
							text2,
							"\"\r\n\r\n",
							parameters[text2],
							"\r\n"
						}));
					}
				}
				stringBuilder.Append(text + "\r\nContent-Disposition: form-data; name=\"filedata\"; ");
				stringBuilder.Append("filename=\"");
				stringBuilder.Append(fileName);
				stringBuilder.Append("\"\r\nContent-Type: text/plain;charset=UTF-8\r\n\r\n");
				string s = stringBuilder.ToString();
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				byte[] bytes2 = Encoding.ASCII.GetBytes("\r\n" + text + "--\r\n");
				long num = (fileStream == null) ? 0L : fileStream.Length;
				long num2 = (long)bytes.Length;
				long num3 = num2 + num + (long)bytes2.Length;
				array = new byte[num3];
				bytes.CopyTo(array, 0);
				byte[] array2 = new byte[Math.Min(4096, (int)num)];
				bool flag3 = fileStream != null;
				if (flag3)
				{
					memoryStream = new MemoryStream();
					int count;
					while ((count = fileStream.Read(array2, 0, array2.Length)) != 0)
					{
						memoryStream.Write(array2, 0, count);
					}
					memoryStream.ToArray().CopyTo(array, num2);
				}
				bytes2.CopyTo(array, num2 + num);
			}
			catch (Exception ex)
			{
				SystemLog.Log(string.Format("转化byte失败,原因:{0}", ex.Message), LogMessageType.INFO);
				result = null;
				return result;
			}
			finally
			{
				bool flag4 = memoryStream != null;
				if (flag4)
				{
					memoryStream.Close();
				}
			}
			result = array;
			return result;
		}
	}
}
