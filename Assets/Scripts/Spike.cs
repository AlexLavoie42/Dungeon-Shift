using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

    bool timerStarted;
    bool timerStarted2;
    bool spikeUp;
    GameObject player;

    void Update()
    {
        if (!timerStarted)
        {
            StartCoroutine(SpikeTimer(3f));
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player" && spikeUp)
        {
            player = collider.gameObject;
            if (!timerStarted2)
            {
                StartCoroutine(DamageTimer(0.4f));
            }
        }
    }

    IEnumerator SpikeTimer(float time)
    {
        timerStarted = true;
        spikeUp = true;
        yield return new WaitForSeconds(time);
        spikeUp = false;
        yield return new WaitForSeconds(time);
        timerStarted = false;
    }

    IEnumerator DamageTimer(float time)
    {
        timerStarted2 = true;
        player.GetComponent<PlayerScript>().Damage(5);
        yield return new WaitForSeconds(time);
        timerStarted2 = false;

    }
}
