using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public int moneyValue;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}

    public void TakeDamage(int damage)
    {

    }

    public void Death()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
