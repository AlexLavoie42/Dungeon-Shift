using UnityEngine;
using System.Collections;

public class TurretShootHandler : MonoBehaviour {

    GameObject player;
    Rigidbody2D ball;
    public float speed;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ball = gameObject.GetComponent<Rigidbody2D>();

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        ball.velocity = direction * speed;

        StartCoroutine(Timer(4f));
	}
	
	void Update () {

	}

    IEnumerator Timer(float weaponTime)
    {
        yield return new WaitForSeconds(weaponTime);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<PlayerScript>().Damage(15);
        }
    }
}
