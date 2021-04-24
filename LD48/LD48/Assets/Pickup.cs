using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CircleCollider2D col;
    private GameObject gameManager;
    public string pickup_name;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Pickup";

        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        }
        rb2d.isKinematic = true;

        col = GetComponent<CircleCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        }
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().UpdatePickups(gameObject);

            // Play pickup sound
            //GetComponent<PlayerController>().audio.clip = player.GetComponent<PlayerController>().pickup;
            //GetComponent<PlayerController>().audio.Play();

            gameObject.SetActive(false);
        }
    }
}
