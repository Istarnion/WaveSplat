﻿using System.Collections;
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

    [HideInInspector]
    public bool inControll;

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
        if (!inControll)
        {
            position.x = transform.position.x;
            position.y = transform.position.y;
            return;
        }

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

            var vel = new Vector2(0.5f * velocity.x * Time.fixedDeltaTime, 0);
            var sign = Mathf.Sign(vel.x);
            RaycastHit2D hitInfo = Physics2D.Raycast(
                position, vel, Mathf.Abs(vel.x)+radius);
            if(hitInfo)
            {
                vel.x = (hitInfo.distance-radius)*sign;
            }
            position.x += vel.x;

            vel = new Vector2(0, 0.5f * velocity.y * Time.fixedDeltaTime);
            sign = Mathf.Sign(vel.y);
            hitInfo = Physics2D.Raycast(
                position, vel, Mathf.Abs(vel.y)+radius);
            if(hitInfo)
            {
                vel.y = (hitInfo.distance-radius)*sign;
            }
            position.y += vel.y;

            transform.position = position;

            // Splats
            // TODO: Hook this up against the walking speed!
            splatCooldown += Time.fixedDeltaTime;
            if (splatCooldown >= splatTime)
            {
                AudioSource.PlayClipAtPoint(hitScale[noteIndex++], Vector3.zero);
                if (noteIndex == hitScale.Count) noteIndex = 0;
                splatCooldown = 0;
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
        }
	}

    void LoadSounds(string path, List<AudioClip> clips)
    {
        foreach(Object clip in Resources.LoadAll(path)) clips.Add((AudioClip) clip);     
    }
}
