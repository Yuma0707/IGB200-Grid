using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialogue
{
    public string NPCName;
    public string[] lines;
}


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; 
    public TMP_Text dialogueText; 
    public TMP_Text NPCText;
    public Button closeButton; 
    public Button nextButton;
    public GameObject WinUI;

    private Queue<Dialogue> dialogues = new Queue<Dialogue>();
    public Coroutine typingMechanic;

    public UnityEngine.UI.Image NPCImage;
    public Sprite Raina;
    public Sprite Wattson;
    public Sprite Flo;
    public int Tut = 0;
    private static bool hasShownIntro = false;



    private void Start()
    {

        
        closeButton.onClick.AddListener(CloseDialogue);
        nextButton.onClick.AddListener(ShowNextLine);
        nextButton.gameObject.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Tutorial")
        {
            TutorialDialogue();
        }
        else if (currentScene.name == "Level 2")
        {
            FloDialogue();
        }
        else if (currentScene.name == "Level 3")
        {
            WattsonDialogue();
        }
        else if(currentScene.name == "LevelSelect" && hasShownIntro == false)
        {
            if (hasShownIntro)
            {
                dialoguePanel.SetActive(false);

            }
            else if (hasShownIntro == false)
            {
                IntroDialogue();
                
            }
            
        }
        Tut = 0;

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
                yield return new WaitForSeconds(0.05f); // Typing speed
            }


            nextButton.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && nextButton.gameObject.activeSelf);
            nextButton.gameObject.SetActive(false);
        }
        OnDialogueEnd();
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        if (typingMechanic != null) StopCoroutine(typingMechanic);
        hasShownIntro = true;

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
                "Ooh sorry busy day, how are you going? Good? Good.",
                "One thing that has changed since our interview was our implementation of mental health days,"
                "Which are separate from sick days, just thought I should let you know. Anyways,"
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
                "The grid is where all your planning will be visualised.",
                "On the left side of the screen you can see the buildings you have access to.",
                "The numbers next to each building displays how many of each building you have left to place.",
                "Buildings placed next to each other can grant bonuses",
                "For example houses and parks each grant 1 point when placed next to each other.",
                "The allocation of points its pretty simple, since living near parks boosts public mental health, you get a point. That sort of thing.",
                "Your score is shown up the top right of the screen, as well as the par score to beat each level",
                "And below that is the help button incase you get confused on what buildings give what points."


            }

        };
        StartDialogue(tutDialogue);
        NPCImage.sprite = Raina;
    }

    public void PostTutorialDialogue()
    {
        Tut = 1;
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
                "So you're the new architect right? Always nice to meet a new person, although I wish it were under better circumstances.",
                "Oh? Raina didn't tell you about Robert?",
                "Well I'm sure she had her reasons, he's no longer with us to put it lightly.",
                "We actually hired a psychologist to help with people readjusting after what happened.",
                "They should be in tomorrow, I'm sure they'll want to meet you and go through some strategies or something for dealing with stress.",
                "Anyways, just thought I'd introduce myself since we'll be working together on this project.",
                "Welcome and if people seem on edge, know its probably not because of you. Toodles."
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
                "I'm Wattson, the head sparkie.",
                "I see you're already amped up for this project haha.",
                "Who was Robert? Oh I'm guessing Flo told you about him.",
                "Well he and I weren't super close but, yeah when I heard about his passing I was a bit taken a back.",
                "But, you're here now and I don't want your first day to be doom and gloom.",
                "I'm sure over the next week or so, you'll fit right in.",
                "As you could probably tell, this place is less of a sausage fest than most",
                "Raina only hires based on skills over filling a quota. Yet, we are still a diverse bunch.",
                "Right'o, I'll let you get back to it. See you around."
            }
        };
        StartDialogue(wattsonDialogue);
        NPCImage.sprite = Wattson;
    }
    private void OnDialogueEnd()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Tutorial" && Tut == 1)
        {
            WinUI.SetActive(true);
        }
        CloseDialogue();
    }
}
