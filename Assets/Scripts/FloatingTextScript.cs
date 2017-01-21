using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingTextScript : MonoBehaviour
{
    private TextMesh textMesh;

    private Color color;
    public int number;

    public float timeToLive = 0.3f;
    public float speed = 5;

    public Color positiveColor = Color.black;
    public Color negativeColor = Color.black;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Start()
    {
        if (number < 0)
        {
            color = negativeColor;
        }
        else
        {
            color = positiveColor;
        }
        textMesh.color = color;
        textMesh.text = number.ToString("+#;-#0");
    }

    void Update()
    {
        var pos = transform.position;
        pos.y += Time.deltaTime * speed;

        transform.position = pos;

        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
