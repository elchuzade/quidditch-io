using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    Spinner spinner;

    [SerializeField] GameObject collectParticles;
    [SerializeField] GameObject components;
    [SerializeField] BoxCollider col;

    void Start()
    {
        spinner = FindObjectOfType<Spinner>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // If the hit is ball then run spinner else give a random skill
            if (other.GetComponent<Ball>().ballId == 0)
            {
                // Turn the spinner
                spinner.StartSpinning();
            } else
            {
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
