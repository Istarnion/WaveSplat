using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 0.5f;

    public float maxSpeed = 20;
    public float drag = 1.2f;
    public float accel = 500;

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;

	void Start ()
    {
        position = transform.position;
	}
	
	void FixedUpdate ()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

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
        }
        else
        {
            velocity.x = velocity.y = 0;
        }
	}
}
