using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    float currentY;

    [Range(0.1f, 5f)] [SerializeField] float floatRange = 1.0f;
    [Range(0.1f, 2f)] [SerializeField] float floatSpeed = 0.5f;
    bool travellingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        currentY = 0;
        Vector3 brainTransform = GameObject.Find("Brain").transform.position;
        transform.rotation = RotateToWorldCenter(brainTransform);
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.name == "Player"){
            Debug.Log(gameObject.name + " had a collision with the player!");
            //add damage
            //add sfx
        }
    }

    void Float()
    {
        float increment = Time.deltaTime * floatSpeed;
        if (travellingUp)
        {
            gameObject.transform.Translate(Vector3.up * increment);
            currentY += increment;
        }
        else
        {
            gameObject.transform.Translate(Vector3.down * increment);
            currentY -= increment;
        }

        if ((currentY > floatRange) || (currentY < -floatRange))
        {
            travellingUp = !travellingUp;
        }
    }

    Quaternion RotateToWorldCenter(Vector3 worldCenter)
    {
        Vector2 vecToWorldCenter = transform.position - worldCenter;
        return Quaternion.LookRotation(Vector3.forward, vecToWorldCenter);
    }
}
