using UnityEngine;

public class Quest
{
    public Quest(QuestData data)
    {
        questData = data;
        isCompleted = false;
    }

    public QuestData questData;
    private bool isCompleted;
    private bool startDialogueShowed;
    private bool finishDialogueShowed;

    public string GetCurrentDialogueName()
    {
        if (!startDialogueShowed)
        {
            return questData.StartDialogue;
        }

        if (isCompleted && !finishDialogueShowed)
        {
            return questData.FinishDialogue;
        }

        return string.Empty;
    }

    public void CompleteQuest()
    {
        isCompleted = true;
    }

    public bool IsStartDialogueShowed()
    {
        return startDialogueShowed;
    }

    public bool IsFinishDialogueShowed()
    {
        return finishDialogueShowed;
    }

    public void FinishDialogue()
    {
        if (!startDialogueShowed)
        {
            startDialogueShowed = true;
        }

        if (isCompleted && !finishDialogueShowed)
        {
            finishDialogueShowed = true;
        }
    }
}

public class QuestManager : MonoBehaviour
{
    private Quest currentQuest;

    private void Awake()
    {
        PublicVars.questManager = this;

        EventsSystem.OnLaterEffectScreenFaded += OnLaterEffectScreenFaded;
    }

    private void OnDestroy()
    {
        EventsSystem.OnLaterEffectScreenFaded -= OnLaterEffectScreenFaded;
    }

    private void Start()
    {
        SetCurrentQuest("Intro"); // start quest
    }

    public void SetCurrentQuest(string questName)
    {
        currentQuest = new Quest(PublicVars.gameResources.GetQuest(questName));
    }

    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    public void SetNextQuest()
    {
        if (GetCurrentQuest().questData.NextQuestName == "EndGame")
        {
            PublicVars.playerController.SetBusy(true);
            PublicVars.uiManager.ShowEndGame();
        }
        SetCurrentQuest(GetCurrentQuest().questData.NextQuestName);

    }

    private void OnLaterEffectScreenFaded()
    {
        PublicVars.playerController.ResetPos();
        if (GetCurrentQuest().questData.TilemapToShow != TilemapName.None)
        {
            PublicVars.tilemapsHolder.GetTilemapByName(GetCurrentQuest().questData.TilemapToShow).gameObject.SetActive(true);
        }
        if (GetCurrentQuest().questData.ResetCrops)
        {
            PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Seeds).ClearAllTiles();
            PublicVars.farmingController.ClearSeeds();
        }
        SetNextQuest();
    }

    public void FinishQuest()
    {
        if (GetCurrentQuest().questData.ShowLaterEffect)
        {
            PublicVars.uiManager.ShowLaterEffect();
        }
        else
        {
            SetNextQuest();
        }
    }
}
