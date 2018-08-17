using UnityEngine;
using System.Collections;

public class Potions : MonoBehaviour {

    public string potionType;
    PlayerScript handler;
    GameObject potion;

    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        potion = gameObject;
    }

    void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.tag == "Player")
        {

            if (potionType == "HealthPotion")
            {
                if (handler.healthPotions < handler.maxHealthPots)
                {
                    handler.healthPotions++;
                    Destroy(gameObject);
                }
            }else
            {
                if(handler.currentPotion == "")
                {
                    handler.currentPotionItem = potion;
                    handler.currentPotion = potionType;
                    gameObject.SetActive(false);
                }
                else if(Input.GetButtonDown("DropPotion"))
                {
                    handler.currentPotionItem.SetActive(true);
                    handler.currentPotionItem.transform.position = transform.position;
                    handler.currentPotion = potionType;
                    handler.currentPotionItem = potion;
                    gameObject.SetActive(false);
                }
            }

        }
    }

}
