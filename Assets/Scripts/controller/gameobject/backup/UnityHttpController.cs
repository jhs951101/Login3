using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityHttpController : MonoBehaviour
{
    private string result;
    private bool wait;

    public string sendGet(string originalUrl, Dictionary<string, string> parameters)
    {
        lock(typeof(UnityHttpController))
        {
            result = null;
            wait = true;

            StartCoroutine(get(originalUrl, parameters));

            while(wait){ Debug.Log(wait); }
            return result;
        }
    }

    public string sendPost(string originalUrl, Dictionary<string, string> parameters)
    {
        lock(typeof(UnityHttpController))
        {
            result = null;
            wait = true;

            StartCoroutine(post(originalUrl, parameters));

            while(wait){ Debug.Log(wait); }
            return result;
        }
    }

    private IEnumerator get(string originalUrl, Dictionary<string, string> parameters)
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
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (!request.isNetworkError && !request.isHttpError)
            {
                result = request.downloadHandler.text;
            }

            wait = false;
        }
    }

    private IEnumerator post(string originalUrl, Dictionary<string, string> parameters)
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
        
        using (UnityWebRequest request = UnityWebRequest.Post(originalUrl, data))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && !request.isHttpError)
            {
                result = request.downloadHandler.text;
            }

            wait = false;
        }
    }
}