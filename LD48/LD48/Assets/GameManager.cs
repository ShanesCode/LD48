using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, bool> pickups = new Dictionary<string, bool>();

    private void Start()
    {
        LogInventory();
    }

    public void UpdatePickups(GameObject pickup)
    {
        pickups[pickup.name] = true;
        LogInventory();
    }

    private void LogInventory()
    {
        foreach (KeyValuePair<string, bool> entry in pickups)
        {
            Debug.Log("The pickup named: " + entry.Key + "\thas been picked up: " + entry.Value);
        }
    }
}
