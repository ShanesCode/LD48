using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sinkSpeed;
    public float moveSpeed;
    
    public int playerHealth;

    public GameObject worldCenter;
    public GameObject brainJuice;

    private float moveX;
    private float moveY;
    private Vector2 moveVec;
    
    public float maxMoveSpeed;
    public float maxSpeed;

    private Rigidbody2D rb2d;

    private bool submerged;
    public LayerMask juiceLM;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.up * -30);
        submerged = true;
        facingRight = false;
    }

    private void FixedUpdate()
    {
        submerged = Submerged();
    }

    // Update is called once per frame
    void Update()
    {
        if (submerged)
        {
            maxSpeed = maxMoveSpeed;
        } else
        {
            maxSpeed = maxMoveSpeed * 5;
        }

        Debug.Log("Facing right: " + facingRight);
        if (Input.GetAxis("Vertical") != 0 && submerged)
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

        if (Input.GetAxis("Horizontal") != 0 && submerged)
        {
            moveX = Input.GetAxis("Horizontal") * moveSpeed;
        }
        else
        {
            if (Mathf.Abs(moveX) > 0.1)
            {
                if (moveX < 0)
                {
                    if (facingRight)
                    {
                        Flip();
                    }
                    moveX += maxSpeed / 100;
                }
                else
                {
                    if (!facingRight)
                    {
                        Flip();
                    }
                    moveX -= maxSpeed / 100;
                }
            } else
            {
                moveX = 0;
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

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward);
    }

    void RotateToWorldCenter()
    {
        Vector2 vecToWorldCenter = transform.position - worldCenter.transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, vecToWorldCenter);
        transform.rotation = rotation;
    }

    private bool Submerged()
    {
        bool submerged_ = true;
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(brainJuice.transform.position, new Vector2(brainJuice.transform.localScale.x * 0.95f, brainJuice.transform.localScale.y * 0.95f), brainJuice.GetComponent<CapsuleCollider2D>().direction, brainJuice.transform.rotation.z);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == gameObject)
            {
                submerged_ = true;
                break;
            }
            else
            {
                submerged_ = false;
            }
        }
        return submerged_;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
