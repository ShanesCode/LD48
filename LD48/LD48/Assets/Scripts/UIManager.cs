using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    
    public GameObject healthBar;
    private int maxHealth;

    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject gameOverLabelAndButtons;
    [SerializeField] private float gameOverMenuDelay = 1.5f;
    private float gameOverMenuTimer;

    public AudioMixer mixer;
    public Slider volume;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.GetComponent<PlayerController>().maxHealth;
        healthBar.GetComponent<Slider>().maxValue = maxHealth;

        volume.value = PlayerPrefs.GetFloat("volume", 1.0f);
        volume.onValueChanged.AddListener(SetVolume);
        gameOverMenuTimer = gameOverMenuDelay;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Slider>().value = player.GetComponent<PlayerController>().currentHealth;

        if (gameOver.activeSelf)
        {
            if (gameOverMenuTimer >= 0)
            {
                gameOverMenuTimer -= Time.deltaTime;
            }
            else
            {
                gameOverLabelAndButtons.SetActive(true);
            }
        }
    }

    public void MissionFailed()
    {
        gameOver.SetActive(true);
        gameOverLabelAndButtons.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
    }

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("volume", sliderValue);
    }
}
