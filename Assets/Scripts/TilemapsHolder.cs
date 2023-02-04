using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TilemapName
{
    None,
    Ground,
    Farm,
    Trees,
    Seeds,
    WaterPlants,
    Chickens
}

public class TilemapsHolder : MonoBehaviour
{
    [Serializable]
    public struct TilemapInfo
    {
        public TilemapName Name;
        public Tilemap Tilemap;
    } 

    [SerializeField] private List<TilemapInfo> tilemaps;

    private void Awake()
    {
        PublicVars.tilemapsHolder = this;
    }

    public Tilemap GetTilemapByName(TilemapName name)
    {
        int index = tilemaps.FindIndex((el) => el.Name == name);
        if (index >= 0)
        {
            return tilemaps.Find((el) => el.Name == name).Tilemap;
        }
        return null;
    }
}
