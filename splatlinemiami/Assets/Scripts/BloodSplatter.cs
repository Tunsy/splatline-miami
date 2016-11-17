using UnityEngine;
using System.Collections;

public class BloodSplatter : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D other)
    {
        BloodTile bloodTile = other.gameObject.GetComponent<BloodTile>();
        if(bloodTile)
        {
            if (!bloodTile.isBloody)
            {
                bloodTile.isBloody = true;
                GameManager.Instance.totalBloodCount++;
            }
        }
    }
}
