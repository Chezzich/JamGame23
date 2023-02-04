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

    private int neededCropsCount;

    private void Awake()
    {
        PublicVars.farmingController = this;

        neededCropsCount = PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Farm).GetTilesRangeCount(new Vector3Int(-9999, -9999), new Vector3Int(9999, 9999));
    }

    public bool CheckCell(Vector3Int playerCellPosition)
    {
        if (PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Farm).GetTile(playerCellPosition) is null) 
            return false;

        if (PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Seeds).GetTile(playerCellPosition))
            return false;

        seedControllers.Add(gameObject.AddComponent<SeedController>());
        seedControllers.Last().AddSeed(
            PublicVars.gameResources.GetSeed(PublicVars.questManager.GetCurrentQuest().questData.CropName),
            playerCellPosition + playerPosOffset);

        if (neededCropsCount == seedControllers.Count)
        {
            PublicVars.questManager.GetCurrentQuest().CompleteQuest();
            ClearSeeds();
        }

        return true;
    }

    public void ClearSeeds()
    {
        seedControllers.Clear();
    }
}
