using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [Header("Config")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image NpcIcon;
    [SerializeField] private TextMeshProUGUI NpcNameTMP;
    [SerializeField] private TextMeshProUGUI NpcDialogueTMP;
    public NPCInteraction NPCSelected { get; set; }
    private bool dialogueStarted;
    private PlayerActions actions;
    private Queue<string> dialogueQueue = new Queue<string>();
    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    void Start()
    {
        actions.Dialogue.Interact.performed += ctx => ShowDialogue();
        actions.Dialogue.Continue.performed += ctx => ContinueDialogue();
    }
    private void LoadDialogueFromNPC() {
        if (NPCSelected.DialogueToShow.Dialogue.Length <= 0) return;
        foreach (string sentence in NPCSelected.DialogueToShow.Dialogue) {
            dialogueQueue.Enqueue(sentence);
        }
    }

    private void ShowDialogue() {
        if (NPCSelected == null) return;
        if (dialogueStarted) return;
        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();
        NpcIcon.sprite = NPCSelected.DialogueToShow.Icon;
        NpcNameTMP.text = NPCSelected.DialogueToShow.Name;
        NpcDialogueTMP.text = NPCSelected.DialogueToShow.Greeting;
        dialogueStarted = true;
    }

    private void ContinueDialogue() {
        if (NPCSelected == null) {
            dialogueQueue.Clear();
            return;
        } 
        if (dialogueQueue.Count <= 0) {
            CloseDialoguePanel();
            dialogueStarted = false;
            if (NPCSelected.DialogueToShow.HasInteraction) {
                OnExtraInteractionEvent?.Invoke(NPCSelected.DialogueToShow.interactionType);
            }
            return;
        }

        NpcDialogueTMP.text = dialogueQueue.Dequeue();
    }

    public void CloseDialoguePanel() {
        dialogueStarted = false;
        dialoguePanel.SetActive(false);
        dialogueQueue.Clear();
    }

    void OnEnable()
    {
        actions.Enable();
    }
    void OnDisable()
    {
        actions.Disable();
    }
}
