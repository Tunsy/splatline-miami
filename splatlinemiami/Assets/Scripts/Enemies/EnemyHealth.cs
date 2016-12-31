using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    // Health
    public int maxHealth;
    public int currentHealth;
    public int moneyValue;
    public GameObject[] bloodList;

    // Invincibility
    private float invincibilityTime;
    private float invincibilityTimer;
    public bool isInvincible;
    private bool blink;

    // Components
    private Rigidbody2D rb;
    public Sprite deadSprite;
    private SpriteRenderer sr;
    private CameraShaking shake;
    private BloodSplatter currentBloodSplatterInstance;
    public GameObject bloodBurst;
    private EnemyMoveController movecontroller;

    // Item drops
    public int dropChance;
    public GameObject itemDrop;

    // Sounds
    public AudioClip[] deathSounds;
    public AudioClip[] hurtSounds;


    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        invincibilityTime = 0.3f;
        invincibilityTimer = invincibilityTime;
        isInvincible = false;
        rb = GetComponent<Rigidbody2D>();
        shake = FindObjectOfType<CameraShaking>();
        movecontroller = GetComponent<EnemyMoveController>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check for invincibility
        if (isInvincible)
        {
            // Update invincibility timer
            invincibilityTimer -= Time.deltaTime;

            // Check for invincibility flig
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                invincibilityTimer = invincibilityTime;
                sr.enabled = true;
                sr.color = Color.white;
            }
            else
            {
                // Sprite blinks and turns red if invincible
                blink = !blink;
                sr.enabled = blink;
                sr.color = Color.red;
            }


        }

    }

    // TODO: Take in account of invincibility
    public void TakeDamage(int damage, float bloodScale)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            currentHealth -= damage;

            // Play sound
            if (hurtSounds != null)
            {
                AudioSource.PlayClipAtPoint(hurtSounds[Random.Range(0, hurtSounds.Length)], Camera.main.transform.position, .2f);
            }

            // Check for health. Splatter blood and shake camera for juiciness
            if (currentHealth <= 0)
            {
                Death();
                SplatterBlood(bloodScale * 1.7f);
            }
            else
            {
                SplatterBlood(bloodScale);
            }
            shake.Shake(.1f, .15f);


        }
    }

    public void Death()
    {
        if (deathSounds != null)
        {
            AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0, deathSounds.Length)], Camera.main.transform.position, .8f);
        }
        DropItem();
        Instantiate(bloodBurst, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SplatterBlood(float bloodScale)
    {
        GameObject blood = bloodList[Random.Range(0, bloodList.Length)];
        blood.transform.localScale = new Vector2(bloodScale, bloodScale);
        GameObject bloodInstance = (GameObject)Instantiate(blood, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
        GameManager.Instance.CheckBloodTiles();
    }

    public void CalculateKnockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        movecontroller.Knockback(knockbackDirection, knockbackStrength);
    }

    void DropItem()
    {
        if(itemDrop != null)
        {
            if (Random.Range(0, 100) < dropChance)
            {
                Instantiate(itemDrop, transform.position, Quaternion.identity);
            }
        }
    }
}