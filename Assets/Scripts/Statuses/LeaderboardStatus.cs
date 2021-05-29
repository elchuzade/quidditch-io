using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Server;
using static GlobalVariables;

public class LeaderboardStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;
    Server server;

    [SerializeField] Text xp;

    // Single line of leadersboard
    [SerializeField] GameObject leaderboardItemPrefab;
    [SerializeField] GameObject leaderboardItemTrippleDots;
    // To set parent for leaderboard items
    [SerializeField] GameObject leaderboardScrollContent;
    // To scroll down to your position
    [SerializeField] Scrollbar leaderboardScrollbar;

    List<LeaderboardValue> before = new List<LeaderboardValue>();
    List<LeaderboardValue> after = new List<LeaderboardValue>();
    List<LeaderboardValue> top = new List<LeaderboardValue>();
    LeaderboardValue you = new LeaderboardValue();

    Color32 goldColor = new Color32(255, 215, 0, 255);
    Color32 silverColor = new Color32(192, 192, 192, 255);
    Color32 bronzeColor = new Color32(205, 127, 50, 255);

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
        server = FindObjectOfType<Server>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.ResetPlayer();
        player.LoadPlayer();

        SetPlayerXP();

        server.GetLeaderboard();

        // Save click
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long date = now.ToUnixTimeMilliseconds();
        player.leaderboardClicks.Add(date);
        player.SavePlayer();
    }

    #region Private Methods
    void SetPlayerXP()
    {
        xp.text = player.xp.ToString() + " XP";
    }

    private IEnumerator ScrollListToPlayer()
    {
        yield return new WaitForSeconds(0.2f);

        leaderboardScrollbar.value = 0;
    }

    GameObject InstantiateLeaderboardItem()
    {
        GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, transform.position, Quaternion.identity);
        return leaderboardItem;
    }

    void BuildUpList()
    {
        // Loop through top ten players and instantiate an item object
        top.ForEach(item =>
        {
            // Check for revert leaderboard items
            GameObject leaderboardItem = InstantiateLeaderboardItem();

            // Set its parent to be scroll content, for scroll functionality to work properly
            leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
            leaderboardItem.transform.localScale = Vector3.one;

            Debug.Log(item.rank);

            if (item.rank == 1)
            {
                leaderboardItem.GetComponent<LeaderboardItem>().SetLeaderboardItemColor(LeaderboardItemColor.Gold);
            }
            else if (item.rank == 2)
            {
                leaderboardItem.GetComponent<LeaderboardItem>().SetLeaderboardItemColor(LeaderboardItemColor.Silver);
            }
            else if (item.rank == 3)
            {
                leaderboardItem.GetComponent<LeaderboardItem>().SetLeaderboardItemColor(LeaderboardItemColor.Bronze);
            }

            // Compare item from top ten with your rank incase you are in top ten
            if (item.rank == you.rank)
            {
                // Show frame around your entry
                leaderboardItem.GetComponent<LeaderboardItem>().SetYouItem();
            }

            leaderboardItem.GetComponent<LeaderboardItem>().SetItemEntry(item.rank, item.playerName, item.xp);
        });

        // Add tripple dots after top ten only if your rank is > 14,
        // since at 14 the the top ten and 3 before you become continuous, so no need for dots in between
        if (you.rank > 14)
        {
            CreateTrippleDotsEntry();
        }

        // Loop through before players and instantiate an item object
        before.ForEach(item =>
        {
            // Check for revert leaderboard items
            GameObject leaderboardItem = InstantiateLeaderboardItem();

            // Set its parent to be scroll content, for scroll functionality to work properly
            leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
            leaderboardItem.transform.localScale = Vector3.one;

            leaderboardItem.GetComponent<LeaderboardItem>().SetItemEntry(item.rank, item.playerName, item.xp);
        });

        // Create your entry item only if your rank is not in top ten
        // 0 is assigned by default if there is no value

        if (you.rank > 10)
        {
            CreateYourEntry();
        }

        // Loop through after players and instantiate an item object
        after.ForEach(item =>
        {
            // Check for revert leaderboard items
            GameObject leaderboardItem = InstantiateLeaderboardItem();

            // Set its parent to be scroll content, for scroll functionality to work properly
            leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
            leaderboardItem.transform.localScale = Vector3.one;

            leaderboardItem.GetComponent<LeaderboardItem>().SetItemEntry(item.rank, item.playerName, item.xp);
        });

        CreateTrippleDotsEntry();

        // Add the scroll value after all the data is populated
        ScrollListToPlayer();
    }

    void CreateTrippleDotsEntry()
    {
        // Create tripple dots to separate different lists
        GameObject leaderboardItem = Instantiate(leaderboardItemTrippleDots, transform.position, Quaternion.identity);
        // Set its parent to be scroll content, for scroll functionality to work properly
        leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
    }

    void CreateYourEntry()
    {
        // Create tripple dots to separate different lists
        GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, transform.position, Quaternion.identity);

        // Set its parent to be scroll content, for scroll functionality to work properly
        leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
        leaderboardItem.transform.localScale = Vector3.one;
        // Show frame around your entry
        leaderboardItem.GetComponent<LeaderboardItem>().SetYouItem();
        leaderboardItem.GetComponent<LeaderboardItem>().SetItemEntry(you.rank, you.playerName, you.xp);
    }

    // Loop through top ten, 3 before and 3 after lists to find if give data exists not to repeat
    bool CheckIfExists(int rank)
    {
        if (CheckIfExistInTop(rank) ||
            CheckIfExistInBefore(rank) ||
            CheckIfExistInAfter(rank))
        {
            return true;
        }
        return false;
    }

    // Loop through the list of players ranked in top ten and see if iven data exists
    bool CheckIfExistInTop(int rank)
    {
        for (int i = 0; i < top.Count; i++)
        {
            if (top[i].rank == rank)
            {
                return true;
            }
        }
        return false;
    }

    // Loop through the list of players ranked before you and see if iven data exists
    bool CheckIfExistInBefore(int rank)
    {
        for (int i = 0; i < before.Count; i++)
        {
            if (before[i].rank == rank)
            {
                return true;
            }
        }
        return false;
    }

    // Loop through the list of players ranked after you and see if iven data exists
    bool CheckIfExistInAfter(int rank)
    {
        for (int i = 0; i < after.Count; i++)
        {
            if (after[i].rank == rank)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Public Methods
    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }

    public void SetLeaderboardData(List<LeaderboardValue> topData, List<LeaderboardValue> beforeData, LeaderboardValue youData, List<LeaderboardValue> afterData)
    {
        // Clear the lists incase they already had data in them
        foreach (Transform child in leaderboardScrollContent.transform)
        {
            Destroy(child.gameObject);
        }
        top.Clear();
        before.Clear();
        after.Clear();
        if (topData != null)
        {
            // Loop though top ten list provided by the server and add them to local list
            for (int i = 0; i < topData.Count; i++)
            {
                top.Add(topData[i]);
            }
        }
        if (beforeData != null)
        {
            // Loop though up to 3 players before you list provided by the server
            // and if they have not yet been added to the list add them
            for (int i = 0; i < beforeData.Count; i++)
            {
                if (!CheckIfExists(beforeData[i].rank))
                {
                    before.Add(beforeData[i]);
                }
            }
        }
        if (youData != null)
        {
            you.rank = youData.rank;
            // Check if your rank has already been added to the list if not add it
            if (!CheckIfExists(youData.rank))
            {
                you = youData;
            }
        }
        if (afterData != null)
        {
            // Loop though up to 3 players after you list provided by the server
            // and if they have not yet been added to the list add them
            for (int i = 0; i < afterData.Count; i++)
            {
                if (!CheckIfExists(afterData[i].rank))
                {
                    after.Add(afterData[i]);
                }
            }
        }

        BuildUpList();
    }
    #endregion
}
