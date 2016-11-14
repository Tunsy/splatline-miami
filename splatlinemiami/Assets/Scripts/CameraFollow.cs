using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform playerPosition;
    public Camera m_camera;
    public float xOffset;
    public float yOffset;
    public float DampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        m_camera = Camera.main;
        playerPosition = FindObjectOfType<Player>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerPosition)
        {
            Vector3 point = m_camera.WorldToViewportPoint(playerPosition.position);
            Vector3 delta = playerPosition.position - m_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta + new Vector3(xOffset, yOffset);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, DampTime);
        }
    }
}
