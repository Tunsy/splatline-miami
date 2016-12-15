using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    public Player player;
    public Animator anim;

	// Use this for initialization
	void Start () {
        player = GetComponentInChildren<Player>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("CurrentAngle", player.currentRotation);
        anim.SetBool("IsMoving", player.isMoving);
	}

}
