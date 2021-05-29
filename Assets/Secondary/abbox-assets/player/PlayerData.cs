using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int xp = 0;
    public int currentBallIndex = 0;
    public string playerName = "";
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

    public PlayerData (Player player)
    {
        xp = player.xp;
        currentBallIndex = player.currentBallIndex;
        playerName = player.playerName;
        playerCreated = player.playerCreated;
        privacyPolicyAccepted = player.privacyPolicyAccepted;
        privacyPolicyDeclined = player.privacyPolicyDeclined;
        newChallengeUnlocked = player.newChallengeUnlocked;
        newSkinUnlocked = player.newSkinUnlocked;
        selectedMode = player.selectedMode;

        shopClicks = player.shopClicks;
        challengesClicks = player.challengesClicks;
        leaderboardClicks = player.leaderboardClicks;

        pushModePlayed = player.pushModePlayed;
        targetModePlayed = player.targetModePlayed;
    }
}
