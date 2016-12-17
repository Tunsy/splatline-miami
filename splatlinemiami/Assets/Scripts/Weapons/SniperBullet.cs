using UnityEngine;
using System.Collections;

public class SniperBullet : Bullet
{
    public int penetrationAmount;

    public void Update()
    {
        rb.velocity = angle * bulletSpeed;
    }

    public override void OnTriggerEnter2D(Collider2D other)
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
            penetrationAmount--;
        }

        if(penetrationAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
