using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteractionManager : MonoBehaviour
{
    // Dictionary to store interactions. Key is a combination of two tile types, 
    // value is the interaction score
    public Dictionary<string, int> tileInteractions = new Dictionary<string, int>();

    // Example interactions (you'll need to fill this in for all tile types)
    void Start()
    {
        
        tileInteractions.Add("House_House", 0); // Houses next to each other are bad
        tileInteractions.Add("House_Park", 1);  // Houses near parks are good
        tileInteractions.Add("House_Dump", -1);
        tileInteractions.Add("House_Factory", -1);
        tileInteractions.Add("Dump_Park", -1);
        tileInteractions.Add("Dump_Factory", 1);
        tileInteractions.Add("Park_Factory", 0);
        tileInteractions.Add("Dump_Dump", 0);
        tileInteractions.Add("Factory_Factory", 0);
        tileInteractions.Add("Park_Park", 0);
        // ... add more interactions here

    }

    // Function to get the interaction score between two tiles
    public int GetInteractionScore(string tileType1, string tileType2)
    {
        string key1 = tileType1 + "_" + tileType2;
        string key2 = tileType2 + "_" + tileType1; // Check both combinations

        if (tileInteractions.ContainsKey(key1))
            return tileInteractions[key1];
        else if (tileInteractions.ContainsKey(key2))
            return tileInteractions[key2];
        else
            return 3; // No interaction defined
    }
}
