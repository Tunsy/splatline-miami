using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
    public float bulletSpeed;
    public float knockbackStrength;
    private float timer;
    private float despawnTime;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
        despawnTime = 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Destroy the bullet after despawnTime has passed
        timer += Time.deltaTime;
        if(timer > despawnTime)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            //Take Damage
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            health.TakeDamage(damage);
            health.CalculateKnockback(GetComponent<Rigidbody2D>().velocity, 1);

            Destroy(gameObject);
        }
    }
}
