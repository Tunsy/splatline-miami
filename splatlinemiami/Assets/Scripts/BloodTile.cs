﻿using UnityEngine;
using System.Collections;

public class BloodTile : MonoBehaviour {

    public bool isBloody;
    private BoxCollider2D col;

    public float xSize;
    public float ySize;

	// Use this for initialization
	void Start () {
        isBloody = false;
        col = GetComponent<BoxCollider2D>();
        xSize = col.size.x;
        ySize = col.size.y;
        Debug.Log("2 " + xSize);
        col.offset = new Vector2(col.size.x/2, col.size.y/2 * -1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
