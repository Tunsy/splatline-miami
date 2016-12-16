using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    private int score;
    public Font deadFont;
    public bool infiniteTime;

    // Use this for initialization
    void Start()
    {
        if (infiniteTime) timeLeft = 0;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        score = GameManager.Instance.totalBloodCount;
        GameManager.Instance.isGameOver = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (!infiniteTime)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GameOver();
            }
        }
    }
    void OnGUI()
    {
        GUI.skin.label.fontSize = 20;
        GUI.skin.label.font = deadFont;
        GUI.Label(new Rect(10, 10, 100, 100), "SCORE: " + GameManager.Instance.totalBloodCount * 10);
        GUI.skin.label.fontSize = 40;
        GUI.Label(new Rect(Screen.width / 2, 10, 200, 100), "" + (int)timeLeft);

    }
}
