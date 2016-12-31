using UnityEngine;
using System.Collections;

public class TimePickup : MonoBehaviour {

    public float timeExtended;
    private Timer timer;

	// Use this for initialization
	void Start () {
        timer = GameManager.Instance.GetComponent<Timer>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            timer.ExtendTime(timeExtended);
        }
    }
}
