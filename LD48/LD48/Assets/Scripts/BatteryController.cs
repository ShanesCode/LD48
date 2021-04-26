using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : Pickup
{
    public int healAmount = 5;
    public float healTickLength = 0.2f;
    private float healTickTimer;

    // Start is called before the first frame update
    public override void Start()
    {      
        base.Start();
        clip = Resources.Load<AudioClip>("SFX/jessey_drake_synth_theremin_robot_wobble_power_up_short_snth_jd") as AudioClip;
        highlight.GetComponent<SpriteRenderer>().color = new Color(255.0f / 255.0f, 155.0f / 255.0f, 0.0f, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<AudioSource>().clip = clip;
            gameManager.GetComponent<AudioSource>().loop = true;
            gameManager.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (healTickTimer >= 0)
            {
                healTickTimer -= Time.deltaTime;
            }
            else
            {
                other.GetComponent<PlayerController>().Heal(healAmount);
                healTickTimer = healTickLength;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<AudioSource>().loop = false;
        }
    }
}
