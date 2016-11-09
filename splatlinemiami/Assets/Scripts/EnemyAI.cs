using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform targetPosition;
    private Vector3 targetCoords;

	// Use this for initialization
	void Start () {
        targetCoords = targetPosition.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        targetCoords = targetPosition.transform.position;


    }
}
