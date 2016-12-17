using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
    public Vector2 angle;
    public float bulletSpeed;
    public float knockbackStrength;
    private float timer;
    private float despawnTime;
    protected Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
        despawnTime = 4;
        angle = rb.velocity.normalized;
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

    public virtual void OnTriggerEnter2D(Collider2D other)
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
            health.CalculateKnockback(GetComponent<Rigidbody2D>().velocity, knockbackStrength);

            Destroy(gameObject);
        }
    }
}
