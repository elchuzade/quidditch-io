using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class Bot : Ball
{
    public bool dontMove; // For chanllenge levels where bot should not move by itself

    // Vector from your position to target position
    Vector3 direction;

    public List<Skill> allSkills = new List<Skill> { Skill.Speed, Skill.Weight, Skill.Push, Skill.Shield, Skill.Slow };

    void Awake()
    {
        levelStatus = FindObjectOfType<LevelStatus>();
    }

    void Start()
    {
        initialPosition = transform.position;

        SetInitialDebuffs();
        SetInitialSkills();
        FindAllPushables();

        GiveRandomGift();
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
                    if (levelStatus.selectedMode == "push")
                    {
                        ChargeAtBall();
                    } else
                    {
                        ChargeAtTarget();
                    }
                }
            }
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
    #endregion

    #region Private Methods
    void Charge()
    {
        // Add small error to the vector to make it look not perfectly aimed
        Vector3 error = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
        direction += error;

        // Choose random speed multiplier like arrows for normal ball
        int speedMultiplier = Random.Range(0, 4);
        speed = baseSpeed * speedMultiplier * debuffSpeed * skillSpeed + weightSkillSpeed;

        // Push yourself toward the target with set speed
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    void ChargeAtBall()
    {
        Ball randomBall = botsAndPlayer[Random.Range(0, botsAndPlayer.Length)];
        // Aim at random target
        direction = randomBall.transform.position - transform.position;

        Charge();
    }

    void ChargeAtTarget()
    {
        Target randomTarget = targets[Random.Range(0, targets.Length)];
        // Aim at random target
        direction = randomTarget.transform.position - transform.position;

        Charge();
    }
    #endregion

    #region Coroutines
    IEnumerator GiveSkill(Skill skill)
    {
        yield return new WaitForSeconds(3);

        SetGivenSkill(skill);
    }
    #endregion
}
