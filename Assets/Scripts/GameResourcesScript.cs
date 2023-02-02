using System.Collections.Generic;
using UnityEngine;

public class GameResourcesScript : MonoBehaviour
{
    [SerializeField] private GameResourcesSO resourcesSO;

    private Dictionary<string, GameObject> prefabs;
    private Dictionary<string, AudioClip> sounds;

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
}