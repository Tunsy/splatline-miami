using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public Bullet bullet;
    public float fireCooldown;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public int maxBulletCount;
    public int reloadTime;
    private int currentBulletCount;
    private bool isReloading;


    public void Start()
    {
        currentBulletCount = maxBulletCount;
        isReloading = false;
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
                //Reload();
            }
        }
    }

    public void Reload()
    {
        isReloading = true;
        //Start a timer
        System.Timers.Timer atimer = new System.Timers.Timer();
        atimer.AutoReset = false;
        atimer.Elapsed += new System.Timers.ElapsedEventHandler(reloadBullet);
        atimer.Interval = reloadTime;
        atimer.Start();
        if (reloadSound)
        {
            AudioSource.PlayClipAtPoint(reloadSound, transform.position);
        }

    }

    private void reloadBullet(object source, System.Timers.ElapsedEventArgs e)
    {
        currentBulletCount = maxBulletCount;
        isReloading = false;
    }
}
