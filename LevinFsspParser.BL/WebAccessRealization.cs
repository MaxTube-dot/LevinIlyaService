using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LevinFsspParser.BL
{
    /// <summary>
    /// Класс реализующий GET и POST запросы.
    /// </summary>
    public class WebAccessRealization
    {
        /// <summary>
        /// GET - запрос
        /// </summary>
        /// <param name="url">Путь к методу.</param>
        /// <param name="parameters">Параметры запроса.</param>
        /// <returns></returns>
        public string Get(string url, NameValueCollection parameters)
        {
            using (WebClient client = new WebClient())
            {
                client.QueryString = parameters;

                string response = client.DownloadString(url);

                return response;
            }

        }

        /// <summary>
        /// POST запрос
        /// </summary>
        /// <param name="url">Путь к методу.</param>
        /// <param name="data">Данные</param>
        /// <returns></returns>
        public string Post(string url, string data)
        {
            using (WebClient client = new WebClient())
            {

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Headers["accept"] = "application/json";
                httpRequest.ContentType = "application/json";

  
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                HttpWebResponse httpResponse;

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
       

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }


            }
        }
    }
}
