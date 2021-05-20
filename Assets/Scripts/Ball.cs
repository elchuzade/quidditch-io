using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;

    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider col;

    // Opposite to mouse drag direction
    Vector3 direction;

    [SerializeField] GameObject arrows;
    [SerializeField] GameObject arrowOne;
    [SerializeField] GameObject arrowTwo;
    [SerializeField] GameObject arrowThree;

    float baseSpeed = 50;
    float speed = 0;
    // When score increases, the multiplier increases too
    float baseSpeedStep = 50;
    float massStep = 50;

    // This is not to wait until the full stop
    float almostStopped = 100f;

    // When you are sucked in to the hole disable controls
    bool disabled = false;

    // To allow aiming
    bool idle = true;

    // Distance to which the player needs to approach the hole center to reappear
    float holeCenterMargin = 10;

    Vector3 holePosition;
    float holeSuckSpeed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        if (disabled)
        {
            if (Vector2.Distance(transform.position, holePosition) > holeCenterMargin)
            {
                Vector3 movePos = Vector3.MoveTowards(transform.position, holePosition, holeSuckSpeed);
                transform.position = movePos;
                transform.localScale *= 0.99f;
            }
            else
            {
                AddScore();
                Reappear();
            }
        }
        else
        {
            if (rb.velocity.magnitude < almostStopped)
            {
                idle = true;
            }
            else
            {
                idle = false;
            }
            if (idle)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    HideArrows();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (idle)
        {
            ShowArrows();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            CoughtByHole(other.gameObject.transform.position);
        }
    }

    #region Public Methods
    // @access from Hole script
    public void CoughtByHole(Vector3 _holePosition)
    {
        col.enabled = false;
        holePosition = _holePosition;
        disabled = true;
        rb.velocity = Vector3.zero;
    }
    #endregion

    #region Private Methods
    void AddScore()
    {
        Debug.Log("Scored");
    }

    void Reappear()
    {
        disabled = false;
        col.enabled = true;
        idle = true;
        transform.position = new Vector3(Random.Range(-50, 50) + 375, 800, Random.Range(-50, 50));
        transform.localScale = Vector3.one;
    }

    void ShowArrows()
    {
        direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        arrows.transform.rotation = Quaternion.Euler(90, 0, angle - 90);

        float value = direction.magnitude;

        if (value < 0.25f)
        {
            arrowOne.SetActive(false);
            arrowTwo.SetActive(false);
            arrowThree.SetActive(false);
            speed = baseSpeed * 0;
        }
        else if (value >= 0.25f && value < 0.5f)
        {
            arrowOne.SetActive(true);
            arrowTwo.SetActive(false);
            arrowThree.SetActive(false);
            speed = baseSpeed * 1;
        }
        else if (value >= 0.5f && value < 0.75f)
        {
            arrowOne.SetActive(true);
            arrowTwo.SetActive(true);
            arrowThree.SetActive(false);
            speed = baseSpeed * 2;
        }
        else
        {
            arrowOne.SetActive(true);
            arrowTwo.SetActive(true);
            arrowThree.SetActive(true);
            speed = baseSpeed * 3;
        }
    }

    void HideArrows()
    {
        arrowOne.SetActive(false);
        arrowTwo.SetActive(false);
        arrowThree.SetActive(false);

        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }
    #endregion
}
