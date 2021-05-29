using System;
using UnityEngine;

public class ChallengesStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        player.newChallengeUnlocked = false;
        player.SavePlayer();

        // Save click
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long date = now.ToUnixTimeMilliseconds();
        player.challengesClicks.Add(date);
        player.SavePlayer();
    }

    #region Public Methods
    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }
    #endregion
}
