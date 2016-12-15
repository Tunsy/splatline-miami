using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    public float rotateSpeed;

    private Camera cam;
    private Quaternion targetRotation;
    private Rigidbody2D rb;

    public float currentRotation;
    public bool isMoving;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        ControlMouse();
    }

    void ControlMouse()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        transform.rotation = rot;
        currentRotation = transform.rotation.eulerAngles.z;
        transform.eulerAngles = new Vector3(0, 0, currentRotation);
        rb.angularVelocity = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(x * speed, y * speed);
        isMoving = !(rb.velocity.x == 0 && rb.velocity.y == 0);
    }
}
