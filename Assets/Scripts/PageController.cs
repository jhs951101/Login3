using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField]
    private GameObject loginPage;

    [SerializeField]
    private GameObject signUpPage;

    private GameObject[] menus = new GameObject[2];

    void Start()
    {
        menus[0] = loginPage;
        menus[1] = signUpPage;
    }

    void Initialize()
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(false);
    }

    public void ShowLoginPage()
    {
        Initialize();
        loginPage.SetActive(true);
    }

    public void ShowSignUpPage()
    {
        Initialize();
        signUpPage.SetActive(true);
    }
}
