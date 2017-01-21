using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatManager : MonoBehaviour {

    public GameObject splatPrefab;

    void Start()
    {
        SpawnSplat(Vector3.zero, 5);
    }

    public void SpawnSplat(Vector3 pos, float radius)
    {
        SplatScript splat = Instantiate(splatPrefab, transform).GetComponent<SplatScript>();
        splat.transform.position = pos;
        splat.maxRadius = radius;
    }
}
