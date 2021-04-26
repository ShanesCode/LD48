using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, bool> items = new Dictionary<string, bool>();
    private GameObject UIManager;

    private bool paused;
    private bool dead;

    private void Start()
    {
        dead = false;
        UIManager = GameObject.FindGameObjectWithTag("UIManager");

        LogInventory();
        paused = false;
    }

    private void Update()
    {
        if (!dead)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }
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

    public void Death()
    {
        if (!dead)
        {
            UIManager.GetComponent<UIManager>().MissionFailed();
            dead = true;
        }
    }

    public void TogglePause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            UIManager.GetComponent<UIManager>().Pause();
            paused = true;
        } 
        else
        {
            Time.timeScale = 1;
            UIManager.GetComponent<UIManager>().Unpause();
            paused = false;
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadYourAsyncScene(SceneManager.GetActiveScene().name));
    }

    public void QuitToMenu()
    {
        StartCoroutine(LoadYourAsyncScene("MainMenuScene"));
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            StartCoroutine(LoadYourAsyncScene("Level2"));
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            StartCoroutine(LoadYourAsyncScene("VictoryScene"));
        }
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
