using System;

[Serializable]
public struct QuestData
{
    public string Name;
    public string NextQuestName;
    public string StartDialogue;
    public string FinishDialogue;
    public bool NeedCrops;
    public string CropName;
    public bool ShowLaterEffect;
    public TilemapName TilemapToShow;
    public bool ResetCrops;
}