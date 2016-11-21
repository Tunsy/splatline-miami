using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    public float timeLeft;
    private int score;
	// Use this for initialization
	void Start () {
        
	}
	public void GameOver()
    {
        Time.timeScale = 0;
        score = GameManager.Instance.totalBloodCount;

    }
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            GameOver();
        }
	}
}
