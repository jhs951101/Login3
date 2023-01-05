using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInfoController : MonoBehaviour
{
    [SerializeField]
    private Text welcomeMSG;

    void Start()
    {
        welcomeMSG.text = string.Format("{0}({1})님 환영합니다!", LoginInfo.name, LoginInfo.username);
    }

    public void LogoutButtonClicked()
    {
        LoginInfo.username = null;
        SceneManager.LoadScene("Login");
    }
}