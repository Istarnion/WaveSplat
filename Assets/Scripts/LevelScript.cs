using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int numThingies;

    public float timeLimit;

    [HideInInspector]
    public float timeLeft;

    public Vector3 playerSpawnPoint;

    [HideInInspector]
    public int thingiesHit;

    void Awake()
    {
        timeLeft = timeLimit;
    }
}
