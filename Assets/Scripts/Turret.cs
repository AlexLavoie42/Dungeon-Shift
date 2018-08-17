using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    public GameObject ball;
    Rigidbody2D player;
    float distanceToPlayer;
    bool sighted;
    public float forgetDistance;
    bool timerStarted;
    Quaternion startingRotation;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        startingRotation = transform.rotation;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (PlayerSpot())
        {
            Vector3 playerLocation = player.transform.position;

            transform.rotation = Quaternion.LookRotation(transform.forward, playerLocation - transform.position);

            if (!timerStarted)
            {
                StartCoroutine(ShootTimer(3f));
            }

            PlayerSpot();
        }
        else
        {
            transform.rotation = startingRotation;
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
        if (collider.gameObject.tag == "Player")
        {
            sighted = true;
        }
    }

    IEnumerator ShootTimer(float time)
    {
        timerStarted = true;
        Instantiate(ball, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        timerStarted = false;
    }
}
