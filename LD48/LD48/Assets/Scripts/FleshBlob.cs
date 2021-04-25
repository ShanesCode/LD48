using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshBlob : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PolygonCollider2D col;

    public float pulseLength = 3f;
    [Range(0.0f, 1.0f)] public float sizeChange = 0.2f;
    private float localSizeChange;
    private float pulseTimer;
    [SerializeField] private bool growing = true;
    private Vector3 originalSizeVector;
    private Vector3 maxSizeVector;
    private Vector3 sizeVector;

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

        pulseTimer = pulseLength;
        originalSizeVector = gameObject.transform.localScale;
        maxSizeVector = originalSizeVector * (1 + sizeChange);
    }

    private void Update()
    {
        Pulse();
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

    private void Pulse()
    {
        //Debug.Log("pulseTimer: " + pulseTimer);
        localSizeChange = (pulseLength - pulseTimer) / pulseLength * sizeChange;
        if (pulseTimer >= 0)
        {
            if (growing)
            {
                sizeVector = originalSizeVector * (1 + localSizeChange);
            }
            else
            {
                sizeVector = maxSizeVector / (1 + localSizeChange);
            }
            gameObject.transform.localScale = sizeVector;

            pulseTimer -= Time.deltaTime;
        }
        else
        {
            growing = !growing;
            pulseTimer = pulseLength;
        }
    }
}
