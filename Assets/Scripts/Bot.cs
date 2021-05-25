using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class Bot : MonoBehaviour
{
    public bool dontMove; // For chanllenge levels where bot should not move by itself

    GameObject target;
    // Vector from your position to target position
    Vector3 direction;

    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider col;

    float baseSpeed = 2000;
    float speed = 0;
    // When score increases, the multiplier increases too
    float baseSpeedStep = 2000;
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

    [SerializeField] List<Skill> allSkills = new List<Skill> { Skill.Speed, Skill.Weight, Skill.Push, Skill.Stun, Skill.Shield, Skill.Slow };

    [SerializeField] GameObject stunParticles;
    [SerializeField] GameObject slowParticles;
    [SerializeField] GameObject pushParticles;

    void Start()
    {
        target = FindObjectOfType<Target>().gameObject;
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
                if (!dontMove)
                {
                    ChargeAtTarget();
                }
            }
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
    // @access from Box script
    public void GiveRandomGift()
    {
        // Delay is set from spinner, such as 3 seconds
        Skill randomSkill = allSkills[Random.Range(0, allSkills.Count)];
        StartCoroutine(GiveSkill(randomSkill));
    }

    public void StunBall()
    {
        stunParticles.SetActive(true);
    }

    public void SlowBall()
    {
        slowParticles.SetActive(true);
    }
    #endregion

    #region Private Methods
    void CoughtByHole(Vector3 _holePosition)
    {
        col.enabled = false;
        holePosition = _holePosition;
        disabled = true;
        rb.velocity = Vector3.zero;
    }

    void AddScore()
    {
        Debug.Log("Scored");
    }

    void Reappear()
    {
        disabled = false;
        col.enabled = true;
        idle = true;
        transform.position = new Vector3(Random.Range(-50, 50) + 375, 730, Random.Range(-50, 50));
        transform.localScale = Vector3.one;
    }

    void ChargeAtTarget()
    {
        // Aim at target
        direction = target.transform.position - transform.position;
        // Add small error to the vector to make it look not perfectly aimed
        Vector3 error = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
        direction += error;

        // Choose random speed multiplier like arrows for normal ball
        int speedMultiplier = Random.Range(0, 4);
        speed = baseSpeed * speedMultiplier;
        Debug.Log(speed);
        // Push yourself toward the target with set speed
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }
    #endregion

    #region Coroutines
    IEnumerator GiveSkill(Skill skill)
    {
        yield return new WaitForSeconds(3);
        Debug.Log(skill);
    }
    #endregion
}
