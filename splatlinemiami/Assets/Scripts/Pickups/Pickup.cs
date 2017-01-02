using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Pickup : MonoBehaviour {

    public float buffTime;
    public float currentTime;
    public bool currentlyApplied;
    protected BuffManager bm;
    protected BoxCollider2D col;
    protected SpriteRenderer sr;
    public SpriteRenderer shadow;
    public Image buffIcon;
    public Image iconInstance;
    public AudioClip powerupSound;
    protected Canvas canvas;

    public abstract void RemoveBuff();

    public void Start()
    {
        bm = FindObjectOfType<BuffManager>();
        col = GetComponent <BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        canvas = FindObjectOfType<Canvas>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && !currentlyApplied)
        {
            ApplyBuff();
        }
    }

    public virtual void ApplyBuff()
    {
        // Hide powerup
        currentlyApplied = true;
        Destroy(col);
        Destroy(sr);
        Destroy(shadow);

        // Check for existing buff
        if (powerupSound)
        {
            AudioSource.PlayClipAtPoint(powerupSound, Camera.main.transform.position);
        }




        currentTime = buffTime;
    }

    public void UpdateTimer()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0 && currentlyApplied)
        {
            RemoveBuff();
            currentlyApplied = false;
        }
    }

    public void resetTimer()
    {
        currentTime = buffTime;
    }

}
