using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class LoginController : MonoBehaviour
{
    [SerializeField]
    private InputField usernameIF;

    [SerializeField]
    private InputField passwordIF;

    [SerializeField]
    private PageController pageController;

    [SerializeField]
    private PopupController popupController;

    private HttpController httpController = new HttpController();

    public void LoginButtonClicked()
    {
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            popupController.ShowAlertPopup("아이디를 입력해 주세요.");
        }
        else if (string.IsNullOrEmpty(passwordIF.text))
        {
            popupController.ShowAlertPopup("비밀번호를 입력해 주세요.");
        }
        else
        {
            string res = httpController.post(
                AppInfo.serverDomain + "signin_post_json.php",
                new Dictionary<string, string>() {
                    { "username", usernameIF.text },
                    { "password", passwordIF.text },
                }
            );

            if(string.IsNullOrEmpty(res))
            {
                popupController.ShowAlertPopup("통신 중 오류가 발생하였습니다.");
            }
            else
            {
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(res);
                
                if(response.success)
                {
                    LoginInfo.username = usernameIF.text;
                    LoginInfo.name = response.name;
                    SceneManager.LoadScene("UserInfo");
                }
                else
                {
                    popupController.ShowAlertPopup("아이디 또는 비밀번호가 일치하지 않습니다.");
                }
            }
        }
    }

    public void GoToSignUpButtonClicked()
    {
        pageController.ShowSignUpPage();
    }

    public void BackButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}