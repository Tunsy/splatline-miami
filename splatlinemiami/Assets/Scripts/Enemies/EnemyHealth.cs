﻿using UnityEngine;
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
    public BloodSplatter currentBloodSplatterInstance;
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
    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;

            if (hurtSounds != null)
            {
                AudioSource.PlayClipAtPoint(hurtSounds[Random.Range(0, hurtSounds.Length)], Camera.main.transform.position, .2f);
            }

            if (currentHealth <= 0)
            {
                Death();
            }

            SplatterBlood();
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
        movecontroller.Knockback(knockbackDirection, knockbackStrength);
    }
}