using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CurrenciesRates.Core.CurrencyRate
{
    public interface INBPService
    {
        public string Api_Url { get; }
        string CreateWebRequest(string url, string method = "GET");
        string GetResponseStream(WebRequest webRequest);
    }

   public class NBPService : INBPService
    {
        public string Api_Url { get; } = "https://api.nbp.pl/api/exchangerates/";
        WebRequest webRequest;

        public string CreateWebRequest(string url, string method = "GET")
        {
            this.webRequest = WebRequest.Create(url);
            this.webRequest.Method = method;

            var result = GetResponseStream(this.webRequest);

            return result;
        }
        public string GetResponseStream(WebRequest webRequest)
        {
            HttpWebResponse httpWebResponse = null;
            httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            string strResult;
            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResult = sr.ReadToEnd();
                sr.Close();
            }

            return strResult;
        }
    }
}
