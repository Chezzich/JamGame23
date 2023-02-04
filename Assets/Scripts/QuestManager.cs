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
            startDialogueShowed = true;
            return questData.StartDialogue;
        }

        if (isCompleted && !finishDialogueShowed)
        {
            finishDialogueShowed = true;
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
        SetCurrentQuest("Quest1"); // start quest
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
        SetCurrentQuest(GetCurrentQuest().questData.NextQuestName);
    }

    private void OnLaterEffectScreenFaded()
    {
        SetNextQuest();
    }
}
