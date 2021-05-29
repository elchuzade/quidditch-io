using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int xp = 0;
    public int currentBallIndex = 0;
    public string playerName = "Player";
    public bool playerCreated = false;
    public bool privacyPolicyAccepted = false;
    public bool privacyPolicyDeclined = false;
    public bool newChallengeUnlocked = false;
    public bool newSkinUnlocked = false;
    public string selectedMode = "push";

    public List<long> shopClicks = new List<long>();
    public List<long> challengesClicks = new List<long>();
    public List<long> leaderboardClicks = new List<long>();

    public List<long> pushModePlayed = new List<long>();
    public List<long> targetModePlayed = new List<long>();

    void Awake()
    {
        transform.SetParent(transform.parent.parent);
        // Singleton
        int instances = FindObjectsOfType<Player>().Length;
        if (instances > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void ResetPlayer()
    {
        xp = 2532;
        currentBallIndex = 0;
        playerName = "Player";
        playerCreated = false;
        privacyPolicyAccepted = false;
        privacyPolicyDeclined = false;
        newChallengeUnlocked = true;
        newSkinUnlocked = true;
        selectedMode = "push";

        shopClicks = new List<long>();
        challengesClicks = new List<long>();
        leaderboardClicks = new List<long>();

        pushModePlayed = new List<long>();
        targetModePlayed = new List<long>();

        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            ResetPlayer();
            data = SaveSystem.LoadPlayer();
        }

        xp = data.xp;
        currentBallIndex = data.currentBallIndex;
        playerName = data.playerName;
        playerCreated = data.playerCreated;
        privacyPolicyAccepted = data.privacyPolicyAccepted;
        privacyPolicyDeclined = data.privacyPolicyDeclined;
        newChallengeUnlocked = data.newChallengeUnlocked;
        newSkinUnlocked = data.newSkinUnlocked;
        selectedMode = data.selectedMode;

        shopClicks = data.shopClicks;
        challengesClicks = data.challengesClicks;
        leaderboardClicks = data.leaderboardClicks;

        pushModePlayed = data.pushModePlayed;
        targetModePlayed = data.targetModePlayed;
    }
}
