using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatManager : MonoBehaviour {

    public GameObject splatPrefab;

    public float maxSplatRadius = 5;

    public Color[] colors;
    private int colorIndex = 0;

    private Vector3 offset = new Vector3(0, 0, 5);

    public void SpawnSplat(Vector3 pos, float radius)
    {
        SplatScript splat = Instantiate(splatPrefab, transform).GetComponent<SplatScript>();
        offset.x = pos.x;
        offset.y = pos.y;
        offset.z -= 0.01f;
        splat.transform.position = offset;
        splat.maxRadius = radius * maxSplatRadius;
        splat.splatColor = colors[colorIndex++ % colors.Length];
    }

    public void ClearSplats()
    {
        int childCount = transform.childCount;
        for(int i=0; i<childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
