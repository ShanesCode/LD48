using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    Vector3 brainTransform;

    [Range(0.1f, 5f)] [SerializeField] float floatSpeed = 2.0f;
    public int damage = 5;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex;
    private float distance_to_waypoint;
    private float min_distance_to_waypoint;

    // Start is called before the first frame update
    void Start()
    {
        brainTransform = GameObject.Find("Brain").transform.position;
        min_distance_to_waypoint = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = RotateToWorldCenter(brainTransform);
        Float();

        distance_to_waypoint = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (distance_to_waypoint < min_distance_to_waypoint)
        {
            IncreaseIndex();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.name == "Player"){

            GameObject p = other.gameObject;
            //pass relative positions of player and floater
            Vector3 p_relativePosition = transform.InverseTransformPoint(p.transform.position);
            float vLength = (float)Math.Sqrt(Math.Pow(p_relativePosition.x, 2) + Math.Pow(p_relativePosition.y, 2));
            p_relativePosition.x = p_relativePosition.x / vLength;
            p_relativePosition.y = p_relativePosition.y / vLength;
            //Damage player
            p.GetComponent<PlayerController>().Damage(damage, p_relativePosition);    
        }
    }

    void Float()
    {
        float increment = Time.deltaTime * floatSpeed;
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, increment);
    }

    Quaternion RotateToWorldCenter(Vector3 worldCenter)
    {
        Vector2 vecToWorldCenter = transform.position - worldCenter;
        return Quaternion.LookRotation(Vector3.forward, vecToWorldCenter);
    }

    void IncreaseIndex()
    {
        waypointIndex++;

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }

        Float();
    }
}
