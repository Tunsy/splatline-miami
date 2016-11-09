using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
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
        currentTimer -= Time.deltaTime;

        if(currentTimer <= 0 )
        {
            Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
            currentTimer = Random.Range(minTime, maxTime);
        }
	}
}
