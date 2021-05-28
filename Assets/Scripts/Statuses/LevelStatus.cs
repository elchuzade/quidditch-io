using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;

    [SerializeField] RandomName randomNames;

    [SerializeField] GameObject canvas; // Hiding for development purposes

    [SerializeField] GameObject leadersWindowClosed;
    [SerializeField] GameObject leadersWindowOpened;

    [SerializeField] Text first;
    [SerializeField] Text second;
    [SerializeField] Text third;
    [SerializeField] Text fourth;
    [SerializeField] Text fifth;

    Color32 yourColor = new Color32(146, 255, 107, 255);
    Color32 defaultColor = new Color32(255, 255, 255, 255);
    int yourFontSize = 28;
    int defaultFontSize = 24;

    [SerializeField] GameObject bots;
    [SerializeField] Ball[] balls;

    [SerializeField] GameObject ballParent;
    [SerializeField] GameObject[] allBalls;

    [SerializeField] Text timer;
    [SerializeField] Text targetKills;
    [SerializeField] Text botKills;

    int botKillsCount = 0;
    int targetKillsCount = 0;
    float secondsLeft = 120;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
        canvas.SetActive(true);
        balls = FindObjectsOfType<Ball>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        InstantiatePlayerBall();
        SetRandomNames();

        SetLeaderboard();
        SetCanvasValues();
    }

    void Update()
    {
        if (secondsLeft > 0)
        {
            secondsLeft -= Time.deltaTime;
            FormatTime(secondsLeft);
        } else
        {
            Debug.Log("Game Over");
        }
    }

    #region Private Methods
    void SetCanvasValues()
    {
        targetKills.text = targetKillsCount.ToString();
        botKills.text = botKillsCount.ToString();
    }

    void FormatTime(float totalSeconds)
    {
        int minutes = (int)totalSeconds / 60;
        int seconds = (int)totalSeconds - (minutes * 60);

        timer.text = minutes.ToString() + ":" + seconds.ToString();
    }

    void InstantiatePlayerBall()
    {
        GameObject ballInstance = Instantiate(allBalls[player.currentBallIndex], ballParent.transform.position, Quaternion.identity);
        ballInstance.transform.SetParent(ballParent.transform);
        ballInstance.transform.localScale = new Vector3(20, 20, 20);
    }

    void SetRandomNames()
    {
        for (int i = 0; i < bots.transform.childCount; i++)
        {
            string name = randomNames.GetRandomName();
            bots.transform.GetChild(i).GetComponent<Ball>().SetName(name);
        }
    }

    void SetLeaderboardName(int i, int j)
    {
        switch (j)
        {
            case 0:
                first.text = balls[i].ballName;
                if (balls[i].ballId == 0)
                {
                    first.color = yourColor;
                    first.fontSize = yourFontSize;
                }
                else
                {
                    first.color = defaultColor;
                    first.fontSize = defaultFontSize;
                }
                break;
            case 1:
                second.text = balls[i].ballName;
                if (balls[i].ballId == 0)
                {
                    second.color = yourColor;
                    second.fontSize = yourFontSize;
                }
                else
                {
                    second.color = defaultColor;
                    second.fontSize = defaultFontSize;
                }
                break;
            case 2:
                third.text = balls[i].ballName;
                if (balls[i].ballId == 0)
                {
                    third.color = yourColor;
                    third.fontSize = yourFontSize;
                }
                else
                {
                    third.color = defaultColor;
                    third.fontSize = defaultFontSize;
                }
                break;
            case 3:
                fourth.text = balls[i].ballName;
                if (balls[i].ballId == 0)
                {
                    fourth.color = yourColor;
                    fourth.fontSize = yourFontSize;
                }
                else
                {
                    fourth.color = defaultColor;
                    fourth.fontSize = defaultFontSize;
                }
                break;
            case 4:
                fifth.text = balls[i].ballName;
                if (balls[i].ballId == 0)
                {
                    fifth.color = yourColor;
                    fifth.fontSize = yourFontSize;
                }
                else
                {
                    fifth.color = defaultColor;
                    fifth.fontSize = defaultFontSize;
                }
                break;
        }
    }

    void SetLeaderboard()
    {
        // Id and Score
        List<(int, int)> leaderboardItems = new List<(int, int)>();

        for (int i = 0; i < balls.Length; i++)
        {
            leaderboardItems.Add((balls[i].ballId, balls[i].ballScore));
        }
        // Sort
        leaderboardItems.Sort((x, y) => y.Item2.CompareTo(x.Item2));

        for (int j = 0; j < leaderboardItems.Count; j++)
        {
            for (int i = 0; i < balls.Length; i++)
            {
                if (leaderboardItems[j].Item1 == balls[i].ballId)
                {
                    balls[i].ballRank = j;
                    SetLeaderboardName(i, j);
                }
            }
        }
    }
    #endregion

    #region Public Methods
    public void AddScore(int id, bool target)
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].ballId == id)
            {
                balls[i].ballScore++;
            }
        }
        SetLeaderboard();
        if (id == 0)
        {
            // My player scored
            if (target)
            {
                targetKillsCount++;
            } else
            {
                botKillsCount++;
            }
            SetCanvasValues();
        }
    }

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
