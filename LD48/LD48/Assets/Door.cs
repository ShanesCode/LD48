using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private BoxCollider2D col;

    public bool open;
    private Animator anim;

    private AudioSource audioSource;
    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        }

        open = false;

        anim = GetComponent<Animator>();
        anim.SetBool("open", open);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        audioSource.spatialBlend = 0.9f;

        clip = Resources.Load<AudioClip>("SFX/zapsplat_horror_gore_guts_bloody_squeeze_002_29269") as AudioClip;
        audioSource.clip = clip;
    }

    public void Open()
    {
        open = true;
        audioSource.Play();
        // Animate Door
        anim.SetBool("open", open);
    }
}
