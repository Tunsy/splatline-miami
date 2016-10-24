using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

    public Weapon currentWeapon;

    private float timer;

    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

	// Use this for initialization
	void Start () {
        shootableMask = LayerMask.GetMask("Shootable");
        currentWeapon = GetComponentInChildren<Weapon>();
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        // Check if the player is holding down a click and shoot if the cooldown is over
        if (Input.GetButton("Fire1") && timer > currentWeapon.fireCooldown)
        {
            Shoot();
        }
	}

    void Shoot()
    {
        timer = 0;

        // Convert the angle of the player to the velocity of the bullet and shoot it forward
        GameObject currentBullet = (GameObject)Instantiate(currentWeapon.bullet.gameObject, currentWeapon.transform.position, transform.rotation);
        Vector2 angle = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
        currentBullet.GetComponent<Rigidbody2D>().velocity = angle * currentBullet.GetComponent<Bullet>().bulletSpeed;
    }
}
