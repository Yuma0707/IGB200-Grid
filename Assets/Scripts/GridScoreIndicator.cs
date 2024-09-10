using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridScoreIndicator : MonoBehaviour
{
    public int PartScore { get; private set; } = 0;

    public GameObject displayObject;

    private TMP_Text displayText;
    private TileInteractionManager interactionManager;

    void Start()
    {
        if (displayObject != null)
        {
            displayText = displayObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError("Display object not assigned in GridScoreIndicator!");
        }

        interactionManager = FindObjectOfType<TileInteractionManager>();
        InvokeRepeating("CheckNeighborsAndUpdateScore", 1f, 1f);
    }

    private void CheckNeighborsAndUpdateScore()
    {
        GameObject closestPanel1 = FindClosestGameObjectWithTag("Panel");
        GameObject closestPanel2 = FindAnotherClosestPanel(closestPanel1);

        if (closestPanel1 != null && closestPanel2 != null)
        {
            GameObject tile1 = FindClosestGameObjectWithTagAtPosition("Tile", closestPanel1.transform.position);
            GameObject tile2 = FindClosestGameObjectWithTagAtPosition("Tile", closestPanel2.transform.position);

            if (tile1 != null && tile2 != null)
            {
                // Check if tile positions match their corresponding panel positions
                if (tile1.transform.position == closestPanel1.transform.position &&
                    tile2.transform.position == closestPanel2.transform.position)
                {
                    // Get tile types and calculate interaction score
                    string tileType1 = tile1.GetComponent<Tile>().tileType;
                    string tileType2 = tile2.GetComponent<Tile>().tileType;
                    PartScore = interactionManager.GetInteractionScore(tileType1, tileType2);
                    displayText.text = PartScore.ToString();
                }
                else
                {
                    // Tiles are not perfectly placed on the panels
                    PartScore = 0;
                    displayText.text = "";
                }
            }
            else
            {
                // One or both panels don't have tiles on them
                PartScore = 0;
                displayText.text = "";
            }
        }
        else
        {
            // Couldn't find two panels close to this indicator
            PartScore = 0;
            displayText.text = "";
        }
    }

    private GameObject FindClosestGameObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        Vector3 myPosition = transform.position;

        foreach (GameObject obj in objectsWithTag)
        {
            Vector3 objPosition = obj.transform.position;
            float distance = Vector3.Distance(myPosition, objPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }

    private GameObject FindAnotherClosestPanel(GameObject firstPanel)
    {
        GameObject[] allPanels = GameObject.FindGameObjectsWithTag("Panel");
        GameObject secondClosestPanel = null;
        float closestDistance = Mathf.Infinity;

        Vector3 myPosition = transform.position; // Position of the GridScoreIndicator

        foreach (GameObject panel in allPanels)
        {
            // Skip if this panel is the same as the first panel OR if its position matches the first panel's
            if (panel == firstPanel || panel.transform.position == firstPanel.transform.position)
                continue;

            Vector3 panelPosition = panel.transform.position;
            float distance = Vector3.Distance(myPosition, panelPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                secondClosestPanel = panel;
            }
        }

        return secondClosestPanel;
    }

    private GameObject FindClosestGameObjectWithTagAtPosition(string tag, Vector3 targetPosition)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objectsWithTag)
        {
            float distance = Vector3.Distance(targetPosition, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
