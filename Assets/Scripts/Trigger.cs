using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{

    public bool canTrigger;
    int index;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            canTrigger = true;
        }
    }

    void Start()
    {

    }


    void Update()
    {
        if (canTrigger)
        {
            DoTrigger();
        }
        else
        {
            index = 0;
        }
    }

    public void DoTrigger()
    {
        while (index < transform.childCount)
        {
            if (transform.GetChild(index).gameObject.activeSelf == true)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            else if (transform.GetChild(index).gameObject.activeSelf == false)
            {
                transform.GetChild(index).gameObject.SetActive(true);
            }
            index++;
        }
    }
}
