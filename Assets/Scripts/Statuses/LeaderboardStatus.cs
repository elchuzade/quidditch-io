using System;
using UnityEngine;

public class LeaderboardStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;
    Server server;

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

        // Save click
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long date = now.ToUnixTimeMilliseconds();
        player.leaderboardClicks.Add(date);
        player.SavePlayer();
    }

    #region Public Methods
    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }
    #endregion
}
