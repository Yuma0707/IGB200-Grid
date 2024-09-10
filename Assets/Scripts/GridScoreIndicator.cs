using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScoreIndicator : MonoBehaviour
{
    public int PartScore { get; private set; } = 0;

    private Text displayText;
    private TileInteractionManager interactionManager;

    void Start()
    {
        displayText = transform.Find("Display").GetComponent<Text>();
        interactionManager = FindObjectOfType<TileInteractionManager>();
        InvokeRepeating("CheckNeighborsAndUpdateScore", 1f, 1f);
    }

    private void CheckNeighborsAndUpdateScore()
    {
        GameObject closestPanel = FindClosestGameObjectWithTag("Panel");
        GameObject closestTile = FindClosestGameObjectWithTag("Tile");

        if (closestPanel != null && closestTile != null)
        {
            // Check if their positions match
            Vector2 panelPosition = closestPanel.GetComponent<RectTransform>().anchoredPosition;
            Vector2 tilePosition = closestTile.GetComponent<RectTransform>().anchoredPosition;

            if (Vector2.Distance(panelPosition, tilePosition) < 0.1f)
            {
                // Get tile types and calculate interaction score
                string tileType1 = closestPanel.GetComponentInChildren<Tile>().tileType;
                string tileType2 = closestTile.GetComponentInChildren<Tile>().tileType;
                PartScore = interactionManager.GetInteractionScore(tileType1, tileType2);
                displayText.text = PartScore.ToString();
            }
            else
            {
                PartScore = 0;
                displayText.text = "";
            }
        }
        else
        {
            PartScore = 0;
            displayText.text = "";
        }
    }

    private GameObject FindClosestGameObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        RectTransform myRectTransform = GetComponent<RectTransform>();
        Vector2 myPosition = myRectTransform.anchoredPosition;

        foreach (GameObject obj in objectsWithTag)
        {
            Vector2 objPosition = obj.GetComponent<RectTransform>().anchoredPosition;
            float distance = Vector2.Distance(myPosition, objPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
