using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour {

    private EnemyAI enemyAI;
    private Animator anim;


	// Use this for initialization
	void Start () {
        enemyAI = GetComponent<EnemyAI>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 normalizedDirection = new Vector2(Mathf.Abs(enemyAI.targetDirection.x), Mathf.Abs(enemyAI.targetDirection.y));

        // Change direction depending on the magnitude of the X and Y target vector
        if(normalizedDirection.x > normalizedDirection.y)
        {
            if(enemyAI.targetDirection.x > 0)
            {
                anim.SetFloat("FacingX", 1);
                anim.SetFloat("FacingY", 0);
            }
            else
            {
                anim.SetFloat("FacingX", -1);
                anim.SetFloat("FacingY", 0);
            }
        }else
        {
            if (enemyAI.targetDirection.y > 0)
            {
                anim.SetFloat("FacingY", 1);
                anim.SetFloat("FacingX", 0);
            }
            else
            {
                anim.SetFloat("FacingY", -1);
                anim.SetFloat("FacingX", 0);
            }
        }

        anim.SetBool("IsMoving", enemyAI.isMoving);
	}
}
