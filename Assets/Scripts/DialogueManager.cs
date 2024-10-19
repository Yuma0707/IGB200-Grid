using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue
{
    public string NPCName;
    public string[] lines;
}


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // Assign your Panel in the Inspector
    public TMP_Text dialogueText; // Assign your Text in the Inspector
    public TMP_Text NPCText;
    public Button closeButton; // Assign your Button in the Inspector
    public Button nextButton;

    private Queue<Dialogue> dialogues = new Queue<Dialogue>();
    private Coroutine typingMechanic;

    public UnityEngine.UI.Image NPCImage;
    public Sprite Raina;
    public Sprite Wattson;
    public Sprite Flo;


    private void Start()
    {

        closeButton.onClick.AddListener(CloseDialogue);
        dialoguePanel.SetActive(true);
        nextButton.onClick.AddListener(ShowNextLine);
        nextButton.gameObject.SetActive(false);

    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogues.Clear();
        dialogues.Enqueue(dialogue);
        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        Dialogue currentDialogue = dialogues.Dequeue();
        NPCText.text = currentDialogue.NPCName;
        nextButton.gameObject.SetActive(false);
        typingMechanic = StartCoroutine(TypeDialogue(currentDialogue.lines));
    }

    private IEnumerator TypeDialogue(string[] lines)
    {
        foreach (string line in lines)
        {

            dialogueText.text = ""; // Clear existing text


            foreach (char letter in line.ToCharArray())
            {
                dialogueText.text += letter; // Add one letter at a time
                yield return new WaitForSeconds(0.05f); // Adjust typing speed here
            }


            nextButton.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && nextButton.gameObject.activeSelf);
            nextButton.gameObject.SetActive(false);
        }
    }

    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        if (typingMechanic != null) StopCoroutine(typingMechanic);
    }

    public void OnNextButtonClicked()
    {
        ShowNextLine();
    }



    public void IntroDialogue()
    {
        Dialogue testDialogue = new Dialogue
        {
            NPCName = "Raina",
            lines = new string[]
            {
                "Oh hey, you must be the new city planner.",
                "Give me a moment, I wasn't expecting you so soon.",
                "And there we are, so first off. Hi, I'm Raina",
                "I'm the project manager for this new urban city remodel project.",
                "So lets get started.",
                "Ooh sorry busy day,",
                "I'll take you through your first assignment just to get any of those new place jitters out of ya."


            }

        };
        StartDialogue(testDialogue);
        NPCImage.sprite = Raina;

    }

    public void TutorialDialogue()
    {
        Dialogue tutDialogue = new Dialogue
        {
            NPCName = "Raina",
            lines = new string[]
            {
                "So here's what you'll be working with.",
                "The grid is where all the planning will be visualised.",
                "On the left side of the screen you can see the buildings you have access too",
                "Up here it shows on how many of each building you have left to place.",
                "Buildings placed next to each other can grant bonuses",
                "For example houses and parks each grant 1 point when placed next to each other.",
                "Your score is shown here, as well as the par score to beat each level"


            }

        };
        StartDialogue(tutDialogue);
        NPCImage.sprite = Raina;
    }

    public void PostTutorialDialogue()
    {
        Dialogue tut2Dialogue = new Dialogue
        {
            NPCName = "Raina",
            lines = new string[]
            {
                "Nice job, pretty stellar performance.",
                "I've gotta run but I'm sure some of the other team members will come and introduce themselves soon enough",
                "Ta Ta."


            }

        };
        StartDialogue(tut2Dialogue);
        NPCImage.sprite = Raina;
    }

    public void FloDialogue()
    {
        Dialogue floDialogue = new Dialogue
        {
            NPCName = "Flo",
            lines = new string[]
            {
                "Ooh heyya!",
                "You must be the newbie. I'm Flo",
                "I'm the head plumber working on this project.",
                "So you're the architect right? Interesting.",
                "If you need any help, just give me a shout. I'm always happy to tighten a valve.",
                "Anyways, just thought I'd introduce myself since we'll be working together on this project.",
                "Welcome and Good Luck. Toodles."


             }

        };
        StartDialogue(floDialogue);
        NPCImage.sprite = Flo;
    }
    public void WattsonDialogue()
    {
        Dialogue wattsonDialogue = new Dialogue
        {
            NPCName = "Wattson",
            lines = new string[]
            {
                "G'day champ.",
                "Saw you spark up a chat with Flo and figured I should introduce myself",
                "I'm Wattson.",
                "I see you're already amped up for this project haha.",
                "I'm the lead sparkie on the project, shocking I know.",
                "HAHAHAHA!",
                "I'm sure the rest of the department heads are either swamped already or too shy, especially that Mirra.",
                "Right'o, I'll see you around."
            }
        };
        StartDialogue(wattsonDialogue);
        NPCImage.sprite = Wattson;
    }
}