using UnityEngine;
using static GlobalVariables;

public class Barrier : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public BarrierTypes barrierType;

    #region Public Methods
    public void Push(Vector3 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode.Impulse);
    }

    public void DestroyBarrier()
    {
        Debug.Log("Destroy the wall, give coins to the one who last touched it");
    }
    #endregion
}
