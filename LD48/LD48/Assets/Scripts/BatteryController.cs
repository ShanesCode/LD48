using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : Pickup
{
    public int chargeAmount = 100;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        clip = Resources.Load<AudioClip>("SFX/jessey_drake_synth_theremin_robot_wobble_power_up_short_snth_jd") as AudioClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<AudioSource>().clip = clip;
            other.GetComponent<PlayerController>().Charge(chargeAmount);
            base.OnContact();
        }
    }
}
