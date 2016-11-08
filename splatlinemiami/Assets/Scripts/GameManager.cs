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

    public BloodTile bloodTile;
    public Transform tileGrid;
    private BloodTile[,] bloodyTiles;
    private int currentLevel;
    public int totalBloodCount;
    
    // Offset in tiles
    public float xTileOffset;
    public float yTileOffset;

    public void Start()
    {
        timer =  roundTime;
        currentState = StateType.PLAYING;
        yTileOffset *= -1;
        currentLevel = 0;
        Vector2 gridSize = calculateGridSize(6,6,4);
        bloodyTiles = new BloodTile[(int)gridSize.x,(int)gridSize.y];
        GenerateBloodTiles();
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

    public void GenerateBloodTiles()
    {
        // Generate the array of tiles to be marked as bloody
        for (int i = 0; i < bloodyTiles.GetLength(0); i++)
        {
            float yPos = yTileOffset + (i * bloodTile.ySize) * -1;
            for (int j = 0; j < bloodyTiles.GetLength(1); j++)
            {
                float xPos = xTileOffset + (j * bloodTile.xSize);
                BloodTile currentTile = (BloodTile)Instantiate(bloodTile, new Vector3(xPos, yPos, 0), Quaternion.identity, tileGrid);
            }
        }
    }

    //public bool CheckBloodTiles(int percentFilled)
    //{
    //    int count = 0;
    //    int totalSize = bloodyTiles.GetLength(0) * bloodyTiles.GetLength(1);

    //    for (int i = 0; i < bloodyTiles.GetLength(0); i++)
    //    {
    //        for(int j = 0; j < bloodyTiles.GetLength(1); j++)
    //        {
    //            if(bloodyTiles[i,j].isBloody == true)
    //            {
    //                count++;
    //            }
    //        }
    //    }
    //    return false;
    //}

    public bool CheckBloodTiles(int percentFilled)
    {
        int totalSize = bloodyTiles.GetLength(0) * bloodyTiles.GetLength(1);

        return (totalBloodCount/totalSize)*100 > percentFilled;
    }
}