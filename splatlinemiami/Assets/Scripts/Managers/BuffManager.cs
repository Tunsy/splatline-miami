using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour {

    public Player player;
    public AttackController ac;
    public List<Pickup> currentBuffs = new List<Pickup>();


	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        ac = GetComponent<AttackController>();
	}
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < currentBuffs.Count; i++)
        {
            currentBuffs[i].UpdateTimer();
            GameManager.Instance.UpdateBuffsUI(currentBuffs[i]);
        }
    }
}
