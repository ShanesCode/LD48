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

    public GameObject powerBar;
    private int currentPower;
    private int maxPower;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.GetComponent<PlayerController>().maxHealth;
        healthBar.GetComponent<Slider>().maxValue = maxHealth;

        maxPower = player.GetComponent<PlayerController>().maxPower;
        powerBar.GetComponent<Slider>().maxValue = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Slider>().value = player.GetComponent<PlayerController>().currentHealth;
        powerBar.GetComponent<Slider>().value = player.GetComponent<PlayerController>().currentPower;
    }
}
