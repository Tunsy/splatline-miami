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
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable") || other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            // Instantiate an explosion
            Explosion currentExplosion = (Explosion) Instantiate(explosion, transform.position, Quaternion.identity);
            explosion.DealDamage();
        }
    }
}
