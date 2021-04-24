using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sinkSpeed;
    public float moveSpeed;
    
    public int playerHealth;

    public GameObject worldCenter;

    private float moveX;
    private float moveY;
    private Vector2 moveVec;
    
    public float maxSpeed;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            moveY = Input.GetAxis("Vertical") * moveSpeed;
        }
        else
        {
            if (moveY > -sinkSpeed)
            {
                // Sink
                moveY -= sinkSpeed / 100;
            } else
            {
                moveY += sinkSpeed / 100;
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            moveX = Input.GetAxis("Horizontal") * moveSpeed;
        }
        else
        {
            if (moveX != 0)
            {
                if (moveX < 0)
                {
                    moveX += maxSpeed / 100;
                }
                else
                {
                    moveX -= maxSpeed / 100;
                }
            }
        }

        moveVec = moveX * transform.right + moveY * transform.up;
        rb2d.AddForce(moveVec, ForceMode2D.Force);
        RotateToWorldCenter();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 v1 = moveX * transform.right;
        Gizmos.DrawLine(transform.position, transform.position + v1);

        Gizmos.color = Color.blue;
        Vector3 v2 = moveY * transform.up;
        Gizmos.DrawLine(transform.position, transform.position + v2);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, worldCenter.transform.position);
    }

    void RotateToWorldCenter()
    {
        Vector2 vecToWorldCenter = transform.position - worldCenter.transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, vecToWorldCenter);
        transform.rotation = rotation;
    }
}
