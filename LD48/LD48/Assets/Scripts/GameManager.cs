using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, bool> items = new Dictionary<string, bool>();

    private void Start()
    {
        LogInventory();
    }

    public void UpdateItems(GameObject item)
    {
        items[item.name] = true;
        LogInventory();
    }

    private void LogInventory()
    {
        foreach (KeyValuePair<string, bool> entry in items)
        {
            Debug.Log("The item named: " + entry.Key + "\thas been picked up: " + entry.Value);
        }
    }
}
