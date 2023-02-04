using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingController : MonoBehaviour
{
    [Header("Controllers")]
    public List<SeedController> seedControllers = new List<SeedController>();

    [SerializeField] private Vector3Int playerPosOffset;

    private void Awake()
    {
        PublicVars.farmingController = this;
    }

    public bool CheckCell(Vector3Int playerCellPosition)
    {
        if (PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Farm).GetTile(playerCellPosition) is null) 
            return false;

        seedControllers.Add(gameObject.AddComponent<SeedController>());
        seedControllers.Last().AddSeed(
            PublicVars.gameResources.GetSeed("Tomato"),
            playerCellPosition + playerPosOffset);

        if (PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Farm).GetUsedTilesCount() == seedControllers.Count)
        {
            PublicVars.questManager.GetCurrentQuest().isCompleted = true;
        }

        return true;
    }
}
