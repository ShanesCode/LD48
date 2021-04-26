using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sinkSpeed;
    public float moveSpeed;
    
    public int maxHealth;
    public int currentHealth;

    public int healthDrainAmount = 3;
    public float healthDrainTickLength = 1.0f;
    private float healthDrainTimer;

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

    [Range(0.1f, 3.0f)] [SerializeField] private float damageFlashDuration;
    private bool recentlyHit;
    private float flashTimer;
    [Range(2, 25)] [SerializeField] private int flashRate;
    private float flashTrigger;
    private int flashFlag;
    private Color spriteColor;

    private AudioSource audioSource;
    private AudioClip damageClip;
    private AudioClip nondamageClip;

    private bool alive;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.up * -30);
        submerged = false;
        facingRight = false;
        recentlyHit = false;
        flashTimer = damageFlashDuration;
        flashRate = 10;
        flashTrigger = flashTimer / flashRate;
        flashFlag = (int)Mathf.Floor(flashTimer / flashTrigger);
        spriteColor = gameObject.GetComponent<SpriteRenderer>().color;
        audioSource = GetComponent<AudioSource>();

        damageClip = Resources.Load<AudioClip>("SFX/zapsplat_impact_body_slam_hit_against_metal_surface_hard_002_43753") as AudioClip;
        nondamageClip = Resources.Load<AudioClip>("SFX/zapsplat_impacts_body_person_heavy_005_43768") as AudioClip;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        healthDrainTimer = healthDrainTickLength;
    }

    private void FixedUpdate()
    {
        Move(moveX * Time.fixedDeltaTime, moveY * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (submerged && alive)
        {
            moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveY = Input.GetAxisRaw("Vertical") * moveSpeed;
        } else
        {
            moveX = 0;
            moveY = 0;
        }

        if (recentlyHit)
        {
            flashOpacity();
        }

        if (alive)
        {
            if (healthDrainTimer >= 0)
            {
                healthDrainTimer -= Time.deltaTime;
            }
            else
            {
                if (currentHealth - healthDrainAmount > 0)
                {
                    currentHealth -= healthDrainAmount;
                    healthDrainTimer = healthDrainTickLength;
                }
                else
                {
                    Death();
                }
            }
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

    private bool IsSubmerged()
    {
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(transform.position, new Vector2(transform.localScale.x, transform.localScale.y), gameObject.GetComponent<CapsuleCollider2D>().direction, transform.rotation.z);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.layer == 3)//if object is on juice layer
            {
                return true;
            }
        }
        return false;
    }

    public void Move(float moveX, float moveY)
    {
        submerged = IsSubmerged();
        
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

    public void Damage(int damage, Vector3 bounceDirection)
    {
        if (!recentlyHit && damage != 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }
            else
            {
                Vector3 bounceVector = bounceDirection * bounceForce;
                rb2d.AddForce(bounceVector, ForceMode2D.Impulse);
                recentlyHit = true;
            }

            // Play sound
            audioSource.clip = damageClip;
            audioSource.Play();
        } else if (damage == 0)
        {
            // Walking into things which do no damage
            Vector3 bounceVector = bounceDirection * bounceForce;
            rb2d.AddForce(bounceVector, ForceMode2D.Impulse);

            audioSource.clip = nondamageClip;
            audioSource.Play();
        }
    }

    public void Death()
    {
        Debug.Log("Player is dead.");
        alive = false;
        gameManager.GetComponent<GameManager>().Death();
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

    public void Heal(int health)
    {
        if ((currentHealth + health) <= maxHealth)
        {
            currentHealth += health;
        } else
        {
            currentHealth = maxHealth;
        }
    }

    private void flashOpacity()
    {
        if (flashTimer >= 0)
        {
            if (flashFlag != (int)Mathf.Floor(flashTimer / flashTrigger))
            {
                if (spriteColor.a == 0.2f)
                {
                    spriteColor.a = 0.5f;
                }
                else
                {
                    spriteColor.a = 0.2f;
                }
                gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
                flashFlag = (int)Mathf.Floor(flashTimer / flashTrigger);
            }
            flashTimer -= Time.deltaTime;
        }
        else
        {
            spriteColor.a = 1;
            gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
            recentlyHit = false;
            flashTimer = damageFlashDuration;
        }
    }
}
