using UnityEngine;

public class PlayerBall : Ball
{
    public FloatingJoystick joystick;

    // Opposite to mouse drag direction
    Vector3 direction;

    public GameObject arrows;
    public GameObject arrowOne;
    public GameObject arrowTwo;
    public GameObject arrowThree;

    void Start()
    {
        initialPosition = transform.position;

        SetInitialDebuffs();
        SetInitialSkills();
        FindAllPushables();
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
                Reappear();
                AddScore();
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
            if (Input.GetMouseButtonUp(0))
            {
                HideArrows();
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

    #region Private Methods
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
            speed = GetModifiedSpeed(1);
        }
        else if (value >= 0.5f && value < 0.75f)
        {
            arrowOne.SetActive(true);
            arrowTwo.SetActive(true);
            arrowThree.SetActive(false);
            speed = GetModifiedSpeed(2);
        }
        else
        {
            arrowOne.SetActive(true);
            arrowTwo.SetActive(true);
            arrowThree.SetActive(true);
            speed = GetModifiedSpeed(3);
        }
    }

    void HideArrows()
    {
        arrowOne.SetActive(false);
        arrowTwo.SetActive(false);
        arrowThree.SetActive(false);

        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    // Add all the buffs debuffs that affect speed
    float GetModifiedSpeed(int arrowsCount)
    {
        return baseSpeed * arrowsCount * debuffSpeed * skillSpeed + weightSkillSpeed;
    }
    #endregion
}
