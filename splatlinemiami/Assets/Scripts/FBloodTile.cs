using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FBloodTile : MonoBehaviour
{
    private static readonly IDictionary<string, Color> TypeToColor = new Dictionary<string, Color>()
    {
        { "Bridge", new Color(0.5f, 0, 0f) },
        { "Grass", new Color(0.9f, 0.6f, 0) },
        { "Mountain", Color.red },
        { "Tree", new Color(0, 0.5f, 0) },
        { "Wall", Color.black },
        { "Water", Color.blue },
    };

    public string isBloody = "false";
    public int NumberOfClicks = 0;
    public float timer = 0;
    public float maxTime = 3;

    private void OnDrawGizmosSelected()
    {
        Vector3 position = this.transform.position;
        position.x += 8;
        position.y -= 8;

        //Color drawColor;
        //if (!BloodTile.TypeToColor.TryGetValue(this.isBloody, out drawColor))
        //{
        //    drawColor = Color.black;
        //}


        //Color fillColor = drawColor;
        //fillColor.a = 0.25f;

        //Gizmos.color = fillColor;
        //Gizmos.DrawCube(position, new Vector3(16, 16, 0));

        //Gizmos.color = drawColor;
        //Gizmos.DrawWireCube(position, new Vector3(16, 16, 0));
    }

    public void Update()
    {
        //Debug.Log(isBloody);

        timer += Time.deltaTime;
        if(timer > maxTime)
        {
            timer = 0;

            if(isBloody == "false")
            {
                isBloody = "true";
            }else
            {
                isBloody = "false";
            }
        }
    }

}