using UnityEngine;

public class Quest
{
    public Quest(QuestData data)
    {
        questData = data;
        isCompleted = false;
    }

    public QuestData questData;
    public bool isCompleted;

    public string GetCurrentDialogueName()
    {
        return isCompleted ? questData.FinishDialogue : questData.StartDialogue;
    }
}

public class QuestManager : MonoBehaviour
{
    private Quest currentQuest;

    private void Awake()
    {
        PublicVars.questManager = this;
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
}
