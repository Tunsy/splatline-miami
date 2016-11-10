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
    private bool isInvincible;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        invincibilityTime = 0.25f;
        isInvincible = false;
        rb = GetComponent<Rigidbody2D>();
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
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }

        SplatterBlood();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void SplatterBlood()
    {
        if (!isInvincible)
        {
            GameObject blood = bloodList[Random.Range(0, bloodList.Length)];
            Instantiate(blood, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
        }

    }

    public void CalculateKnockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        rb.AddForce(knockbackDirection * knockbackStrength);
    }
}