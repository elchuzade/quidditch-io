using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;

    [SerializeField] RandomName randomNames;

    [SerializeField] GameObject canvas;

    [SerializeField] GameObject leadersWindowClosed;
    [SerializeField] GameObject leadersWindowOpened;

    // To make leaderboard shift to the right based on the length of name
    Vector3 defaultFirstPosition;
    Vector3 defaultSecondPosition;
    Vector3 defaultThirdPosition;
    Vector3 defaultFourthPosition;
    Vector3 defaultFifthPosition;
    float leadersNameWidth;
    float leadersNameMargin = 14;

    [SerializeField] Text first;
    [SerializeField] Text second;
    [SerializeField] Text third;
    [SerializeField] Text fourth;
    [SerializeField] Text fifth;

    Color32 yourColor = new Color32(146, 255, 107, 255);
    Color32 defaultColor = new Color32(255, 255, 255, 255);
    float defaultScreenWidth = 750;
    float screenRatio;

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

    public string selectedMode;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
        canvas.SetActive(true);
        balls = FindObjectsOfType<Ball>();
    }

    void Start()
    {
        screenRatio = Screen.width / defaultScreenWidth;
        defaultFirstPosition = first.transform.position;
        defaultSecondPosition = second.transform.position;
        defaultThirdPosition = third.transform.position;
        defaultFourthPosition = fourth.transform.position;
        defaultFifthPosition = fifth.transform.position;

        Rect nameRect = first.transform.parent.GetComponent<RectTransform>().rect;
        leadersNameWidth = nameRect.width;

        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        selectedMode = player.selectedMode;

        InstantiatePlayerBall();
        SetRandomNames();
        SetMyName();

        SetLeaderboard();
        SetCanvasValues();

        // Save click
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long date = now.ToUnixTimeMilliseconds();
        if (player.selectedMode == "push")
        {
            targetKills.transform.parent.gameObject.SetActive(false);

            player.pushModePlayed.Add(date);
        } else
        {
            player.targetModePlayed.Add(date);
        }
        player.SavePlayer();
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

    void SetMyName()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].ballId == 0)
            {
                balls[i].ballName = player.playerName;
            }
        }
    }

    void SetRandomNames()
    {
        for (int i = 0; i < bots.transform.childCount; i++)
        {
            string name = randomNames.GetRandomName();
            bots.transform.GetChild(i).GetComponent<Ball>().SetName(name);
        }
    }

    void ModifyLeadersInput(Text leadersName, string name, Vector3 defaultPosition, int ballId)
    {
        leadersName.text = name;
        leadersName.transform.parent.transform.position = defaultPosition;

        Debug.Log(leadersNameWidth);

        float shift = leadersNameWidth - leadersName.preferredWidth;

        leadersName.transform.parent.transform.position += new Vector3(shift * screenRatio, 0, 0);

        if (ballId == 0)
        {
            leadersName.color = yourColor;
        }
        else
        {
            leadersName.color = defaultColor;
        }
    }

    void SetLeaderboardName(int i, int j)
    {
        switch (j)
        {
            case 0:
                ModifyLeadersInput(first, balls[i].ballName, defaultFirstPosition, balls[i].ballId);
                break;
            case 1:
                ModifyLeadersInput(second, balls[i].ballName, defaultSecondPosition, balls[i].ballId);
                break;
            case 2:
                ModifyLeadersInput(third, balls[i].ballName, defaultThirdPosition, balls[i].ballId);
                break;
            case 3:
                ModifyLeadersInput(fourth, balls[i].ballName, defaultFourthPosition, balls[i].ballId);
                break;
            case 4:
                ModifyLeadersInput(fifth, balls[i].ballName, defaultFifthPosition, balls[i].ballId);
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
