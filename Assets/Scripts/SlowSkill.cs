using UnityEngine;

public class SlowSkill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Bot")
        {
            // This Bot script will be Ball script
            other.gameObject.GetComponent<Ball>().SlowBall();
        }
    }
}
