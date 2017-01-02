using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class AttackPickup : Pickup {

    public float attackMagnitude;

    public override void ApplyBuff()
    {
        base.ApplyBuff();

        // Check for existing buff
        for (int i = 0; i < bm.currentBuffs.Count; i++)
        {
            if (bm.currentBuffs[i].GetComponent<AttackPickup>())
            {
                bm.currentBuffs[i].resetTimer();
                Destroy(transform.parent.gameObject);
                return;
            }
        }

        // Apply buff
        bm.currentBuffs.Add(this);
        bm.ac.damageMultiplier *= attackMagnitude;
        GameManager.Instance.DisplayText("Attack Powerup!");
        iconInstance = Instantiate(buffIcon, new Vector2(-267 + (30 * bm.currentBuffs.Count), -12), Quaternion.identity) as Image;
        iconInstance.transform.SetParent(canvas.transform.FindChild("HUD").transform, false);

    }

    public override void RemoveBuff()
    {
        bm.ac.damageMultiplier /= attackMagnitude;
        bm.currentBuffs.Remove(this);
        Destroy(gameObject.transform.parent.gameObject);
    }

}
