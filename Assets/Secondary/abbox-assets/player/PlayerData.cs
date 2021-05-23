using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int coins = 0;
    public int diamonds = 0;
    public int currentBallIndex = 0;
    public int nextLevelIndex = 1;
    public string playerName = "";
    public bool playerCreated = false;
    public bool nameChanged = false;
    public bool privacyPolicyAccepted = false;
    public bool privacyPolicyDeclined = false;
    public List<int> allBalls = new List<int>();

    public PlayerData (Player player)
    {
        coins = player.coins;
        diamonds = player.diamonds;
        allBalls = player.allBalls;
        currentBallIndex = player.currentBallIndex;
        nextLevelIndex = player.nextLevelIndex;
        playerName = player.playerName;
        playerCreated = player.playerCreated;
        nameChanged = player.nameChanged;
        privacyPolicyAccepted = player.privacyPolicyAccepted;
        privacyPolicyDeclined = player.privacyPolicyDeclined;
    }
}
