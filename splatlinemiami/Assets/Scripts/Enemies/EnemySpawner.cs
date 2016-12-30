using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemyList;
    public float currentTimer;
    public float maxTime;
    public float minTime;

	// Use this for initialization
	void Start () {
        currentTimer = Random.Range(minTime, maxTime);
	}

    //TODO Random position
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.Instance.isGameOver)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                for (int i = 0; i <= (int)Random.Range(0, 3); i++)
                {
                    GameObject enemy = enemyList[(int)Random.Range(0, enemyList.Length)];
                    Instantiate(enemy, new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2), 0), Quaternion.identity);
                    currentTimer = Random.Range(minTime, maxTime);
                }
            }
        }

	}
}
