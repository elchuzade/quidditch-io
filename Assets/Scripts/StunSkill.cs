using UnityEngine;

public class StunSkill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // This Bot script will be Ball script
        other.gameObject.GetComponent<Bot>().StunBall();
    }
}
