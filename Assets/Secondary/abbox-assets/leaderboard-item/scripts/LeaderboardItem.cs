using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] GameObject itemRank;
    [SerializeField] GameObject itemName;
    [SerializeField] GameObject itemIcon;
    [SerializeField] GameObject dots;

    #region Public Methods
    public void SetLeaderboardItem(int _rank, string _name, Sprite _icon)
    {
        dots.SetActive(false);
        itemRank.SetActive(true);
        itemName.SetActive(true);
        itemIcon.SetActive(true);

        itemIcon.GetComponent<Image>().sprite = _icon;
        itemRank.GetComponent<Text>().text = _rank.ToString();
        itemName.GetComponent<Text>().text = _name;
    }

    public void SetDots()
    {
        dots.SetActive(true);
        itemRank.SetActive(false);
        itemName.SetActive(false);
        itemIcon.SetActive(false);
    }
    #endregion
}
