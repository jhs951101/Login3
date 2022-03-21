using UnityEngine;
using UnityEngine.UI;

public class SignUpController : MonoBehaviour
{
    public static bool process;

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
            pageController.ShowLoginPage();
            popupController.ShowAlertPopup("정상적으로 가입 되었습니다!");

            process = false;
        }
    }

    public void SignUpButtonClicked()
    {
        if (usernameIF.text == null || usernameIF.text == "")
        {
            popupController.ShowAlertPopup("아이디를 입력해 주세요.");
        }
        else if (passwordIF.text == null || passwordIF.text == "")
        {
            popupController.ShowAlertPopup("비밀번호를 입력해 주세요.");
        }
        else if (nameIF.text == null || nameIF.text == "")
        {
            popupController.ShowAlertPopup("이름을 입력해 주세요.");
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("username", usernameIF.text);
            form.AddField("password", passwordIF.text);
            form.AddField("name", nameIF.text);
            form.AddField("gender", "M");
            httpPostSender.Send(URLs.signup, form, PostType.SignUp);
        }
    }

    public void BackButtonClicked()
    {
        pageController.ShowLoginPage();
    }
}