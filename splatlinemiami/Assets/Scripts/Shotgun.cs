using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {

    public int bulletsPerShot;
    public int spreadRange;

    public override void Shoot()
    {
        // Play the shooting sound effect
        if (shootSound)
            AudioSource.PlayClipAtPoint(shootSound, transform.position);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Convert the angle of the player to the velocity of the bullet and shoot it forward
            GameObject currentBullet = (GameObject)Instantiate(bullet.gameObject, transform.position, GetComponentInParent<Transform>().transform.rotation);
            Vector2 angle = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + Random.Range(spreadRange * -1, spreadRange), Vector3.forward) * Vector3.up;
            currentBullet.GetComponent<Rigidbody2D>().velocity = angle * currentBullet.GetComponent<Bullet>().bulletSpeed;
        }
    }

}
