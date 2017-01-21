using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 0.5f;

    public float maxSpeed = 20;
    public float drag = 1.2f;
    public float accel = 500;

    public float splatTime = 0.5f;
    private float splatCooldown = 0.5f;

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;

    private SplatManager splatter;

    private int noteIndex = 0;
    private List<AudioClip> hitScale;
    private List<AudioClip> levelComplete;

	void Start ()
    {
        position = transform.position;
        splatter = GameObject.Find("Splatter").GetComponent<SplatManager>();

        hitScale = new List<AudioClip>();
        levelComplete = new List<AudioClip>();
        LoadSounds("Audio/HitScale", hitScale);
        LoadSounds("Audio/LevelComplete", levelComplete);
	}
	
	void FixedUpdate ()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        input.Normalize();

        if (input.sqrMagnitude > 0)
        {
            acceleration = input * accel;
            velocity += acceleration * Time.deltaTime;
            if (velocity.sqrMagnitude > maxSpeed*maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }
            position += 0.5f * velocity * Time.fixedDeltaTime;
            transform.position = position;

            splatCooldown -= Time.fixedDeltaTime;
            if (splatCooldown <= 0)
            {
                AudioSource.PlayClipAtPoint(hitScale[noteIndex++], Vector3.zero);
                if (noteIndex == hitScale.Count) noteIndex = 0;
                splatCooldown = splatTime;
                splatter.SpawnSplat(transform.position, Utils.Map(
                    velocity.magnitude,
                    0, maxSpeed,
                    0, 1)
                );
            }
        }
        else
        {
            velocity.x = velocity.y = 0;
            splatCooldown = splatTime;
        }
	}

    void LoadSounds(string path, List<AudioClip> clips)
    {
        foreach(Object clip in Resources.LoadAll(path)) clips.Add((AudioClip) clip);     
    }
}
