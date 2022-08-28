using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Singleton variables
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    //Reference variables
    public Transform Player;
    [HideInInspector] public PlayerMovement PMovement;
    [HideInInspector] public PlayerAbilities PAbility;

    //UI variables
    public GameObject InteractText;

    //Dialogue variables
    private bool inDialogue = false;
    public GameObject DialogueObject;
    public TextMeshProUGUI dialogueTextArea;
    private Queue<string> currentDialogue = new Queue<string>();

    public void Awake()
    {
        //Singleton checks
        if (GameManager.Instance && GameManager.Instance!=this) { Destroy(this); }
        instance = this;
    }

    private void Start()
    {
        PMovement = Player.GetComponent<PlayerMovement>();
        PAbility = Player.GetComponent<PlayerAbilities>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && inDialogue) { ContinueDialogue(); }
    }

    public bool CanMove()
    {
        return !inDialogue;
    }
    public void SetInteractableText(bool toSet)
    {
        if (!InteractText) { return; }
        InteractText.SetActive(toSet);
    }

    //Dialogue Functions
    #region Dialogue

    public void PlayDialogue(Dialogue dialogue)
    {
        if (inDialogue || !DialogueObject || !dialogueTextArea) { return; }
        SetInteractableText(false);
        inDialogue = true;
        foreach (string i in dialogue.dialogues)
        {
            currentDialogue.Enqueue(i);
        }
        DialogueObject.SetActive(true);
    }
    public void ContinueDialogue()
    {
        if (currentDialogue.Count <= 0)
        {
            EndDialogue();
            return;
        }
        dialogueTextArea.text = currentDialogue.Dequeue();
    }
    private void EndDialogue()
    {
        dialogueTextArea.text = "";
        inDialogue = false;
        DialogueObject.SetActive(false);
    }

    #endregion
}
