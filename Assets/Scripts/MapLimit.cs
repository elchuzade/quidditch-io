using UnityEngine;

public class MapLimit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            other.GetComponent<Barrier>().DestroyBarrier();
        }
    }
}
