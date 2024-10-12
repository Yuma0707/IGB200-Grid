using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // Assign your Panel in the Inspector
    public TMP_Text dialogueText; // Assign your Text in the Inspector
    public Button closeButton; // Assign your Button in the Inspector

    private void Start()
    {
        
        closeButton.onClick.AddListener(CloseDialogue);
        dialoguePanel.SetActive(false); // Start with the panel hidden
    }

    public void ShowDialogue(string dialogue)
    {
        StartCoroutine(TypeDialogue(dialogue));
        dialoguePanel.SetActive(true);
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // Clear existing text
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }
    }

    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void TriggerTestDialogue()
    {
        ShowDialogue("This is a test of the dialogue system!");
    }
}