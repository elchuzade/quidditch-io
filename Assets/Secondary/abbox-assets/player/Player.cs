﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    public List<int> allBalls = new List<int>() { 1, 0, 0, 0, 0 };
    public bool newChallengeUnlocked = false;

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
        coins = 900;
        diamonds = 900;
        currentBallIndex = 2;
        nextLevelIndex = 1;
        playerName = "";
        playerCreated = false;
        nameChanged = false;
        privacyPolicyAccepted = false;
        privacyPolicyDeclined = false;
        newChallengeUnlocked = true;

        allBalls = new List<int>() { 1, 0, 1, 0, 0 };

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

        coins = data.coins;
        diamonds = data.diamonds;
        currentBallIndex = data.currentBallIndex;
        playerName = data.playerName;
        allBalls = data.allBalls;
        playerCreated = data.playerCreated;
        nameChanged = data.nameChanged;
        nextLevelIndex = data.nextLevelIndex;
        privacyPolicyAccepted = data.privacyPolicyAccepted;
        privacyPolicyDeclined = data.privacyPolicyDeclined;
        newChallengeUnlocked = data.newChallengeUnlocked;
    }
}
