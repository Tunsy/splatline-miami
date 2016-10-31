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
        currentWeapon.Shoot();
    }
}
