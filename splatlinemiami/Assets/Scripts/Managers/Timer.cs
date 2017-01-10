using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    private int score;
    public Font deadFont;
    public bool infiniteTime;
    public bool musicDisabled;

    private bool runningOut;
    public AudioClip gameMusic;
    public AudioClip outOfTimeMusic;
    public AudioClip tick;
    private int lastSecond;

    // Use this for initialization
    void Start()
    {
        if (infiniteTime) timeLeft = 0;
        if (!musicDisabled) GameManager.Instance.PlayClip(gameMusic, .3f, true);
        runningOut = false;
    }
    public void GameOver()
    {
        //Time.timeScale = 0;
        score = GameManager.Instance.currentBloodCount;
        GameManager.Instance.isGameOver = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            // Update timer
            if (!infiniteTime)
            {
                timeLeft -= Time.deltaTime;

                // Check for end of time
                if (timeLeft < 0)
                {
                    GameOver();
                }

                // Check for running out of time
                if(timeLeft <= 16 && !runningOut)
                {
                    GameManager.Instance.PlayClip(outOfTimeMusic, .2f, false);
                    runningOut = true;
                    lastSecond = 15;
                }
                if(timeLeft > 16)
                {
                    runningOut = false;
                }

                // Tick after every second
                if(runningOut && timeLeft <= 10)
                {
                    int currentSecond = (int)timeLeft;
                    if(currentSecond != lastSecond)
                    {
                        Tick();
                    }
                    lastSecond = currentSecond;
                }
            }
        }
    }

    void OnGUI()
    {
        // GUI Style
        GUIStyle myStyle = new GUIStyle();
        myStyle.font = deadFont;
        myStyle.normal.textColor = Color.red;
        myStyle.fontSize = 50;
        GUI.skin.label.fontSize = 20;

        // Display GUI if the game isnt over
        if(!GameManager.Instance.isGameOver)
        {
            GUI.Label(new Rect(10, 10, 100, 100), "SCORE: " + GameManager.Instance.GetScore(), myStyle);
            GUI.skin.label.fontSize = 40;
            GUI.Label(new Rect(Screen.width / 2, 10, 200, 100), "" + (int)timeLeft, myStyle);
        }
    }

    void Tick()
    {
        AudioSource.PlayClipAtPoint(tick, Camera.main.transform.position);
    }

    public void ExtendTime(int length)
    {
        timeLeft += length;
    }
}
