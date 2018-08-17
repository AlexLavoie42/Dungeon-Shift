using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

    int i;

    void Start () {
        i = Application.loadedLevel;
    }

	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Application.LoadLevel(i + 1);
        }
    }
}
