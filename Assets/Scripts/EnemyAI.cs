using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    
    Rigidbody2D player;
    public int enemyHealth;
    public float enemySpeed;
    float distanceToPlayer;
    public float hitTime;
    public int damage;
    bool sighted;
    public int shiftAmount;
    public float forgetDistance;
    public GameObject[] patrol;
    public int index = 1;
    PlayerScript playerScript;
    bool timerStarted;
    Animator enemyAnimation;
    


    void Start ()
    {
        if(patrol[0] == null)
        {
            patrol[0] = (GameObject)Instantiate(new GameObject(), transform.position, transform.rotation);
        }
        enemyAnimation = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            if (playerScript.shiftTime < 100)
            {
                playerScript.shiftTime += shiftAmount;
            }
            
        }
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (PlayerSpot())
        {
            Vector3 playerLocation = player.transform.position;

            transform.rotation = Quaternion.LookRotation(transform.forward, playerLocation - transform.position);

            transform.position = Vector3.MoveTowards(transform.position, playerLocation, enemySpeed);

            PlayerSpot();
        }

        if (!sighted)
        {
            if (index < patrol.Length)
            {
                if (transform.position != patrol[index].transform.position)
                {
                    
                    transform.rotation = Quaternion.LookRotation(transform.forward, patrol[index].transform.position - transform.position);
                    
                    transform.position = Vector3.MoveTowards(transform.position, patrol[index].transform.position, enemySpeed);
                }
                else
                {
                    index++;
                }

            }else
            {
                index = 0;
            }
        }

    }

    bool PlayerSpot()
    {
        if (sighted)
        {
            if (distanceToPlayer < forgetDistance)
            {
                return true;
            }
            else
            {
                sighted = false;
                return false;
            }
        }
        else
        {
            return false;
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            
            sighted = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && sighted)
        {
            collider.GetComponent<EnemyAI>().sighted = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Weapon")
        {
            enemyHealth -= collider.gameObject.GetComponent<WeaponBase>().damage;
            Destroy(collider.gameObject);
            sighted = true;
        }
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!timerStarted)
            {
                StartCoroutine(DamageTimer(hitTime));
            }
        }
    }

    IEnumerator DamageTimer(float time)
    {
        timerStarted = true;
        enemyAnimation.SetBool("IsAttacking", true);
        playerScript.Damage(damage);
        yield return new WaitForSeconds(time);
        enemyAnimation.SetBool("IsAttacking", false);
        timerStarted = false;

    }

}
