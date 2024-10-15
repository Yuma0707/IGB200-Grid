using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    public GameObject helpPanel;

    public void OnHelpButtonClick()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(!helpPanel.activeSelf);
        }
    }
    public void CloseHelpPanel()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
        }
    }
}
