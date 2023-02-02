using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasNode;

    private void Awake()
    {
        PublicVars.uiManager = this;
    }

    public void ShowDialogueAtPos(Vector3 pos, string text)
    {
        GameObject dialogueNode = Instantiate(PublicVars.gameResources.GetPrefabByName("DialoguePopup"), pos, Quaternion.identity, canvasNode.transform);
        dialogueNode.GetComponent<DialoguePopupScript>().SetText(text);   
    }
}
