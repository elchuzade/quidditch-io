using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyView : MonoBehaviour
{
    public string termsLink;
    public string privacyLink;

    #region Private Methods

    #endregion

    #region Public Methods
    public void HideView()
    {
        gameObject.SetActive(false);
    }

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void OpenTermsOfUse()
    {
        Application.OpenURL(termsLink);
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyLink);
    }
    #endregion
}
