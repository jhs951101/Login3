using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

public class HttpController
{
    public string get(string originalUrl, Dictionary<string, string> parameters)
    {
        string responseText = null;

        try
        {
            string url = originalUrl;

            if (parameters != null)
            {
                bool first = true;
                Dictionary<string, string>.KeyCollection keys = parameters.Keys;

                foreach (string key in keys)
                {
                    char conn = '&';

                    if (first)
                    {
                        conn = '?';
                    }

                    url += (conn + key + "=" + parameters[key]);
                    first = false;
                }
            }
        
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 30 * 1000;

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                int status = (int)resp.StatusCode;

                if (status == 200)
                {
                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        responseText = sr.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception exception)
        {
        }

        return responseText;
    }

    public string post(string originalUrl, Dictionary<string, string> parameters)
    {
        string responseText = null;

        try
        {
            int i = 0;
            string data = "";
            data += "{";

            if (parameters != null)
            {
                Dictionary<string, string>.KeyCollection keys = parameters.Keys;

                foreach (string key in keys)
                {
                    data += string.Format(" \"{0}\" : \"{1}\" ", key, parameters[key]);

                    if (i != parameters.Count - 1)
                    {
                        data += ",";
                    }

                    i++;
                }
            }

            data += "}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(originalUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;

            byte[] bytes = UTF8Encoding.UTF8.GetBytes(data);
            request.ContentLength = bytes.Length;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bytes, 0, bytes.Length);
            }
            
            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                int status = (int)resp.StatusCode;

                if (status == 200)
                {
                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        responseText = sr.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception exception)
        {
        }

        return responseText;
    }
}