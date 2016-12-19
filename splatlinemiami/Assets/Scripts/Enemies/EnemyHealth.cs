using UnityEngine;
using System.Collections;

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

    // Components
    private Rigidbody2D rb;
    public Sprite deadSprite;
    private SpriteRenderer sr;
    private CameraShaking shake;
    private BloodSplatter currentBloodSplatterInstance;
    public GameObject bloodBurst;
    private EnemyMoveController movecontroller;

    // Sounds
    public AudioClip[] deathSounds;
    public AudioClip[] hurtSounds;


    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        invincibilityTime = 0.25f;
        isInvincible = false;
        rb = GetComponent<Rigidbody2D>();
        shake = FindObjectOfType<CameraShaking>();
        movecontroller = GetComponent<EnemyMoveController>();
    }

    void Update()
    {
        // Check for invincibility
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                invincibilityTimer = invincibilityTime;
            }
        }

    }

    // TODO: Take in account of invincibility
    public void TakeDamage(int damage, float bloodScale)
    {
        if(!isInvincible)
        {
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

            isInvincible = true;
        }
    }

    public void Death()
    {
        if(deathSounds != null)
        {
            AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0, deathSounds.Length)], Camera.main.transform.position, .8f);
        }
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
}