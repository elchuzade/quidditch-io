using System.Collections;
using UnityEngine;
using static GlobalVariables;

public class Ball : MonoBehaviour
{
    public Vector3 initialPosition;

    public Rigidbody rb;
    public SphereCollider col;

    // Proportional to baseSpeed and initial mass of 10 = 2000, 20 = 4000. will be added when weight skill is on
    public float weightSkillSpeed = 0;
    public float weightSkillMass = 20;
    public float debuffSpeed = 1;
    public float skillSpeed = 1;
    public float pushPower = 100;
    public float pushRadius = 1000;

    public float slowDebuffDuration = 10;
    public float stunDebuffDuration = 5;

    public float slowSkillDuration = 10;
    public float stunSkillDuration = 10;
    public float speedSkillDuration = 10;
    public float weightSkillDuration = 10;
    public float shieldSkillDuration = 10;
    public float pushSkillDuration = 10;

    public float baseSpeed = 2000;
    public float speed = 0;
    // When score increases, the multiplier increases too
    public float baseSpeedStep = 2000;
    public float massStep = 50;

    // This is not to wait until the full stop
    public float almostStopped = 100f;

    // When you are sucked in to the hole disable controls
    public bool disabled = false;

    // To allow aiming
    public bool idle = true;

    // Distance to which the player needs to approach the hole center to reappear
    public float holeCenterMargin = 10;
    public Vector3 holePosition;
    public float holeSuckSpeed = 2;

    // Debuffs
    [Header("Debuff")]
    public GameObject stunDebuffParticles;
    public GameObject slowDebuffParticles;

    // Skills
    [Header("Skills")]
    public GameObject pushSkillParticles;
    public GameObject slowSkillParticles;
    public GameObject stunSkillParticles;
    public GameObject speedSkillParticles;
    public GameObject weightSkillParticles;
    public GameObject shieldSkillParticles;

    // These are skills that a ball has as an attack
    public bool slowSkillStatus;
    public bool stunSkillStatus;
    public bool weightSkillStatus;
    public bool speedSkillStatus;
    public bool pushSkillStatus;
    public bool shieldSkillStatus;

    // These are affects that have been added to current ball by another ball
    public bool slowDebuffStatus;
    public bool stunDebuffStatus;

    public Target target;
    public Ball[] botsAndPlayer;
    public Barrier[] barriers;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            CoughtByHole(other.gameObject.transform.position);
        }
    }

    #region Public Methods
    public void Push(Vector3 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode.Impulse);
    }

    public void FindAllPushables()
    {
        target = FindObjectOfType<Target>();
        botsAndPlayer = FindObjectsOfType<Ball>();
        barriers = FindObjectsOfType<Barrier>();
    }

    // Run from update of playerball and bot scripts
    public void PushEverything()
    {
        if (pushSkillStatus)
        {
            StartCoroutine(PerformPushAction());
        }
    }

    // Runs on start methods in playerball and bot scripts
    public void SetInitialDebuffs()
    {
        if (slowDebuffStatus)
        {
            slowDebuffParticles.SetActive(true);
        }
        if (stunDebuffStatus)
        {
            stunDebuffParticles.SetActive(true);
        }
    }

    // Runs on start methods in playerball and bot scripts
    public void SetInitialSkills()
    {
        if (slowSkillStatus)
        {
            slowSkillParticles.SetActive(true);
        }
        if (stunSkillStatus)
        {
            stunSkillParticles.SetActive(true);
        }
        if (weightSkillStatus)
        {
            weightSkillParticles.SetActive(true);
        }
        if (speedSkillStatus)
        {
            speedSkillParticles.SetActive(true);
        }
        if (pushSkillStatus)
        {
            pushSkillParticles.SetActive(true);
            PushEverything();
        }
        if (shieldSkillStatus)
        {
            shieldSkillParticles.SetActive(true);
        }
    }

    // After spinner gave some skill
    public void SetGivenSkill(Skill skill)
    {
        switch (skill)
        {
            case Skill.Weight:
                weightSkillParticles.SetActive(true);
                StartCoroutine(StopWeightSkill(weightSkillDuration));
                break;
            case Skill.Speed:
                speedSkillParticles.SetActive(true);
                StartCoroutine(StopSpeedSkill(speedSkillDuration));
                break;
            case Skill.Stun:
                stunSkillParticles.SetActive(true);
                StartCoroutine(StopStunSkill(stunSkillDuration));
                break;
            case Skill.Push:
                pushSkillParticles.SetActive(true);
                PushEverything();
                StartCoroutine(StopPushSkill(pushSkillDuration));
                break;
            case Skill.Shield:
                shieldSkillParticles.SetActive(true);
                StartCoroutine(StopShieldSkill(shieldSkillDuration));
                break;
            case Skill.Slow:
                slowSkillParticles.SetActive(true);
                StartCoroutine(StopSlowSkill(slowSkillDuration));
                break;
        }
    }

    // Runs from skill script when it hits you with stun
    public void StunBall()
    {
        stunDebuffParticles.SetActive(true);
        StartCoroutine(StopStunDebuff(stunSkillDuration));
    }

    // Runs from skill script when it hits you with slow
    public void SlowBall()
    {
        slowDebuffParticles.SetActive(true);
        StartCoroutine(StopSlowDebuff(slowSkillDuration));
    }

    public void AddScore()
    {
        Debug.Log("Scored");
    }

    public void Reappear()
    {
        disabled = false;
        col.enabled = true;
        idle = true;
        transform.position = initialPosition;
        transform.localScale = Vector3.one;
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
    #endregion

    #region Coroutine
    IEnumerator StopPushSkill(float duration)
    {
        pushSkillStatus = true;

        yield return new WaitForSeconds(duration);

        pushSkillStatus = false;
        pushSkillParticles.SetActive(false);
    }

    IEnumerator StopShieldSkill(float duration)
    {
        shieldSkillStatus = true;
        yield return new WaitForSeconds(duration);

        shieldSkillStatus = false;
        shieldSkillParticles.SetActive(false);
    }

    IEnumerator StopSpeedSkill(float duration)
    {
        skillSpeed = 2;
        speedSkillStatus = true;

        yield return new WaitForSeconds(duration);

        skillSpeed = 1;
        speedSkillStatus = false;

        speedSkillParticles.SetActive(false);
    }

    IEnumerator StopWeightSkill(float duration)
    {
        rb.mass += weightSkillMass;
        weightSkillSpeed = 4000;
        weightSkillStatus = true;

        yield return new WaitForSeconds(duration);

        rb.mass -= weightSkillMass;
        weightSkillSpeed = 0;
        weightSkillStatus = false;

        weightSkillParticles.SetActive(false);
    }

    IEnumerator StopSlowSkill(float duration)
    {
        slowSkillStatus = true;

        yield return new WaitForSeconds(duration);

        slowSkillStatus = false;

        slowSkillParticles.SetActive(false);
    }

    IEnumerator StopStunSkill(float duration)
    {
        stunSkillStatus = true;

        yield return new WaitForSeconds(duration);

        stunSkillStatus = false;

        stunSkillParticles.SetActive(false);
    }

    IEnumerator StopStunDebuff(float duration)
    {
        debuffSpeed = 0;
        stunDebuffStatus = true;

        yield return new WaitForSeconds(duration);

        debuffSpeed = 1;
        stunDebuffStatus = false;

        stunDebuffParticles.SetActive(false);
    }

    IEnumerator StopSlowDebuff(float duration)
    {
        debuffSpeed = 0.25f;
        slowDebuffStatus = true;

        yield return new WaitForSeconds(duration);

        debuffSpeed = 1;
        slowDebuffStatus = false;

        slowDebuffParticles.SetActive(false);
    }

    IEnumerator PerformPushAction()
    {
        yield return new WaitForSeconds(1);

        if (Vector3.Distance(target.transform.position, transform.position) < pushRadius)
        {
            target.Push((target.transform.position - transform.position).normalized, pushPower);
        }

        for (int i = 0; i < botsAndPlayer.Length; i++)
        {
            if (Vector3.Distance(botsAndPlayer[i].transform.position, transform.position) < pushRadius)
            {
                botsAndPlayer[i].Push(
                    (botsAndPlayer[i].transform.position - transform.position).normalized, pushPower
                );
            }
        }

        for (int i = 0; i < barriers.Length; i++)
        {
            if (barriers[i].barrierType == BarrierTypes.Light)
            {
                if (Vector3.Distance(barriers[i].transform.position, transform.position) < pushRadius)
                {
                    barriers[i].Push(
                        (barriers[i].transform.position - transform.position).normalized, pushPower
                    );
                }
            }
        }

        if (pushSkillStatus)
        {
            StartCoroutine(PerformPushAction());
        }
    }
    #endregion
}
