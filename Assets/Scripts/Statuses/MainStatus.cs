using UnityEngine;
using UnityEngine.UI;
using static Server;

public class MainStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;
    Server server;
    TV tv;

    [SerializeField] Text xp;
    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject challengesButton;
    [SerializeField] GameObject hapticsButton;
    [SerializeField] GameObject soundsButton;
    [SerializeField] GameObject leaderboardButton;

    [SerializeField] GameObject privacyWindow;
    [SerializeField] GameObject quitGameWindow;
    [SerializeField] GameObject gameModesWindow;

    [SerializeField] GameObject ballParent;
    [SerializeField] GameObject[] allBalls;

    [SerializeField] InputField nameInput;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
        server = FindObjectOfType<Server>();
        tv = FindObjectOfType<TV>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        server.GetVideoLink();

        if (player.privacyPolicyAccepted)
        {
            privacyWindow.SetActive(false);
            leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ClickLeaderboardButton());

            if (!player.playerCreated)
            {
                //server.CreatePlayer(player);
            }
            else
            {
                //server.SavePlayerData(player);
            }
        }
        else
        {
            privacyWindow.SetActive(true);
            leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ShowPrivacyPolicy());
            leaderboardButton.GetComponent<Image>().color = new Color32(255, 197, 158, 100);
        }

        SetPlayerXP();

        SetButtonInitialState();
        SetChallengesButton();
        SetShopButton();

        GameObject ballPrefab = Instantiate(allBalls[player.currentBallIndex], ballParent.transform.position, Quaternion.identity);
        ballPrefab.transform.SetParent(ballParent.transform);

        nameInput.text = player.playerName;
    }

    #region Private Methods
    void SetPlayerXP()
    {
        xp.text = player.xp.ToString() + " XP";
    }

    void SetChallengesButton()
    {
        if (player.newChallengeUnlocked)
        {
            challengesButton.transform.Find("Rocket").GetComponent<Animator>().enabled = true;
        } else
        {
            challengesButton.transform.Find("Rocket").GetComponent<Animator>().enabled = false;
        }
    }

    void SetShopButton()
    {
        if (player.newSkinUnlocked)
        {
            shopButton.transform.Find("Shop").GetComponent<Animator>().enabled = true;
        }
        else
        {
            shopButton.transform.Find("Shop").GetComponent<Animator>().enabled = false;
        }
    }

    // Set initial states of haptics button based on player prefs
    void SetButtonInitialState()
    {
        // Haptics
        if (PlayerPrefs.GetInt("Haptics") == 1)
        {
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(false);
        }
        else
        {
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(true);
        }
        // Sounds
        if (PlayerPrefs.GetInt("Sounds") == 1)
        {
            soundsButton.transform.Find("Disabled").gameObject.SetActive(false);
        }
        else
        {
            soundsButton.transform.Find("Disabled").gameObject.SetActive(true);
        }
    }
    #endregion

    #region Public Methods
    // @access from Server script
    public void CreatePlayerSuccess()
    {
        player.playerCreated = true;
        player.SavePlayer();
    }

    // @access from Server script
    public void SetVideoLinkSuccess(VideoJson response)
    {
        tv.SetAdLink(response.video);
        tv.SetAdButton(response.website);
    }

    // @access from MainStatus canvas
    public void ClickPlayButton()
    {
        player.playerName = nameInput.text;
        player.SavePlayer();

        navigator.LoadNextLevel(player.nextLevelIndex);
    }

    // @access from MainStatus canvas
    public void ClickChallengesButton()
    {
        navigator.LoadChallenges();
    }

    // @access from MainStatus canvas
    public void ClickShopButton()
    {
        navigator.LoadShop();
    }

    // @access from MainStatus canvas
    public void ClickGameModesButton()
    {
        gameModesWindow.SetActive(true);
    }

    // @access from MainStatus canvas
    public void ClickLeaderboardButton()
    {
        navigator.LoadLeaderboard();
    }

    // @access from MainStatus canvas
    public void ClickHapticsButton()
    {
        if (PlayerPrefs.GetInt("Haptics") == 1)
        {
            // Set button state to disabled
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(true);
            // If haptics are turned on => turn them off
            PlayerPrefs.SetInt("Haptics", 0);
        }
        else
        {
            // Set button state to enabled
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(false);
            // If haptics are turned off => turn them on
            PlayerPrefs.SetInt("Haptics", 1);
        }
    }

    // @access from MainStatus canvas
    public void ClickSoundsButton()
    {
        if (PlayerPrefs.GetInt("Sounds") == 1)
        {
            // Set button state to disabled
            soundsButton.transform.Find("Disabled").gameObject.SetActive(true);
            // If sounds are turned on => turn them off
            PlayerPrefs.SetInt("Sounds", 0);
        }
        else
        {
            // Set button state to enabled
            soundsButton.transform.Find("Disabled").gameObject.SetActive(false);
            // If sounds are turned off => turn them on
            PlayerPrefs.SetInt("Sounds", 1);
        }
    }

    // @access from MainStatus canvas
    public void ShowPrivacyPolicy()
    {
        privacyWindow.SetActive(true);
    }

    // @access from MainStatus canvas
    public void ClickDeclinePrivacyPolicy()
    {
        leaderboardButton.GetComponent<Button>().onClick.AddListener(() => privacyWindow.SetActive(true));
        privacyWindow.SetActive(false);
    }

    // @access from MainStatus canvas
    public void ClickAcceptPrivacyPolicy()
    {
        leaderboardButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ClickLeaderboardButton());

        privacyWindow.transform.localScale = new Vector3(0, 1, 1);
        privacyWindow.SetActive(false);
        player.privacyPolicyAccepted = true;
        player.SavePlayer();

        //server.CreatePlayer(player);
    }

    // @access from MainStatus canvas
    public void ClickQuitGame()
    {
        Application.Quit();
    }

    // @access from MainStatus canvas
    public void CancelQuitGame()
    {
        quitGameWindow.SetActive(false);
    }

    // @access from MainStatus canvas
    public void ClickTermsOfUse()
    {
        Application.OpenURL("https://abboxgames.com/terms-of-use");
    }

    // @access from MainStatus canvas
    public void ClickPrivacyPolicy()
    {
        Application.OpenURL("https://abboxgames.com/privacy-policy");
    }
    #endregion
}
