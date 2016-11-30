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
        if (GameManager.Instance.isGameOver == false)
        {
            targetCoords = targetPosition.transform.position;
            Vector2 targetDirection = targetCoords - transform.position;
            //transform.position += transform.TransformDirection(targetDirection.normalized) * 0.02f;
        }
    }
}
