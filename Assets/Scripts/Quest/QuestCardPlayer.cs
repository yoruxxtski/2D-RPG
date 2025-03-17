using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardPlayer : QuestCard
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private TextMeshProUGUI goldRewardTMP;
    [SerializeField] private TextMeshProUGUI expRewardTMP;

    [Header("Item")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    [Header("Quest Completed")]
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject rewardsPanel;
    void Update()
    {
        statusTMP.text = $"Status\n{QuestToComplete.CurrentStatus}/{QuestToComplete.QuestGoal}";
    }


    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n{quest.CurrentStatus}/{quest.QuestGoal}";
        goldRewardTMP.text = quest.GoldReward.ToString();
        expRewardTMP.text = quest.ExpReward.ToString();

        itemIcon.sprite = quest.questItemRewards.Item.Icon;
        itemQuantityTMP.text = quest.questItemRewards.Quantity.ToString();;
    }

    private void QuestCompletedCheck() {
        if (QuestToComplete.QuestCompleted) {
            claimButton.SetActive(true);
            rewardsPanel.SetActive(false);
        } 
    }

    public void ClaimQuest() {
        GameManager.Instance.AddPlayerExp(QuestToComplete.ExpReward);
        Inventory.Instance.AddItem(QuestToComplete.questItemRewards.Item,
         QuestToComplete.questItemRewards.Quantity);

        //  * Todo add coins
        CoinManager.Instance.AddCoins(QuestToComplete.GoldReward);
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        QuestCompletedCheck();
    }
}
