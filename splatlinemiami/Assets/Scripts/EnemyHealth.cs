using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int moneyValue;
    public GameObject[] bloodList;

    private float invincibilityTime;
    private float invincibilityTimer;
    public bool isInvincible;

    private Rigidbody2D rb;
    public Sprite deadSprite;
    private SpriteRenderer sr;
    private CameraShake shake;
    public BloodSplatter currentBloodSplatterInstance;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        invincibilityTime = 0.25f;
        isInvincible = false;
        rb = GetComponent<Rigidbody2D>();
        shake = FindObjectOfType<CameraShake>();
        sr = GetComponent<SpriteRenderer>();
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
    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }

            SplatterBlood();
            //shake.ShakeCamera(10f, .2f);

            isInvincible = true;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void SplatterBlood()
    {
        GameObject blood = bloodList[Random.Range(0, bloodList.Length)];
        GameObject bloodInstance = (GameObject)Instantiate(blood, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
        GameManager.Instance.CheckBloodTiles();
    }

    public void CalculateKnockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        rb.AddForce(knockbackDirection * knockbackStrength);
    }
}