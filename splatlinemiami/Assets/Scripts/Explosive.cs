using UnityEngine;
using System.Collections;

public class Explosive : Bullet {

    Explosion explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            // Instantiate an explosion
            Explosion currentExplosion = (Explosion) Instantiate(explosion, transform.position, Quaternion.identity);
            explosion.DealDamage();
        }
    }
}
