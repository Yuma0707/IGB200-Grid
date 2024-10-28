using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Or TMPro.TMP_Text if you're using TextMeshPro
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class SubmitButtonHandler : MonoBehaviour
{
    // Public variable to assign the GameObject to update
    public GameObject scoreDisplayObject;
    public int playertartgetscore = 0;
    public GameObject WinUI;
    public int tutorialscore= 4;
    public int level1_score = 16;
    public int level2_score = 45;
    public int level3_score = 50;
    public GameObject LoseUI;

    private TMP_Text scoreDisplayText; // Or TMP_Text if using TextMeshPro

    private DialogueManager dialogueManager;
    public GameObject dialogueButton;

    void Start()
    {
        // Get the Text/TMP_Text component from the assigned GameObject
        if (scoreDisplayObject != null)
        {
            scoreDisplayText = scoreDisplayObject.GetComponent<TMP_Text>(); // Or TMP_Text
        }
        else
        {
            Debug.LogError("Score display object not assigned in SubmitButtonHandler!");
        }


        // Get the Button component and add a listener to its onClick event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CalculateAndDisplayTotalScore);

        dialogueManager = GameObject.Find("Manager").GetComponent<DialogueManager>();
        //dialogueButton = GameObject.Find("NextLine");
        Scene currentScene = SceneManager.GetActiveScene();

    }

    private void CalculateAndDisplayTotalScore()
    {
        int totalScore = 0;


        Scene currentScene = SceneManager.GetActiveScene();

        // Find all GridScoreIndicator objects in the scene
        GridScoreIndicator[] indicators = FindObjectsOfType<GridScoreIndicator>();

        // Sum their PartScore values
        foreach (GridScoreIndicator indicator in indicators)
        {
            totalScore += indicator.PartScore;
        }

        // Update the score display 
        if (scoreDisplayText != null)
        {
            scoreDisplayText.text = totalScore.ToString();
        }

        if(totalScore >= playertartgetscore && currentScene.name != "Tutorial")
        {
            
            WinUI.SetActive(true);
            if(totalScore>=level1_score && totalScore < level2_score)
            {
                PlayerPrefs.SetInt("Level1Completed", 1);
                PlayerPrefs.Save();
            }
            if(totalScore >= level2_score && totalScore < level3_score)
            {
                PlayerPrefs.SetInt("Level2Completed", 1);
                PlayerPrefs.Save();
            }
            if(totalScore >= level3_score)
            {
                PlayerPrefs.SetInt("Level3Completed", 1);
                PlayerPrefs.Save();
            }
            
        }

        if(totalScore < playertartgetscore)
        {
            LoseUI.SetActive(true);
        }

        if(totalScore >= playertartgetscore && currentScene.name == "Tutorial")
        {
            dialogueManager.PostTutorialDialogue();
            
        }
    }
}