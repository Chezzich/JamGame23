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

    public void ShowPlayerDialogueIfNeeded(Vector3 pos)
    {
        if (PublicVars.questManager.GetCurrentQuest().questData.Name == null)
            return;

        if (PublicVars.gameResources.GetDialogue(PublicVars.questManager.GetCurrentQuest().GetCurrentDialogueName()).IsPlayerDialogue
            && phraseNum == 0)
        {
            ShowDialogueAtPos(pos, PublicVars.questManager.GetCurrentQuest().GetCurrentDialogueName());
        }
    }

    public void ShowDialogueAtPos(Vector3 spawnPos, string dialogueName)
    {
        if (string.IsNullOrEmpty(dialogueName) && phraseNum == 0)
            return;

        if (string.IsNullOrEmpty(activeDialogData.Name))
        {
            activeDialogData = PublicVars.gameResources.GetDialogue(dialogueName);
            phraseNum = 0;
        }
        if (!dialogueNode)
        {
            var prefabName = activeDialogData.IsPlayerDialogue ? "DialoguePopupMan" : "DialoguePopup";
            dialogueNode = Instantiate(PublicVars.gameResources.GetPrefabByName(prefabName), spawnPos, Quaternion.identity, canvasNode.transform);
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

        phraseNum = 0;
        isActiveDialog = false;
        activeDialogData = new DialogData();
        PublicVars.playerController.SetBusy(false);
        PublicVars.playerController.SetIsIntro(false);

        PublicVars.questManager.GetCurrentQuest().FinishDialogue();
        if (PublicVars.questManager.GetCurrentQuest().IsFinishDialogueShowed()
            || string.IsNullOrEmpty(PublicVars.questManager.GetCurrentQuest().questData.FinishDialogue))
        {
            PublicVars.questManager.FinishQuest();
        }
        else if (!string.IsNullOrEmpty(PublicVars.questManager.GetCurrentQuest().questData.FinishDialogue)
            && !PublicVars.questManager.GetCurrentQuest().questData.NeedCrops)
        {
            PublicVars.questManager.GetCurrentQuest().CompleteQuest();
        }
    }

    public bool IsActiveDialogue()
    {
        return isActiveDialog;
    }

    public void ShowLaterEffect()
    {
        Instantiate(PublicVars.gameResources.GetPrefabByName("LaterEffect"), canvasNode.transform);
    }

    public void ShowEndGame()
    {
        Instantiate(PublicVars.gameResources.GetPrefabByName("EndGameEffect"), canvasNode.transform);
    }
}
