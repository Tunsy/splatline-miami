using UnityEngine;
using System.Collections;

public class BloodSplatter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        BloodTile bloodTile = other.gameObject.GetComponent<BloodTile>();
        if(bloodTile)
        {
            if (!bloodTile.isBloody)
            {
                bloodTile.isBloody = true;
                GameManager.Instance.totalBloodCount++;
                GameManager.Instance.CheckBloodTiles();
            }
        }
    }
}
