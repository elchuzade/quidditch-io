using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider col;

    // Distance to which the player needs to approach the hole center to reappear
    float holeCenterMargin = 10;
    float holeSuckSpeed = 2;
    Vector3 holePosition;

    bool disabled = false;
    float speed = 2000;

    void Start()
    {
        StartCoroutine(RotateTarget());
    }

    void Update()
    {
        if (disabled)
        {
            if (Vector2.Distance(transform.position, holePosition) > holeCenterMargin)
            {
                Vector3 movePos = Vector3.MoveTowards(transform.position, holePosition, holeSuckSpeed);
                transform.position = movePos;
                transform.localScale *= 0.9f;
            }
            else
            {
                AddScore();
                Reappear();
            }
        }
    }

    #region Private Methods
    void AddScore()
    {
        Debug.Log("Scored");
    }

    void Reappear()
    {
        disabled = false;
        col.enabled = true;
        transform.position = new Vector3(Random.Range(-50, 50) + 375, 730, Random.Range(-50, 50));
        transform.localScale = Vector3.one;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            CoughtByHole(other.gameObject.transform.position);
        }
    }

    void CoughtByHole(Vector3 _holePosition)
    {
        col.enabled = false;
        holePosition = _holePosition;
        disabled = true;
        rb.velocity = Vector3.zero;
    }
    #endregion

    #region Coroutines
    IEnumerator RotateTarget()
    {
        yield return new WaitForSeconds(2);

        if (!disabled)
        {
            float angle = Random.Range(0, 360);

            transform.RotateAround(transform.position, Vector3.up, angle);

            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        StartCoroutine(RotateTarget());
    }
    #endregion
}
