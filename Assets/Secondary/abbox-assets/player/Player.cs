using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
        nextLevelIndex = 1;
        playerName = "";
        playerCreated = false;
        nameChanged = false;
        privacyPolicyAccepted = false;
        privacyPolicyDeclined = false;
        newChallengeUnlocked = true;
        newSkinUnlocked = true;
        selectedMode = "push";

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
        nameChanged = data.nameChanged;
        nextLevelIndex = data.nextLevelIndex;
        privacyPolicyAccepted = data.privacyPolicyAccepted;
        privacyPolicyDeclined = data.privacyPolicyDeclined;
        newChallengeUnlocked = data.newChallengeUnlocked;
        newSkinUnlocked = data.newSkinUnlocked;
        selectedMode = data.selectedMode;
    }
}
