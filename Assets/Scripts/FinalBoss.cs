using UnityEngine;
using System.Collections;

public class FinalBoss : MonoBehaviour {

    GameObject player;
    PlayerScript playerScript;
    public Dialogue dialogue;
    public int fightMove;
    bool fightStarted;
    public float bossSpeed;
    public int bossHealth;
    bool timerStarted;
    public float bossHitTime;
    public int bossHitDamage;
    public GameObject turretBall;
    public GameObject trigger;
    bool ballTimerStarted;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
	}
	
	void Update () {
        if (dialogue.dialogueDone && !fightStarted)
        {
            StartCoroutine(BossFight());
        }
        if (fightStarted)
        {
            playerScript.shiftTime = 0;
        }
	}

    void FixedUpdate()
    {
        if (bossHealth <= 0)
        {
            gameObject.SetActive(false);
            playerScript.BossDeath();
        }
        if (fightMove == 1 || fightMove == 2 || fightMove == 5)
        {
            Vector3 playerLocation = player.transform.position;

            transform.rotation = Quaternion.LookRotation(transform.forward, playerLocation - transform.position);

            transform.position = Vector3.MoveTowards(transform.position, playerLocation, bossSpeed);
        }
        if(fightMove == 2)
        {
            if (!ballTimerStarted)
            {
                StartCoroutine(BallLaunch(1));
            }
        }
        if(fightMove == 3)
        {
            trigger.GetComponent<Trigger>().canTrigger = true;
            if (!ballTimerStarted)
            {
                StartCoroutine(BallLaunch(0.4f));
            }
        }
        else
        {
            trigger.GetComponent<Trigger>().canTrigger = false;
        }
        if(fightMove == 4)
        {
            if (!ballTimerStarted)
            {
                StartCoroutine(BallLaunch(0.4f));
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Weapon" && fightStarted)
        {
            bossHealth -= collider.gameObject.GetComponent<WeaponBase>().damage;
            Destroy(collider.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" && fightStarted)
        {
            if (!timerStarted)
            {
                StartCoroutine(DamageTimer(bossHitTime));
            }
        }
    }

    IEnumerator DamageTimer(float time)
    {
        timerStarted = true;
        playerScript.Damage(bossHitDamage);
        yield return new WaitForSeconds(time);
        timerStarted = false;

    }

    IEnumerator BossFight()
    {
        fightStarted = true;
        fightMove = 1;
        yield return new WaitForSeconds(15);
        fightMove = 2;
        yield return new WaitForSeconds(10);
        fightMove = 3;
        yield return new WaitForSeconds(15);
        playerScript.speed -= 0.04f;
        fightMove = 4;
        yield return new WaitForSeconds(5);
        playerScript.speed += 0.04f;
        fightMove = 5;
        playerScript.extraBulletDamage -= 5;
        trigger.GetComponent<Trigger>().DoTrigger();
        yield return new WaitForSeconds(10);
        fightStarted = false;
    }

    IEnumerator BallLaunch(float time)
    {
        ballTimerStarted = true;
        Instantiate(turretBall, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        ballTimerStarted = false;
    }
}
