using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform targetPosition;
    private Vector3 targetCoords;

	// Use this for initialization
	void Start () {
        // Default to the players position
        if(targetPosition == null)
        {
            targetPosition = FindObjectOfType<Player>().GetComponent<Transform>();
        }

        targetCoords = targetPosition.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        targetCoords = targetPosition.transform.position;
    }
}
