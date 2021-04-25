using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshBlob : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PolygonCollider2D col;

    private void Start()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector3 bounceDirection = -contact.normal;

            //Damage player
            collision.collider.GetComponent<PlayerController>().Damage(0, bounceDirection);
        }
    }
}
