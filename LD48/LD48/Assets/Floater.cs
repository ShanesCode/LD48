using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    Vector3 brainTransform;

    [Range(0.1f, 20f)] [SerializeField] float floatRange = 10.0f;
    [Range(0.1f, 5f)] [SerializeField] float floatSpeed = 2.0f;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex;
    private float distance_to_waypoint;
    private float min_distance_to_waypoint;

    // Start is called before the first frame update
    void Start()
    {
        brainTransform = GameObject.Find("Brain").transform.position;
        min_distance_to_waypoint = 0.5f;
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
            //add damage
            //add sfx
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
