using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] GameObject itemRank;
    [SerializeField] GameObject itemName;
    [SerializeField] GameObject itemXP;
    [SerializeField] GameObject frame;
    [SerializeField] GameObject wrapper;

    Color32 goldColor = new Color32(255, 215, 0, 255);
    Color32 silverColor = new Color32(192, 192, 192, 255);
    Color32 bronzeColor = new Color32(205, 127, 50, 255);

    #region Public Methods
    public void SetItemEntry(int _rank, string _name, int _xp)
    {
        itemRank.SetActive(true);
        itemName.SetActive(true);
        itemXP.SetActive(true);

        itemRank.GetComponent<Text>().text = _rank.ToString();
        itemName.GetComponent<Text>().text = _name;
        itemXP.GetComponent<Text>().text = _xp.ToString();
    }

    public void SetLeaderboardItemColor(LeaderboardItemColor color)
    {
        switch (color)
        {
            case LeaderboardItemColor.Gold:
                frame.GetComponent<Image>().color = goldColor;
                break;
            case LeaderboardItemColor.Silver:
                frame.GetComponent<Image>().color = silverColor;
                break;
            case LeaderboardItemColor.Bronze:
                frame.GetComponent<Image>().color = bronzeColor;
                break;
        }
    }

    public void SetYouItem()
    {
        wrapper.SetActive(true);
    }
    #endregion
}
