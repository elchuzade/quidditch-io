using UnityEngine;
using static GlobalVariables;

public class GiftWindow : MonoBehaviour
{
    Player player;
    [Header("Where the logic for setting counts is")]
    [SerializeField] LevelStatus levelStatus;

    [SerializeField] GameObject allSpinners;

    [SerializeField] Ball ball;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        StartSpinning();
    }

    #region Public Methods
    #endregion

    #region Private Methods
    public void SetBallSkill(Skill skill)
    {
        ball.SetGivenSkill(skill);
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
