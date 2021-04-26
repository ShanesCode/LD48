using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VictoryManager : MonoBehaviour
{
    public GameObject video;
    private VideoPlayer vp;

    private void Start()
    {
        vp = video.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Each time we reach the end, we slow down the playback by a factor of 10.
        //vp.loopPointReached += EndReached;
        if (vp.frame == (long)vp.frameCount - 1)
        {
            Application.Quit();
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
