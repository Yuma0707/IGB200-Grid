using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Or TMPro.TMP_Text if you're using TextMeshPro
using TMPro;

public class SubmitButtonHandler : MonoBehaviour
{
    // Public variable to assign the GameObject to update
    public GameObject scoreDisplayObject;

    private TMP_Text scoreDisplayText; // Or TMP_Text if using TextMeshPro

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
    }

    private void CalculateAndDisplayTotalScore()
    {
        int totalScore = 0;

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
    }
}