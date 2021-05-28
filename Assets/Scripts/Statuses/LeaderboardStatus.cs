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
    }

    #region Public Methods
    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }
    #endregion
}
