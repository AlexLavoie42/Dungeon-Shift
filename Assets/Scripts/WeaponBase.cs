using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour {

    public Rigidbody2D weapon;
    public float weaponTime;
    public float weaponSpeed;
    public int damage;
    Vector2 direction;
    public bool timerDone = false;

    public void Start()
    {
        weapon = gameObject.GetComponent<Rigidbody2D>();

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position + (transform.right * 0.4f);
        direction.Normalize();
        weapon.velocity = direction * weaponSpeed;
        StartCoroutine(Timer());

    }

    public void Update()
    {

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(weaponTime);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }
    }
	
}
