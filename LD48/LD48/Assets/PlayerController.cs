using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sinkForce;
    public float moveForce;
    public int playerHealth;

    public GameObject worldCenter;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce((gameObject.transform.position - worldCenter.transform.position) * -sinkForce, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
