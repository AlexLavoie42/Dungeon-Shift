using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Open");

    }

}
