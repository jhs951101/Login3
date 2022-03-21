using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HTTPPostSender : MonoBehaviour
{
    public void Send(string URL, WWWForm form, PostType postType)
    {
        StartCoroutine(Do(URL,form,postType));
    }

    IEnumerator Do(string URL, WWWForm form, PostType postType)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                UserResponse response = JsonUtility.FromJson<UserResponse>(request.downloadHandler.text);

                if (postType == PostType.Login)
                {
                    if (response.success)
                    {
                        LoginInfo.username = response.username;
                        LoginInfo.name = response.name;
                    }
                }
            }

            if (postType == PostType.Login)
                LoginController.process = true;
            if (postType == PostType.SignUp)
                SignUpController.process = true;
        }
    }
}