using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform targetPosition;
    private Vector3 targetCoords;
    [HideInInspector]
    public Vector2 targetDirection;
    private EnemyMoveController moveController;

	// Use this for initialization
	void Start () {
        // Default to the players position
        if(targetPosition == null)
        {
            targetPosition = FindObjectOfType<Player>().GetComponent<Transform>();
        }
        targetCoords = targetPosition.transform.position;
        moveController = GetComponent<EnemyMoveController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.isGameOver == false)
        {
            targetCoords = targetPosition.transform.position;
            targetDirection = targetCoords - transform.position;
            moveController.UpdateMovement(targetDirection.normalized);
        }
    }
}
