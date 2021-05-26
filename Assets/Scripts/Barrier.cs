using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject coolideParticles;

    public void DestroyBarrier()
    {
        Debug.Log("Destroy the wall, give coins to the one who last touched it");
    }
}
