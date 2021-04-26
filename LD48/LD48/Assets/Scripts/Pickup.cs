using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PolygonCollider2D col;
    [HideInInspector] public AudioClip clip;
    [HideInInspector] public GameObject gameManager;

    [HideInInspector] public GameObject highlight;
    
    private Color highlightColor;
    private float pulseLength = 0.5f;
    private float sizeChange = 0.2f;
    private float localSizeChange;
    private float pulseTimer;
    private bool growing = false;
    private Vector3 originalSizeVector;
    private Vector3 maxSizeVector;
    private Vector3 sizeVector;

    private void Awake()
    {
        highlightColor = new Color(255.0f / 255.0f, 0.0f / 255.0f, 230.0f / 255.0f, 0.3f);
}
    // Start is called before the first frame update
    public virtual void Start()
    {
        highlight = Instantiate(GameObject.FindGameObjectWithTag("PickupHighlight"));
        highlight.transform.localScale = new Vector3(highlight.transform.localScale.x * transform.localScale.x, highlight.transform.localScale.y * transform.localScale.y, highlight.transform.localScale.z * transform.localScale.z);
        highlight.transform.position = transform.position;
        highlight.GetComponent<SpriteRenderer>().sortingOrder = -1;
        highlight.GetComponent<SpriteRenderer>().color = highlightColor;
        pulseTimer = pulseLength;
        originalSizeVector = highlight.transform.localScale;
        maxSizeVector = originalSizeVector * (1 + sizeChange);

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

    private void Update()
    {
        Pulse(highlight);
    }

    public virtual void OnContact()
    {
        // Play pickup sound
        gameManager.GetComponent<AudioSource>().Play();

        gameObject.SetActive(false);
        highlight.SetActive(false);
    }

    private void Pulse(GameObject obj)
    {
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
            obj.transform.localScale = sizeVector;

            pulseTimer -= Time.deltaTime;
        }
        else
        {
            growing = !growing;
            pulseTimer = pulseLength;
        }

        obj.transform.Rotate(0, 0, 30 * Time.deltaTime);
    }
}
