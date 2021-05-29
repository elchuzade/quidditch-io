using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int xp = 0;
    public int currentBallIndex = 0;
    public int nextLevelIndex = 1;
    public string playerName = "";
    public bool playerCreated = false;
    public bool nameChanged = false;
    public bool privacyPolicyAccepted = false;
    public bool privacyPolicyDeclined = false;
    public bool newChallengeUnlocked = false;
    public bool newSkinUnlocked = false;
    public string selectedMode = "push";

    public PlayerData (Player player)
    {
        xp = player.xp;
        currentBallIndex = player.currentBallIndex;
        nextLevelIndex = player.nextLevelIndex;
        playerName = player.playerName;
        playerCreated = player.playerCreated;
        nameChanged = player.nameChanged;
        privacyPolicyAccepted = player.privacyPolicyAccepted;
        privacyPolicyDeclined = player.privacyPolicyDeclined;
        newChallengeUnlocked = player.newChallengeUnlocked;
        newSkinUnlocked = player.newSkinUnlocked;
        selectedMode = player.selectedMode;
    }
}
