using UnityEngine;
using UnityEngine.UI;

public class ChangeNameView : MonoBehaviour
{
    [SerializeField] InputField nameInput;
    [SerializeField] GameObject saveButton;
    [SerializeField] GameObject getDiamondsButton;

    void Start()
    {
        getDiamondsButton.SetActive(true);
        saveButton.SetActive(false);
    }

    #region Public Methods
    public void HideView()
    {
        gameObject.SetActive(false);
    }

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void SetName(string _name)
    {
        nameInput.text = _name;
        if (!string.IsNullOrEmpty(_name))
        {
            // Name exists so do not give diamonds
            getDiamondsButton.SetActive(false);
            getDiamondsButton.SetActive(true);
        }
    }

    public string GetName()
    {
        return nameInput.text;
    }
    #endregion
}
