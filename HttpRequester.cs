using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单
{
    class HttpRequester
    {
        public static void get(string url, AsyncCallback callback)
        {
            request("GET", "application/json", url, callback);

        }

        public static void post(string url, AsyncCallback callback)
        {
            request("POST", "application/json", url, callback);

        }

        public static void request(string method,string contentType, string url, AsyncCallback callback)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = method;
            request.ContentType = contentType;
            request.BeginGetResponse(callback, request);
        }
    }
}
