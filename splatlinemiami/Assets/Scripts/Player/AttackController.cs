using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

    public Weapon currentWeapon;
    public Weapon[] weapons;
    public float damageMultiplier;
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



	// Use this for initialization
	void Start () {
        currentWeapon = weapons[0];
        timer = 0;
        damageMultiplier = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isGameOver)
        {
            timer += Time.deltaTime;

            // Check if the player is holding down a click and shoot if the cooldown is over
            if (Input.GetButton("Fire1") && timer > currentWeapon.fireCooldown)
            {
                Shoot(damageMultiplier);
            }

            // Check for weapon switcher
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]) && weapons[i] != null)
                {
                    currentWeapon = weapons[i];
                    GameManager.Instance.SetActiveWeapon(i);
                }
            }
        }
    }

    void Shoot(float damageMultiplier)
    {
        timer = 0;
        currentWeapon.Shoot(damageMultiplier);
    }
}
