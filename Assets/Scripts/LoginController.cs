using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    public static bool process;

    [SerializeField]
    private InputField usernameIF;

    [SerializeField]
    private InputField passwordIF;

    [SerializeField]
    private PageController pageController;

    [SerializeField]
    private PopupController popupController;

    private HTTPPostSender httpPostSender;

    void Start()
    {
        process = false;
        httpPostSender = GetComponent<HTTPPostSender>();
    }

    void Update()
    {
        if (process)
        {
            if(LoginInfo.username == null)
            {
                popupController.ShowAlertPopup("로그인에 실패하였습니다.");
            }
            else
            {
                SceneManager.LoadScene("UserInfo");
            }

            process = false;
        }
    }

    public void LoginButtonClicked()
    {
        if (usernameIF.text == null || usernameIF.text == "")
        {
            popupController.ShowAlertPopup("아이디를 입력해 주세요.");
        }
        else if (passwordIF.text == null || passwordIF.text == "")
        {
            popupController.ShowAlertPopup("비밀번호를 입력해 주세요.");
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("username", usernameIF.text);
            form.AddField("password", passwordIF.text);
            httpPostSender.Send(URLs.login, form, PostType.Login);
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