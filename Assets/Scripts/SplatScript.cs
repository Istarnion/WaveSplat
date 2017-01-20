using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SplatScript : MonoBehaviour {

    MeshFilter meshFilter;

    private float radius = 0;
    public float maxRadius = 5;

    [Range(0.01f, 0.5f)]
    public float perlinStepSize = 0.01f;

    public Color[] colors;
    private Color splatColor;
    private float perlinStart;

    public float animationScale = 80;

	void Start()
    {
        splatColor = colors[Random.Range(0, colors.Length)];
        perlinStart = Random.value*1000.0f;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            radius = 0;
            splatColor = colors[Random.Range(0, colors.Length)];
            perlinStart = Random.value*1000.0f;
        }

        if (radius < maxRadius)
        {
            if ((radius += Time.deltaTime*animationScale) > maxRadius) radius = maxRadius;
            GenerateMesh();
        }
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        verts.Capacity = 361;
        verts.Add(Vector3.zero);
        float thetaInc = 2 * Mathf.PI / 360.0f;
        float perlinState = perlinStart;
        for(int i=1; i<verts.Capacity; ++i)
        {
            perlinState += perlinStepSize;

            verts.Add(new Vector3(
                Mathf.Cos(i*thetaInc)*radius*Map(Mathf.PerlinNoise(perlinState, 0), 0, 1, 0.5f, 1.2f),
                Mathf.Sin(i*thetaInc)*radius*Map(Mathf.PerlinNoise(perlinState, 0), 0, 1, 0.5f, 1.2f),
                0f
            ));
        }

        List<int> indices = new List<int>();
        for(int i=1; i<360; ++i)
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

    float Map(float t, float mina, float maxa, float minb, float maxb)
    {
        return (t - mina) * (maxb - minb) / (maxa - mina) + minb;
    }
}
