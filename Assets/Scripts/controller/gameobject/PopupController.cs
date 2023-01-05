using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField]
    private GameObject alertPopup;

    [SerializeField]
    private Text alertText;

    public void ShowAlertPopup(string message)
    {
        alertText.text = message;
        alertPopup.SetActive(true);
    }

    public void HideAlertPopup()
    {
        alertPopup.SetActive(false);
    }
}
