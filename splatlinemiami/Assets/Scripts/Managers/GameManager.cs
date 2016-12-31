using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public enum StateType
    {
        DEFAULT,      //Fall-back state, should never happen
        PLAYING,      //waiting for other player to finish his turn
        SHOP,    //Once, on start of each player's turn
        GAMEOVER,
    };

    // Game state
    public StateType currentState;
    public bool isGameOver;
    public bool enemiesDisabled;
    public bool musicDisabled;

    // Grid
    public BloodTile bloodTile;
    public Transform tileGrid;
    private BloodTile[,] bloodyTiles;
    public List<BloodSplatter> bloodSplatters = new List<BloodSplatter>();
    public int totalBloodCount;
    public int percentToExpand;
    public float xTileOffset;
    public float yTileOffset;
    public int startingWidth;
    public int startingHeight;


    // Rooms
    public GameObject[] maps;
    public GameObject currentMap;
    private int currentLevel;
    private int maxLevel;

    //
    public int money;
    public Transform player;

    // UI
    public GameObject gameoverMenu;
    public GameObject HUDMenu;
    public RectTransform bloodBar;
    public Sprite weaponSelected;
    public Sprite weaponNotSelected;
    public Image[] weaponBoxes;
    public Text score;
    public Text hiScore;
    private bool finishedPanning;

    // Audio 
    public AudioSource sfxSource;
    public AudioClip levelUpSound;
    public AudioClip beep;


    public void Start()
    {
        currentState = StateType.PLAYING;
        yTileOffset *= -1;
        currentLevel = 0;
        maxLevel = maps.GetLength(0) - 1;
        money = 0;
        Time.timeScale = 1;

        // Create map and grid
        Instantiate(maps[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
        CreateGrid(startingWidth, startingHeight, 2);

        InvokeRepeating("ClearBlood", 0, 1.0f);
        CheckBloodTiles();

        isGameOver = false;
        finishedPanning = false;
    }

    public void Update()
    {
        if (isGameOver)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        HUDMenu.SetActive(false);
        StartCoroutine("PanCamera");

        if(finishedPanning)
        {
            gameoverMenu.SetActive(true);
            UpdateHighScore();
        }
    }

    void UpdateHighScore()
    {
        // Compare high scores
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (GetScore() > oldHighscore)
            PlayerPrefs.SetInt("highscore", GetScore());

        // Display score
        score.text = GetScore().ToString();
        hiScore.text = PlayerPrefs.GetInt("highscore", 0).ToString();

    }
    
    public IEnumerator PanCamera()
    {
        yield return new WaitForSeconds(0.75f);
        while(Camera.main.orthographicSize < 15.9f)
        {
            Camera.main.transform.position = new Vector3((Mathf.Lerp(player.position.x, 6.5f, Time.deltaTime)), Mathf.Lerp(player.position.y, -6.5f, Time.deltaTime * 0.01f), -10);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 16, Time.deltaTime * 0.003f);
            yield return null;
        }
        finishedPanning = true;
    }

    public void SetActiveWeapon(int weaponIndex)
    {
        for(int i = 0; i < weaponBoxes.Length; i++)
        {
            if (i == weaponIndex)
                weaponBoxes[i].sprite = weaponSelected;
            else
                weaponBoxes[i].sprite = weaponNotSelected;
        }
    }

    public int GetScore()
    {
        return totalBloodCount * 10;
    }

    public void PlayClip(AudioClip clip, float volume, bool isLooping)
    {
        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.loop = isLooping;
        sfxSource.Play();
    }

    public void ClearBlood()
    {
        BloodSplatter[] splatters = FindObjectsOfType<BloodSplatter>();
        foreach(BloodSplatter currentSplatter in splatters)
        {
            if (!currentSplatter.hasTile)
            {
                Destroy(currentSplatter.GetComponent<Rigidbody2D>());
                Destroy(currentSplatter);
            }
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

    private Vector2 CalculateGridSize(int length, int height, int divisions)
    {
        int gridLength = (length + currentLevel) * divisions;
        int gridHeight = (height + currentLevel) * divisions;
        return new Vector2(gridLength, gridHeight);
    }

    private void CreateGrid(int length, int height, int divisions)
    {
        Vector2 gridSize = CalculateGridSize(length, height, divisions);
        bloodyTiles = new BloodTile[(int)gridSize.x, (int)gridSize.y];
        GenerateBloodTiles();
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
                BloodTile currentTile = (BloodTile)Instantiate(bloodTile, new Vector3(xPos, yPos, 1), Quaternion.identity, tileGrid);
                currentTile.x = j;
                currentTile.y = i;
            }
        }
    }

    public void CheckBloodTiles()
    {
        int totalSize = bloodyTiles.GetLength(0) * bloodyTiles.GetLength(1);
        float percentCurrentlyFilled = ((float)totalBloodCount / totalSize)*100;

        // Update size of the blood bar
        float barScaling = percentCurrentlyFilled / percentToExpand;
        barScaling = barScaling > 1 ? 1 : barScaling;
        bloodBar.transform.localScale = new Vector2(barScaling, bloodBar.transform.localScale.y);

        if (percentCurrentlyFilled > percentToExpand){
            ExpandRoom();
        }
    }

    public void ExpandRoom()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            if(levelUpSound)
                AudioSource.PlayClipAtPoint(levelUpSound, Camera.main.transform.position, 1f);

            // Destroy old grid and create new grid 
            for (int i = 0; i < tileGrid.transform.childCount; i++)
            {
                Transform currentTile = tileGrid.transform.GetChild(i);
                Destroy(currentTile.gameObject);
            }
            CreateGrid(startingWidth + (2 * currentLevel), startingHeight + (2 * currentLevel), 2);
            totalBloodCount = 0;

            // Destroy old map and create new map
            Destroy(FindObjectOfType<Tiled2Unity.TiledMap>().gameObject);
            Instantiate(maps[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void AddMoney(int add)
    {
        money += add;
    }

    public void SpendMoney(int sub)
    {
        money -= sub;
        if (money < 0)
            money = 0;
    }
}