using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingTextScript : MonoBehaviour
{
    private TextMesh textMesh;

    public Color color;
    public int number;

    private float timeToLive = 1.5f;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Start()
    {
        textMesh.color = color;
        textMesh.text = string.Format("{+0;-#}", number);
    }

    void Update()
    {
        var pos = transform.position;
        pos.y += Time.deltaTime * 10;

        transform.position = pos;

        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
