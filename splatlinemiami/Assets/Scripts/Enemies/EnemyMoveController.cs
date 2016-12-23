using UnityEngine;
using System.Collections;

public class EnemyMoveController : MonoBehaviour {

    public Vector2 movementVector;
    private Rigidbody2D rb;
    public float speed;
    public float knockbackTime;
    public bool isMoving;
    public bool isStunned;
    public bool isKnockedback;
    public bool knockbackable;

    private float timeSpentKnockedback;
    private Vector2 knockbackDirection;
    private float knockbackStrength;

	// Use this for initialization
	void Start () {
        movementVector = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
        isMoving = true;
        if (speed == 0) speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.Instance.isGameOver)
        {
            Move();
        }else
        {
            rb.velocity = new Vector2(0,0);
        }
	}

    public void Move()
    {
        // Handle movement
        if(movementVector.x != 0 && movementVector.y != 0)
        {
            isMoving = true;
        }else
        {
            isMoving = false;
        }

        // Handle knockback
        if (isKnockedback)
        {
            HandleKnockback(ref movementVector);
        }

        rb.velocity = movementVector;
    }

    public void UpdateMovement(Vector2 newMovement)
    {
        movementVector = newMovement * speed;
    }

    public void Knockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        if (knockbackable)
        {
            isKnockedback = true;
            this.knockbackDirection = knockbackDirection;
            this.knockbackStrength = knockbackStrength;
        }
    }

    public void HandleKnockback(ref Vector2 movementVector)
    {
        timeSpentKnockedback += Time.deltaTime;
        movementVector = knockbackDirection * knockbackStrength;
        if (timeSpentKnockedback >= knockbackTime)
        {
            isKnockedback = false;
            timeSpentKnockedback = 0;
        }
    }
}
