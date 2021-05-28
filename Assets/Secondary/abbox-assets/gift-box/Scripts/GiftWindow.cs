using UnityEngine;
using static GlobalVariables;
using UnityEngine.UI;

public class GiftWindow : MonoBehaviour
{
    Player player;
    [Header("Where the logic for setting counts is")]
    [SerializeField] LevelStatus levelStatus;

    [SerializeField] GameObject allSpinners;

    [SerializeField] Ball ball;

    [SerializeField] Image loader;

    float time;
    bool reloading;
    float reloadTime;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        StartSpinning();
    }

    void Update()
    {
        if (reloading)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                loader.fillAmount = time / reloadTime;
            }
            else
            {
                RemoveGivenSkill();
            }
        }
    }

    #region Private Methods
    void RemoveGivenSkill()
    {
        for (int i = 0; i < allSpinners.transform.childCount; i++)
        {
            allSpinners.transform.GetChild(i).GetComponent<Spinner>().HideSpinner();
        }
        reloading = false;
    }
    #endregion

    #region Public Methods
    public void SetBallSkill(Skill skill)
    {
        ball.SetGivenSkill(skill);

        // Start Loading skill duration
        time = reloadTime = ball.GetSkillDuration(skill);
        loader.fillAmount = 1;
        reloading = true;
    }

    public void StartSpinning()
    {
        // Save click
        //System.DateTimeOffset now = System.DateTimeOffset.UtcNow;
        //long date = now.ToUnixTimeMilliseconds();
        //player.spinnerClicks.Add(date);
        //player.SavePlayer();

        for (int i = 0; i < allSpinners.transform.childCount; i++)
        {
            allSpinners.transform.GetChild(i).GetComponent<Spinner>().InitializeSpinner();
        }
    }
    #endregion
}
