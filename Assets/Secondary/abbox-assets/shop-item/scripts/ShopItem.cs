using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public enum Currency { Coins, Diamonds }

    [SerializeField] GameObject selectedFrame;
    [SerializeField] GameObject lockedFrame;
    [SerializeField] GameObject priceTag;
    [SerializeField] GameObject coinsIcon;
    [SerializeField] GameObject diamondsIcon;
    [SerializeField] GameObject priceText;

    public bool unlocked;
    public string itemName;
    public string itemPrice;
    public Currency itemCurrency;

    void Start()
    {
        if (itemCurrency == Currency.Coins)
        {
            coinsIcon.SetActive(true);
            diamondsIcon.SetActive(false);
        } else if (itemCurrency == Currency.Diamonds)
        {
            coinsIcon.SetActive(false);
            diamondsIcon.SetActive(true);
        }
        priceText.GetComponent<Text>().text = itemPrice;
    }

    #region Public Methods
    public void SelectItem()
    {
        selectedFrame.SetActive(true);
        lockedFrame.SetActive(false);
        priceTag.SetActive(false);
    }

    public void UnlockItem()
    {
        unlocked = true;
        lockedFrame.SetActive(false);
        priceTag.SetActive(false);
    }
    #endregion
}