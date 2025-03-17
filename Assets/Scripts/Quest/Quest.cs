using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
   [Header("Info")]
   public string Name;
   public string ID;
   public int QuestGoal;
   [Header("Description")]
   [TextArea] public string Description;

   [Header("Reward")]
   public int GoldReward;
   public float ExpReward;
   public QuestItemReward questItemRewards;


   [HideInInspector] public int CurrentStatus; //* Compare with QuestGoal
   [HideInInspector] public bool QuestCompleted;
   [HideInInspector] public bool QuestAccepted;

   public void AddProgress(int amount) {
        CurrentStatus += amount;
        if (CurrentStatus >= QuestGoal) {
            CurrentStatus = QuestGoal;
            QuestIsCompleted();
        }
   }
   private void QuestIsCompleted() {
        if (QuestCompleted) {
            return;
        }
        QuestCompleted = true;
   }

   public void ResetQuest() {
        QuestAccepted = false;
        QuestCompleted = false;
        CurrentStatus = 0;
   }
}


[Serializable]
public class QuestItemReward {
    public InventoryItems Item;
    public int Quantity;

}
