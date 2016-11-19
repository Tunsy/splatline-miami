using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

    public Weapon currentWeapon;
    public Weapon[] weapons;
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

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
    void Update()
    {
        timer += Time.deltaTime;

        // Check if the player is holding down a click and shoot if the cooldown is over
        if (Input.GetButton("Fire1") && timer > currentWeapon.fireCooldown)
        {
            Shoot();
        }

        // Check for weapon switcher
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]) && weapons[i] != null)
            {
                currentWeapon = weapons[i];
            }
        }
    }

    void Shoot()
    {
        timer = 0;
        currentWeapon.Shoot();
    }
}
