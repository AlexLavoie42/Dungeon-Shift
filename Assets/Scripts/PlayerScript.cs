using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public bool canMove = true;
    Rigidbody2D player;
    Animator playerAnim;
    SpriteRenderer playerSprite;
    public string weaponType;
    public GameObject dagger;
    public GameObject bullet;
    public GameObject fire;
    public GameObject rifleBullet;
    public float daggerShootTimer;
    public float bulletShootTimer;
    public float fireShootTimer;
    public float rifleShootTimer;
    public float daggerDamage;
    public float daggerDistance;
    public float daggerSpeed;
    public float bulletDamage;
    public float bulletDistance;
    public float bulletSpeed;
    public float fireDamage;
    public float fireDistance;
    public float fireSpeed;
    public float rifleDamage;
    public float rifleDistance;
    public float rifleSpeed;
    public int extraBulletDamage;
    bool shootTimerStarted;
    public bool canShoot = true;
    public int healthPotions;
    public string currentPotion;
    public GameObject currentPotionItem;
    public int maxHealthPots = 3;
    public float potionTime;
    bool potionTimerStarted;
    public bool canPotion = true;
    public int playerHealth;
    public GameObject endDialogueTrigger;
    public Sprite bossSprite;
    public GameObject endDialogue;
    bool end;
    public float shiftTime;
    public float shiftResetTime;
    public GameObject[] weapons;
    public GameObject[] enemies;
    public GameObject[] trapsAndPotions;
    GameObject[] oldPatrol;
    int oldIndex;
    int index;
    int index2;
    Vector3 enemyPos;
    Vector3 trapsAndPotsPos;
    GameObject newEnemy;
    bool shiftTimerStarted;
    public bool canShift = true;
    public UI UI;

    void OnLevelWasLoaded()
    {
        transform.position = GameObject.Find("StartingPos").transform.position;
        if (Application.loadedLevelName == "5")
        {
            endDialogueTrigger = GameObject.FindGameObjectWithTag("FinalTrigger");
            endDialogue = endDialogueTrigger.transform.GetChild(0).transform.GetChild(0).gameObject;
        }
        UI = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>();
    }

    void Start ()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(transform.gameObject);
        UI = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>();
    }

	void FixedUpdate ()
    {
        if (canMove)
        {
            Move();
        }
        if (canShoot)
        {
            Shoot();
        }
        if (canPotion)
        {
            Potion();
        }
        if (end)
        {
            FinalCutscene();
        }
        Health();
        if (canShift)
        {
            Shift();
        }
    }

    void Move()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            player.MovePosition(transform.position + Vector3.up * speed);
        }

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            player.MovePosition(transform.position + (Vector3.up * -1) * speed);
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            player.MovePosition(transform.position + Vector3.right * speed);
        }

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            player.MovePosition(transform.position + (Vector3.right * -1) * speed);
        }
        if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 1)
        {
            player.MovePosition(transform.position + (Vector3.up + Vector3.right) * speed / 1.3f);
        }
        if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == -1)
        {
            player.MovePosition(transform.position + (Vector3.up + (Vector3.right * -1)) * speed / 1.3f);
        }
        if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == -1)
        {
            player.MovePosition(transform.position + ((Vector3.up * -1) + (Vector3.right * -1)) * speed / 1.3f);
        }
        if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 1)
        {
            player.MovePosition(transform.position + ((Vector3.up * -1) + Vector3.right) * speed / 1.3f);
        }
    }

    void Shoot()
    {
        if (playerAnim != null)
        {
            if (weaponType == "Dagger")
            {
                playerAnim.SetInteger("WeaponNum", 0);
                if (Input.GetMouseButton(0))
                {
                    if (!shootTimerStarted)
                    {
                        StartCoroutine(ShootTimer(daggerShootTimer, dagger, daggerDamage, daggerSpeed, daggerDistance));
                    }
                }
            }

            if (weaponType == "Bullet")
            {
                playerAnim.SetInteger("WeaponNum", 1);
                if (Input.GetMouseButton(0))
                {
                    if (!shootTimerStarted)
                    {
                        StartCoroutine(ShootTimer(bulletShootTimer, bullet, bulletDamage, bulletSpeed, bulletDistance));
                    }
                }
            }

            if (weaponType == "Fireball")
            {
                playerAnim.SetInteger("WeaponNum", 2);
                if (Input.GetMouseButton(0))
                {
                    if (!shootTimerStarted)
                    {
                        StartCoroutine(ShootTimer(fireShootTimer, fire, fireDamage, fireSpeed, fireDistance));
                    }
                }
            }

            if (weaponType == "Rifle")
            {
                playerAnim.SetInteger("WeaponNum", 3);
                if (Input.GetMouseButton(0))
                {
                    if (!shootTimerStarted)
                    {
                        StartCoroutine(ShootTimer(rifleShootTimer, rifleBullet, rifleDamage, rifleSpeed, rifleDistance));
                    }
                }
            }
        }
    }

    void Potion()
    {
        if (Input.GetButtonDown("ConsumeHealthPot"))
        {
            if (healthPotions > 0 && playerHealth < 100)
            {
                ConsumeHealthPot();
            }
        }
        if (Input.GetButtonDown("ConsumePotion"))
        {
            ConsumeOtherPot();
        }
    }

    void ConsumeHealthPot()
    {
        playerHealth += 25;
        healthPotions--;
    }

    void ConsumeOtherPot()
    {
        switch (currentPotion)
        {
            case "DamagePotion":
                if (!potionTimerStarted)
                {
                    StartCoroutine(DamageTimer(potionTime));
                    currentPotion = "";
                }
                break;
            case "SpeedPotion":
                if (!potionTimerStarted)
                {
                    StartCoroutine(SpeedTimer(potionTime));
                    currentPotion = "";
                }
                break;
        }
    }

    public void Damage(int damage)
    {
        playerHealth -= damage;
        if (!UI.outlineTimer)
        {
            UI.StartCoroutine(UI.DamageOutline());
        }
    }

    void Death()
    {
        Application.LoadLevel("StartScreen");
        Destroy(gameObject);
    }

    public void BossDeath()
    {
        gameObject.GetComponent<PlayerScript>().canMove = false;
        endDialogueTrigger.GetComponent<Trigger>().canTrigger = true;
        end = true;
        Destroy(gameObject.GetComponent<Animator>());
    }

    void FinalCutscene()
    {
        if (endDialogue.GetComponent<Dialogue>().index == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bossSprite;
        }
        if (endDialogue.GetComponent<Dialogue>().dialogueDone)
        {
            Application.LoadLevel("EndScreen");
        }
    }

    void Health()
    {
        if (playerHealth <= 0)
        {
            Death();
        }
        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
    }

    void ShapeShift()
    {
        GameObject[] enemiesInLevel;
        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy");
        while (index < enemiesInLevel.Length)
        {
            enemyPos = enemiesInLevel[index].transform.position;
            oldPatrol = enemiesInLevel[index].GetComponent<EnemyAI>().patrol;
            oldIndex = enemiesInLevel[index].GetComponent<EnemyAI>().index;
            Destroy(enemiesInLevel[index]);

            newEnemy = (GameObject)Instantiate(enemies[Random.Range(0, enemies.Length)], enemyPos, enemiesInLevel[index].transform.rotation);
            newEnemy.GetComponent<EnemyAI>().patrol = oldPatrol;
            newEnemy.GetComponent<EnemyAI>().index = oldIndex;
            index++;
        }
        index = 0;

        GameObject[] trapsAndPotionsInLevel;
        trapsAndPotionsInLevel = GameObject.FindGameObjectsWithTag("TrapsAndPotions");

        while (index2 < trapsAndPotionsInLevel.Length)
        {
            trapsAndPotsPos = trapsAndPotionsInLevel[index2].transform.position;
            Destroy(trapsAndPotionsInLevel[index2]);

            Instantiate(trapsAndPotions[Random.Range(0, trapsAndPotions.Length)], trapsAndPotsPos, new Quaternion(0, 0, 0, 0));
            index2++;
        }
        index2 = 0;


        weaponType = weapons[Random.Range(0, weapons.Length)].name;
    }

    void Shift()
    {
        if (!shiftTimerStarted && shiftTime < 100)
        {
            StartCoroutine(ShiftResetTimer(shiftResetTime));
        }

        if (Input.GetButtonDown("DungeonShift"))
        {
            if (shiftTime == 100)
            {
                shiftTime = 0;
                ShapeShift();
            }
        }
        if (shiftTime > 100)
        {
            shiftTime = 100;
        }
    }

    IEnumerator ShootTimer(float time, GameObject weapon, float damage, float speed, float distance)
    {
        shootTimerStarted = true;
        playerAnim.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.2f);
        GameObject shoot = (GameObject)Instantiate(weapon, transform.position + (transform.right * 0.4f), transform.rotation);
        shoot.GetComponent<WeaponBase>().damage = (int)damage;
        shoot.GetComponent<WeaponBase>().weaponSpeed = speed;
        shoot.GetComponent<WeaponBase>().weaponTime = distance;
        shoot.GetComponent<WeaponBase>().damage += extraBulletDamage;
        yield return new WaitForSeconds(time);
        playerAnim.SetBool("Shooting", false);
        shootTimerStarted = false;
    }

    IEnumerator DamageTimer(float time)
    {
        potionTimerStarted = true;
        extraBulletDamage = 10;
        yield return new WaitForSeconds(time);
        extraBulletDamage = 0;
        potionTimerStarted = false;
    }

    IEnumerator SpeedTimer(float time)
    {
        potionTimerStarted = true;
        gameObject.GetComponent<PlayerScript>().speed += 0.1f;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<PlayerScript>().speed -= 0.1f;
        potionTimerStarted = false;
    }

    IEnumerator ShiftResetTimer(float time)
    {
        shiftTimerStarted = true;
        yield return new WaitForSeconds(time);
        shiftTime++;
        shiftTimerStarted = false;

    }
}
