using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "ScriptableObjects/GameResources")]
public class GameResourcesSO : ScriptableObject
{
    public GameObject[] prefabs;
    public AudioClip[] sounds;
    public DialogData[] dialogs;
    public QuestData[] quests;
    public Seed[] seeds;
}
