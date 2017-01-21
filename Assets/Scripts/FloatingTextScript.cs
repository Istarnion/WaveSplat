using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingTextScript : MonoBehaviour
{
    private TextMesh textMesh;

    public Color color;
    public int number;

    private float timeToLive = 0.75f;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Start()
    {
        textMesh.color = color;
        textMesh.text = number.ToString("+#;-#:0");
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
