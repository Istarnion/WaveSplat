using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SplatScript : MonoBehaviour {

    MeshFilter meshFilter;

    private float radius = 0;

    [HideInInspector]
    public float maxRadius = 5;

    [Range(0.01f, 0.5f)]
    public float perlinStepSize = 0.01f;
    public float animationScale = 80f;

    [HideInInspector]
    public Color splatColor;
    private float perlinStart;

	void Start()
    {
        perlinStart = Random.value*1000.0f;
	}

    void Update()
    {
        if (radius < maxRadius)
        {
            if ((radius += Time.deltaTime*animationScale) > maxRadius) radius = maxRadius;
            GenerateMesh();
        }
        else
        {
            Destroy(this); // Remove this script so it won't spend update() calls
        }
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        verts.Capacity = 181;
        verts.Add(Vector3.zero);
        float thetaInc = 2 * Mathf.PI / (verts.Capacity-1.0f);
        float perlinState = perlinStart;
        for(int i=1; i<verts.Capacity; ++i)
        {
            perlinState += perlinStepSize;
            float length = radius * Utils.Map(Mathf.PerlinNoise(perlinState, 0), 0, 1, 0.5f, 1.2f);
            float x = Mathf.Cos(i * thetaInc);
            float y = Mathf.Sin(i * thetaInc);

            // TODO: Add layer mask if needed
            var hitInfo = Physics2D.Raycast(transform.position, new Vector2(x, y), length);
            if(hitInfo)
            {
                length = hitInfo.distance;
                if (hitInfo.collider.CompareTag("Thingy"))
                {
                    var thingy = hitInfo.collider.GetComponent<ThingyScript>();
                    thingy.Hit();
                }
            }

            verts.Add(new Vector3(
                Mathf.Cos(i*thetaInc)*length,
                Mathf.Sin(i*thetaInc)*length,
                0f
            ));
        }

        List<int> indices = new List<int>();
        for(int i=1; i<verts.Count-1; ++i)
        {
            indices.Add(0);
            indices.Add(i);
            indices.Add(i+1);
        }
        indices.Add(0);
        indices.Add(verts.Count-1);
        indices.Add(1);

        mesh.vertices = verts.ToArray();
        mesh.triangles = indices.ToArray();

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = splatColor;
    }
}
