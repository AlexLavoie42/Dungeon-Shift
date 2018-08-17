using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI : MonoBehaviour {
    public Text healthDisplay;
    public Text shiftDisplay;
    public Text healthPotDisplay;
    public Text currentPotDisplay;
    GameObject player;
    PlayerScript playerScript;
    GameObject damageUI;
    GameObject speedPotUI;
    GameObject damagePotUI;
    public bool outlineTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        healthDisplay = GameObject.Find("HealthDisplay").GetComponent<Text>();
        shiftDisplay = GameObject.Find("ShiftDisplay").GetComponent<Text>();
        healthPotDisplay = GameObject.Find("HealthPots").GetComponent<Text>();
        currentPotDisplay = GameObject.Find("CurrentPot").GetComponent<Text>();
        damagePotUI = GameObject.Find("DamagePotionOutline");
        speedPotUI = GameObject.Find("SpeedOutline");
        damageUI = GameObject.Find("DamageOutline");
    }

    void Update()
    {
        transform.position = (player.transform.position - new Vector3(0, 0, 10));

        currentPotDisplay.text = ("Current Potion: " + playerScript.currentPotion);
        healthDisplay.text = ("Health: " + playerScript.playerHealth.ToString());
        shiftDisplay.text = ("Shift:" + playerScript.shiftTime.ToString());
        healthPotDisplay.text = ("Health Potions:" + playerScript.healthPotions);
    }

    public IEnumerator DamageOutline()
    {
        outlineTimer = true;
        damageUI.SetActive(true);
        yield return new WaitForSeconds(0.12f);
        //damageUI.SetActive(false);
        outlineTimer = false;
    }

    public void DamagePotionOutline(bool start)
    {
        
    }

    public void SpeedPotionOutline(bool start)
    {

    }
}
