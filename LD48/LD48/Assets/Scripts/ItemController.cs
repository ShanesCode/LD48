using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Pickup
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        gameManager.GetComponent<GameManager>().items.Add(gameObject.name, false);
        clip = Resources.Load<AudioClip>("SFX/zapsplat_multimedia_alert_single_note_mallet_synth_exclamation_001_25554") as AudioClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<AudioSource>().clip = clip;
            gameManager.GetComponent<GameManager>().UpdateItems(gameObject);
            base.OnContact();
        }
    }
}
