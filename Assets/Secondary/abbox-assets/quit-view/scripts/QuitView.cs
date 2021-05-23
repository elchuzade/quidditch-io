using UnityEngine;
using UnityEngine.UI;

public class QuitView : MonoBehaviour
{
    [SerializeField] Text quitText;

    public string gameName;

    void Start()
    {
        quitText.text = "Are you sure" + "\n" + "You want to Quit" + "\n" + gameName;
        HideView();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowView();
        }
    }

    #region Private Methods
    void HideView()
    {
        transform.localScale = Vector3.zero;
    }

    void ShowView()
    {
        transform.localScale = Vector3.one;
    }
    #endregion

    #region Public Methods
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinuePlaying()
    {
        HideView();
    }
    #endregion
}
