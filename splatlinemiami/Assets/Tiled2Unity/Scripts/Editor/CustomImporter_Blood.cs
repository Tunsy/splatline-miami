﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Tiled2Unity.CustomTiledImporter]
public class CustomImporter_Blood : Tiled2Unity.ICustomTiledImporter
{

    public void HandleCustomProperties(GameObject gameObject,
        IDictionary<string, string> customProperties)
    {
        if (customProperties.ContainsKey("isBloody"))
        {
            // Add the terrain tile game object
            FBloodTile tile = gameObject.AddComponent<FBloodTile>();
            tile.isBloody = customProperties["isBloody"];
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}