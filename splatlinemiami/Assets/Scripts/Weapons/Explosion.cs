﻿using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DealDamage()
    {
        // Look for all enemies in collider

        // Deal damage to all enemies
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();

        if(health != null)
        {
            health.TakeDamage(4, 2f);
        }
    }
}
