using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public float pulseLength = 3f;
    [Range(0.0f, 1.0f)] public float sizeChange = 0.2f;
    private float localSizeChange;
    private float pulseTimer;
    [SerializeField] private bool growing = true;
    private Vector3 originalSizeVector;
    private Vector3 maxSizeVector;
    private Vector3 sizeVector;

    private AudioSource audioSource;
    private AudioClip clip;

    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        pulseTimer = pulseLength;
        originalSizeVector = gameObject.transform.localScale;
        maxSizeVector = originalSizeVector * (1 + sizeChange);

        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("SFX/zapsplat_cartoon_squelch_squish_mouth_saliva_004_63698") as AudioClip;
        audioSource.clip = clip;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {
        Pulse();
    }

    private void Pulse()
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
            gameObject.transform.localScale = sizeVector;

            pulseTimer -= Time.deltaTime;
        }
        else
        {
            growing = !growing;
            pulseTimer = pulseLength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.Play();
            gameManager.GetComponent<GameManager>().NextLevel();
        }
    }
}
