using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, bool> pickups = new Dictionary<string, bool>()
        {
            { "Pickup0", false },
            { "Pickup1", false },
            { "Pickup2", false },
            { "Pickup3", false },
            { "Pickup4", false },
            { "Pickup5", false },
            { "Pickup6", false },
            { "Pickup7", false },
        };

    public void UpdatePickups(GameObject pickup)
    {
        pickups[pickup.GetComponent<Pickup>().pickup_name] = true;
    }
}
