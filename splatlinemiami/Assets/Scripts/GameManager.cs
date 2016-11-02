using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public enum StateType
    {
        DEFAULT,      //Fall-back state, should never happen
        PLAYING,      //waiting for other player to finish his turn
        SHOP,    //Once, on start of each player's turn
    };

    public StateType currentState;

    private float timer;
    public float roundTime;
    private int currentLevel;
    private bool[,] bloodyTiles;

    public void Start()
    {
        timer =  roundTime;
        currentLevel = 0;
        currentState = StateType.PLAYING;
        Vector2 gridSize = calculateGridSize(6,6,4);
        bloodyTiles = new bool[(int)gridSize.x,(int)gridSize.y];
    }

    public void Update()
    {

        timer -= Time.deltaTime;

        // Go to the shop after the timer ends
        if(timer <= 0)
        {
            timer = roundTime;
            currentState = StateType.SHOP;
            Debug.Log("Times up!");
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private Vector2 calculateGridSize(int length, int height, int divisions)
    {
        int gridLength = (length + currentLevel) * divisions;
        int gridHeight = (height + currentLevel) * divisions;
        return new Vector2(gridLength, gridHeight);
    }
}