using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMover : MonoBehaviour
{
    public Transform entity;
    public Transform waypoints;

    private int waypointIndex;
    private int numWaypoints;

    public float speed = 10;

    void Awake()
    {
        numWaypoints = waypoints.childCount;
    }
	
	void Update ()
    {
        entity.transform.position = Vector3.MoveTowards(
            entity.transform.position,
            waypoints.GetChild(waypointIndex).transform.position,
            speed * Time.deltaTime);

        if (Vector3.Distance(entity.transform.position, waypoints.GetChild(waypointIndex).transform.position) < 0.01f)
        {
            waypointIndex = (waypointIndex + 1) % numWaypoints;
        }
	}
}
