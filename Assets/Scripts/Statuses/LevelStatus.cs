using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject canvas; // Hiding for development purposes
    [SerializeField] Scoreboard scoreboard;

    [SerializeField] GameObject leadersWindowClosed;
    [SerializeField] GameObject leadersWindowOpened;

    void Awake()
    {
        canvas.SetActive(true);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        scoreboard.SetCoins(player.coins);
        scoreboard.SetDiamonds(player.diamonds);
    }

    #region Public Methods
    public void ClickOpenedLeadersWindow()
    {
        leadersWindowClosed.SetActive(true);
        leadersWindowOpened.SetActive(false);
    }

    public void ClickClosedLeadersWindow()
    {
        leadersWindowClosed.SetActive(false);
        leadersWindowOpened.SetActive(true);
    }
    #endregion
}
