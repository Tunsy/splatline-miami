using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Bullet bullet;
    public float fireCooldown;
    public AudioClip shootSound;
    public int maxBulletCount;
    private int currentBulletCount;
    private bool isReloading;


    public void Start()
    {
        currentBulletCount = maxBulletCount;
    }

    public virtual void Shoot()
    {
        if (!isReloading)
        {
            // Play the shooting sound effect
            if (shootSound)
                AudioSource.PlayClipAtPoint(shootSound, transform.position);

            // Convert the angle of the player to the velocity of the bullet and shoot it forward
            GameObject currentBullet = (GameObject)Instantiate(bullet.gameObject, transform.position, GetComponentInParent<Transform>().transform.rotation);
            Vector2 angle = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + Random.Range(-3f, 3f), Vector3.forward) * Vector3.up;
            currentBullet.GetComponent<Rigidbody2D>().velocity = angle * currentBullet.GetComponent<Bullet>().bulletSpeed;

            // Reload if ammo count is 0
            currentBulletCount--;
            if (currentBulletCount <= 0)
            {
                Reload();
            }
        }
    }

    public void Reload()
    {
        //Start a timer

        //Play a reload sound

        //Restart bullet count

        //Be able to shoot again after timer ends
    }
}
