using UnityEngine;
using System.Collections;

public class TimePickup : MonoBehaviour {

    public int timeExtended;
    public AudioClip powerupSound;
    private Timer timer;

	// Use this for initialization
	void Start () {
        timer = GameManager.Instance.GetComponent<Timer>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            AudioSource.PlayClipAtPoint(powerupSound, Camera.main.transform.position);
            timer.ExtendTime(timeExtended);
            GameManager.Instance.DisplayText("+5 Seconds!");
            Destroy(transform.parent.gameObject);
        }
    }
}
