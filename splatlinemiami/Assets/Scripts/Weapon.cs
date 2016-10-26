using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Bullet bullet;
    public float fireCooldown;
    public AudioClip shootSound;

    public virtual void Shoot()
    {
        // Play the shooting sound effect
        if(shootSound)
            AudioSource.PlayClipAtPoint(shootSound, transform.position);

        // Convert the angle of the player to the velocity of the bullet and shoot it forward
        GameObject currentBullet = (GameObject)Instantiate(bullet.gameObject, transform.position, GetComponentInParent<Transform>().transform.rotation);
        Vector2 angle = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + Random.Range(-3f, 3f), Vector3.forward) * Vector3.up;
        currentBullet.GetComponent<Rigidbody2D>().velocity = angle * currentBullet.GetComponent<Bullet>().bulletSpeed;
    }
}
