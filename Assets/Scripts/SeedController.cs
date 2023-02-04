using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Seed
{
    public string seedName;
    public Tile tile;
}

public class SeedController : MonoBehaviour
{
    public Seed seed;

    public Vector3Int position { private set; get; }

    public void AddSeed(Seed seed, Vector3Int position)
    {
        this.seed = seed;
        this.position = position;
        ChangeTile();
    }

    private void ChangeTile()
    {
        PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Seeds).SetTile(position, seed.tile);
    }
}
