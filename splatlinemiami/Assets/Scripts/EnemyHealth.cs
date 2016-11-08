using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public int moneyValue;
    public GameObject[] bloodList;

    private float invincibilityTime;
    private float invincibilityTimer;
    private bool isInvincible;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        invincibilityTime = 0.25f;
        isInvincible = false;
	}

    // TODO: Take in account of invincibility
    public void TakeDamage(int damage)
    {

    }

    public void Death()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Check for invincibility
	    if(isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if(invincibilityTimer <= 0)
            {
                isInvincible = false;
                invincibilityTimer = invincibilityTime;
            }
        }
	}

    public void SplatterBlood()
    {
        if(!isInvincible)
        {
            GameObject blood = bloodList[Random.Range(0, bloodList.Length)];
            Instantiate(blood, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }
}
