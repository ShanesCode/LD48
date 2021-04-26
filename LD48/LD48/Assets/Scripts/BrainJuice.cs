using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainJuice : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("SFX/zapsplat_vehicles_boat_gondola_oar_movement_water_003_25340") as AudioClip;
        audioSource.clip = clip;
        audioSource.volume = 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}
