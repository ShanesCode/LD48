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

    private Rigidbody2D rb2d;

    private bool submerged;
    public LayerMask juiceLM;
    
    float bounceForce = 5;

    private bool facingRight;

    [Range(0.1f, 1.5f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 velocity = Vector3.zero;

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
        Move(moveX * Time.fixedDeltaTime, moveY * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (submerged)
        {
            moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveY = Input.GetAxisRaw("Vertical") * moveSpeed;
        } else
        {
            moveX = 0;
            moveY = 0;
        }

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

    public void Move(float moveX, float moveY)
    {
        submerged = Submerged();
        
        if (!submerged)
        {
            if (moveY == 0)
            {
                moveY = -sinkSpeed / 100 * 5;
            }

            Vector3 targetVelocity = moveY * 100f * transform.up;
            // And then smoothing it out and applying it to the character
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, m_MovementSmoothing);
        } 
        else
        {
            // Move the character by finding the target velocity
            if (moveY == 0)
            {
                moveY = -sinkSpeed / 100;
            }

            Vector3 targetVelocity = moveX * 100f * transform.right + moveY * 100f * transform.up;
            // And then smoothing it out and applying it to the character
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (moveX > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (moveX < 0 && facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
    }

    public void Damage(int damage, Vector3 relativePos)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            Vector3 f = relativePos * bounceForce;
            rb2d.AddForce(f, ForceMode2D.Impulse);
        }
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
