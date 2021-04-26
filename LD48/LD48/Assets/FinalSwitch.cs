using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSwitch : MonoBehaviour
{
    public GameObject fleshBlob;

    private BoxCollider2D col;

    public bool pressed;
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
        col.isTrigger = true;

        pressed = false;

        anim = GetComponent<Animator>();
        anim.SetBool("pressed", pressed);

        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("SFX/zapsplat_household_heater_bathroom_switch_on") as AudioClip;
        audioSource.clip = clip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pressed)
        {
            fleshBlob.GetComponent<FinalBlob>().Fall();

            // Animate switch
            Press();
        }
    }

    public void Press()
    {
        pressed = true;
        audioSource.Play();
        anim.SetBool("pressed", pressed);
    }
}
