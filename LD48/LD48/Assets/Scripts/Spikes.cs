using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PolygonCollider2D col;

    public int damage = 5;

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
        col.isTrigger = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {

            GameObject p = other.gameObject;
            //pass relative positions of player and floater
            Vector3 p_relativePosition = transform.InverseTransformPoint(p.transform.position);
            float vLength = (float)Mathf.Sqrt(Mathf.Pow(p_relativePosition.x, 2) + Mathf.Pow(p_relativePosition.y, 2));
            p_relativePosition.x = p_relativePosition.x / vLength;
            p_relativePosition.y = p_relativePosition.y / vLength;
            //Damage player
            p.GetComponent<PlayerController>().Damage(damage, p_relativePosition);
        }
    }
}
