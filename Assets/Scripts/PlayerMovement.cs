using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 0.5f;

    public float runSpeed = 25;
    public float walkSpeed = 15;
    public float drag = 1.2f;
    public float accel = 500;

    public float splatTime = 0.5f;
    private float splatCooldown = 0.5f;

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;

    private SplatManager splatter;
    private AudioManager audioManager;

    [HideInInspector]
    public bool inControll;

	void Start ()
    {
        position = transform.position;
        splatter = GameObject.Find("Splatter").GetComponent<SplatManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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

        float speed = Input.GetAxis("Jump") > 0 ? runSpeed : walkSpeed;

        if (input.sqrMagnitude > 0)
        {
            acceleration = input * accel;
            velocity += acceleration * Time.deltaTime;
            if (velocity.sqrMagnitude > speed*speed)
            {
                velocity.Normalize();
                velocity *= speed;
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
                audioManager.PlaySplat();
                splatCooldown = 0;
                splatter.SpawnSplat(transform.position, Utils.Map(
                    velocity.magnitude,
                    0, runSpeed,
                    0, 1)
                );
            }
        }
        else
        {
            velocity.x = velocity.y = 0;
        }
	}
}
