using UnityEngine;
using System.Collections;

public class SpeedPickup : Pickup {

    public float speedMagnitude;

    public override void ApplyBuff()
    {
        base.ApplyBuff();

        // Check for existing buff
        for(int i = 0; i < bm.currentBuffs.Count; i++)
        {
            if (bm.currentBuffs[i].GetComponent<SpeedPickup>())
            {
                bm.currentBuffs[i].resetTimer();
                Destroy(transform.parent.gameObject);
                return;
            }
        }

        // Apply buff
        bm.currentBuffs.Add(this);
        bm.player.speed *= speedMagnitude;
        GameManager.Instance.DisplayText("Speed powerup!");
    }

    public override void RemoveBuff()
    {
        bm.player.speed /=  speedMagnitude;
        bm.currentBuffs.Remove(this);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
