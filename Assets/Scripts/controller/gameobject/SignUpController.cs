using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SignUpController : MonoBehaviour
{
    [SerializeField]
    private InputField usernameIF;

    [SerializeField]
    private InputField passwordIF;

    [SerializeField]
    private InputField nameIF;

    [SerializeField]
    private PageController pageController;

    [SerializeField]
    private PopupController popupController;

    private HttpController httpController = new HttpController();

    public void SignUpButtonClicked()
    {
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            popupController.ShowAlertPopup("아이디를 입력해 주세요.");
        }
        else if (string.IsNullOrEmpty(passwordIF.text))
        {
            popupController.ShowAlertPopup("비밀번호를 입력해 주세요.");
        }
        else if (string.IsNullOrEmpty(nameIF.text))
        {
            popupController.ShowAlertPopup("이름을 입력해 주세요.");
        }
        else
        {
            string res = httpController.post(
                AppInfo.serverDomain + "signup_post_json.php",
                new Dictionary<string, string>() {
                    { "username", usernameIF.text },
                    { "password", passwordIF.text },
                    { "name", nameIF.text },
                }
            );

            if(string.IsNullOrEmpty(res))
            {
                popupController.ShowAlertPopup("통신 중 오류가 발생하였습니다.");
            }
            else
            {
                Response response = JsonUtility.FromJson<Response>(res);
                
                if(response.success)
                {
                    pageController.ShowLoginPage();
                    popupController.ShowAlertPopup("정상적으로 가입 되었습니다!");
                }
                else
                {
                    popupController.ShowAlertPopup("통신 중 오류가 발생하였습니다.");
                }
            }
        }
    }

    public void BackButtonClicked()
    {
        pageController.ShowLoginPage();
    }
}