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
    // Start is called before the first frame update
    void Start()
    {
        pulseTimer = pulseLength;
        originalSizeVector = gameObject.transform.localScale;
        maxSizeVector = originalSizeVector * (1 + sizeChange);
    }

    private void Update()
    {
        Pulse();
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
