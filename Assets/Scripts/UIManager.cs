using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasNode;

    private GameObject dialogueNode;
    private bool isActiveDialog = false;

    private DialogData activeDialogData;
    private int phraseNum = 0;

    private void Awake()
    {
        PublicVars.uiManager = this;
    }

    public void ShowDialogueAtPos(Vector3 pos, string dialogueName)
    {
        if (string.IsNullOrEmpty(dialogueName) && phraseNum == 0)
            return;

        if (!dialogueNode)
        {
            dialogueNode = Instantiate(PublicVars.gameResources.GetPrefabByName("DialoguePopup"), pos, Quaternion.identity, canvasNode.transform);
        }
        if (string.IsNullOrEmpty(activeDialogData.Name))
        {
            activeDialogData = PublicVars.gameResources.GetDialogue(dialogueName);
            phraseNum = 0;
        }

        if (phraseNum < activeDialogData.Phrases.Length)
        {
            PublicVars.playerController.SetBusy(true);
            isActiveDialog = true;
            
            dialogueNode.GetComponent<DialoguePopupScript>().SetText(activeDialogData.Phrases[phraseNum]);
            phraseNum++;
        }
        else
        {
            FinishDialogue();
        }
    }

    public void FinishDialogue()
    {
        Destroy(dialogueNode);

        isActiveDialog = false;
        activeDialogData = new DialogData();
        PublicVars.playerController.SetBusy(false);
        PublicVars.playerController.SetIsIntro(false);
    }

    public bool IsActiveDialogue()
    {
        return isActiveDialog;
    }
}
