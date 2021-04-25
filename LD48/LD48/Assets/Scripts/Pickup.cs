using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PolygonCollider2D col;
    public AudioClip clip;
    public GameObject gameManager;
    // Start is called before the first frame update
    public virtual void Start()
    {
        gameObject.tag = "Pickup";

        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        }
        rb2d.isKinematic = true;

        col = GetComponent<PolygonCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        }
        col.isTrigger = true;
    }

    public virtual void OnContact()
    {
        // Play pickup sound
        gameManager.GetComponent<AudioSource>().Play();

        gameObject.SetActive(false);
    }
}
