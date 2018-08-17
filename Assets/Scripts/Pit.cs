using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour {
    
	void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerScript>().Damage(1000);
        }
    }
}
