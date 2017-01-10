using UnityEngine;
using System.Collections;

public class BloodSplatter : MonoBehaviour {

    public bool hasTile;
    public bool flaggedToDestroy;
    private SpriteRenderer sr;

    public void Start()
    {
        flaggedToDestroy = false;
        hasTile = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        BloodTile bloodTile = other.gameObject.GetComponent<BloodTile>();
        if (bloodTile)
        {
            if (!bloodTile.isBloody)
            {
                bloodTile.isBloody = true;
                hasTile = true;
                GameManager.Instance.currentBloodCount++;
            }
        }
    }
}
