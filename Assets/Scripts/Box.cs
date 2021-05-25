using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    GiftWindow giftWindow;

    [SerializeField] GameObject collectParticles;
    [SerializeField] GameObject components;
    [SerializeField] BoxCollider col;

    void Start()
    {
        giftWindow = FindObjectOfType<GiftWindow>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // If the hit is ball then run spinner else give a random skill
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                // Turn the spinner
                giftWindow.StartSpinning();
            } else
            {
                // Give random gift to the bot with 3 seconds delay as if it is spinning
                Bot bot = other.gameObject.GetComponent<Bot>();
                if (bot != null)
                {
                    bot.GiveRandomGift();
                }
            }

            StartCoroutine(DestroyBox(2));
        }
    }

    #region Public Methods
    #endregion

    #region Coroutines
    IEnumerator DestroyBox(float delay)
    {
        collectParticles.SetActive(true);
        components.SetActive(false);
        col.enabled = false;

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
    #endregion
}
