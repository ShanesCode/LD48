using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBlob : MonoBehaviour
{
    private Animator anim;

    private AudioSource audioSource;
    private AudioClip clip;
    private AudioClip squishClip;

    public GameObject gameManager;

    private float endTimer = 2.0f;
    private bool squish;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        anim = GetComponent<Animator>();
        anim.SetBool("fall", false);

        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0.9f;
        clip = Resources.Load<AudioClip>("SFX/zapsplat_horror_gore_guts_bloody_squeeze_002_29269") as AudioClip;
        squishClip = Resources.Load<AudioClip>("SFX/zapsplat_horror_gore_impact_thud_bone_break_squelch_001_43679") as AudioClip;
        audioSource.clip = clip;

        squish = false;
    }

    private void Update()
    {
        if (squish)
        {
            endTimer -= Time.deltaTime;
        }

        if (endTimer <= 0)
        {
            gameManager.GetComponent<GameManager>().NextLevel();
        }
    }

    public void Fall()
    {
        audioSource.Play();
        // Animate Door
        anim.SetBool("fall", true);
    }

    public void Squish()
    {
        audioSource.clip = squishClip;
        audioSource.Play();
        squish = true;
    }
}
