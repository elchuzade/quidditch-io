using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] int unlockXP;
    [SerializeField] int index;

    [SerializeField] GameObject selectedText;
    [SerializeField] GameObject selectButton;
    [SerializeField] GameObject unlocksAtText;

    void Start()
    {
        selectedText.SetActive(false);
        selectButton.SetActive(false);
        unlocksAtText.SetActive(true);

        unlocksAtText.transform.GetChild(0).GetComponent<Text>().text = "UNLOCKS AT " + unlockXP.ToString() + " XP";
    }

    #region Public Methods
    // @access from ShopStatus
    public void SelectItem()
    {
        selectedText.SetActive(true);
        selectButton.SetActive(false);
        unlocksAtText.SetActive(false);
    }

    // @access from ShopStatus
    public void UnlockItem()
    {
        selectedText.SetActive(false);
        selectButton.SetActive(true);
        unlocksAtText.SetActive(false);
    }

    // @access from ShopStatus
    public void DeselectItem()
    {
        selectedText.SetActive(false);
        selectButton.SetActive(true);
    }

    // @access from ShopStatus
    public int GetIndex()
    {
        return index;
    }

    // @access from ShopStatus
    public int GetXP()
    {
        return unlockXP;
    }
    #endregion
}