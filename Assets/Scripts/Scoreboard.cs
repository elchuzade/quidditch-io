using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] GameObject diamondIcon;
    [SerializeField] GameObject coinIcon;

    [SerializeField] Text diamondText;
    [SerializeField] Text coinText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Public Methods
    public void SetDiamonds(int count)
    {
        diamondText.text = count.ToString();
    }

    public void SetCoins(int count)
    {
        coinText.text = count.ToString();
    }

    public void TriggerDiamoondAnimation()
    {
        diamondIcon.GetComponent<AnimationTrigger>().Trigger("Start");
    }

    public void TriggerCoinAnimation()
    {
        coinIcon.GetComponent<AnimationTrigger>().Trigger("Start");
    }
    #endregion
}
