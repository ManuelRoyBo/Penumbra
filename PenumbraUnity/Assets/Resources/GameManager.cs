using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //Singleton variables
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    //Reference variables
    public Transform Player;
    public GameObject PlayerObj;
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

    /// <summary>
    /// Returns a vector from the obj pointing towards the mouse
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Vector3 PointTowardsMouse(GameObject obj)
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 playerPosition = new Vector2(obj.transform.position.x, obj.transform.position.y);

        Vector2 difference = mousePosition - playerPosition;
        double angleInRadian = Math.Atan2(difference.y, difference.x);
        Vector3 points = new Vector3((float)Math.Cos(angleInRadian), (float)Math.Sin(angleInRadian), 0);

        return points;
    }
}
