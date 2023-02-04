using System.Collections.Generic;
using UnityEngine;

public class GameResourcesScript : MonoBehaviour
{
    [SerializeField] private GameResourcesSO resourcesSO;

    private Dictionary<string, GameObject> prefabs;
    private Dictionary<string, AudioClip> sounds;
    private Dictionary<string, DialogData> dialogs;
    private Dictionary<string, QuestData> quests;
    private Dictionary<string, Seed> seeds;

    private void Awake()
    {
        PublicVars.gameResources = this;

        prefabs = new Dictionary<string, GameObject>();
        foreach (var prefab in resourcesSO.prefabs)
        {
            prefabs.Add(prefab.name, prefab);
        }
        sounds = new Dictionary<string, AudioClip>();
        foreach (var sound in resourcesSO.sounds)
        {
            sounds.Add(sound.name, sound);
        }
        dialogs = new Dictionary<string, DialogData>();
        foreach (var dialog in resourcesSO.dialogs)
        {
            dialogs.Add(dialog.Name, dialog);
        }
        quests = new Dictionary<string, QuestData>();
        foreach (var quest in resourcesSO.quests)
        {
            quests.Add(quest.Name, quest);
        }
        seeds = new Dictionary<string, Seed>();
        foreach (var seed in resourcesSO.seeds)
        {
            seeds.Add(seed.seedName, seed);
        }
    }

    public GameObject GetPrefabByName(string prefabName)
    {
        if (prefabs.ContainsKey(prefabName))
            return prefabs[prefabName];
        return null;
    }

    public AudioClip GetSound(string soundName)
    {
        if (sounds.ContainsKey(soundName))
            return sounds[soundName];
        return null;
    }

    public DialogData GetDialogue(string name)
    {
        if (dialogs.ContainsKey(name))
        {
            var data = new DialogData() { Name = name, 
                Phrases = dialogs[name].Phrases,
                IsPlayerDialogue = dialogs[name].IsPlayerDialogue
            };
            return data;
        }
        return new DialogData();
    }

    public QuestData GetQuest(string name)
    {
        if (quests.ContainsKey(name))
        {
            var data = new QuestData() { Name = name, 
                NextQuestName = quests[name].NextQuestName, 
                StartDialogue = quests[name].StartDialogue,
                FinishDialogue = quests[name].FinishDialogue,
                NeedCrops = quests[name].NeedCrops,
                CropName = quests[name].CropName,
                ShowLaterEffect = quests[name].ShowLaterEffect,
                TilemapToShow = quests[name].TilemapToShow,
                ResetCrops = quests[name].ResetCrops
            };
            return data;
        }
        return new QuestData();
    }

    public Seed GetSeed(string name)
    {
        if (seeds.ContainsKey(name))
            return seeds[name];
        return null;
    }
}