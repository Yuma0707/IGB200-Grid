using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to switch scenes
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text totalScoreText; // UI element to display Score
    public int parScore = 16; // Percentage Score Settings
    private GridScoreIndicator[] allScoreIndicators;
    private int totalScore;

    void Start()
    {
        InvokeRepeating("UpdateTotalScore", 0.2f, 0.2f); // Total score updated every second
    }

    void UpdateTotalScore()
    {
        totalScore = 0; // Reset total score

        // Retrieve GridScoreIndicator each time
        allScoreIndicators = FindObjectsOfType<GridScoreIndicator>();

        // Aggregate PartScore for all GridScoreIndicator
        foreach (GridScoreIndicator indicator in allScoreIndicators)
        {
            totalScore += indicator.PartScore;
        }

        // Total score reflected in UI
        totalScoreText.text = totalScore.ToString();
        Debug.Log("Total Score‚ðXV’†: " + totalScore);
    }

    /*
    // Function called when the SUBMIT button is pressed
    public void OnSubmit()
    {
        // Process of switching scenes by comparing the PAR score to the current score
        if (totalScore >= parScore)
        {
            Debug.Log("You win!");
            SceneManager.LoadScene("Win");
        }
        else
        {
            Debug.Log("You lose!");
            SceneManager.LoadScene("Lose");
        }
    }
    */

}
