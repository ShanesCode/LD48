using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    
    public GameObject healthBar;
    private int currentHealth;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.GetComponent<PlayerController>().maxHealth;
        healthBar.GetComponent<Slider>().maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Slider>().value = player.GetComponent<PlayerController>().currentHealth;
    }
}
