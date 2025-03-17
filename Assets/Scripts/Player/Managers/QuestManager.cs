using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [Header("NPC Quest Panel")]
    [SerializeField] private QuestCardNPC questCardNPCPrefab;
    [SerializeField] private Transform npcPanelContainer;

    [Header("PLayer Quest Panel")]
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab;
    [SerializeField] private Transform playerQuestContainer;

    void Start()
    {
        LoadQuestsIntroNPCPanel();
    }

    public void AcceptQuest(Quest quest) {
        QuestCardPlayer cardPlayer = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    private Quest QuestExists(string questID) {
        foreach (Quest quest in quests) {
            if (quest.ID == questID) {
                return quest;
            }
        }
        return null;
    }

    public void AddProgress(string questID, int amount) {
        Quest questToUpdate = QuestExists(questID);
        if (questToUpdate == null) return;
        if (questToUpdate.QuestAccepted) {
            questToUpdate.AddProgress(amount);
        }
    }

    private void LoadQuestsIntroNPCPanel() {
        for (int i = 0; i < quests.Length; i++) {
            QuestCard npcCard = Instantiate(questCardNPCPrefab, npcPanelContainer);
            npcCard.ConfigQuestUI(quests[i]);

        }
    }
    void OnEnable()
    {
        for (int i = 0; i < quests.Length; i++) {
            quests[i].ResetQuest();
        }   
    }
}