using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class COLINControl : MonoBehaviour
{
    Animator anim;
    Rigidbody body;

    bool onGround = false;

    public Transform cameraPos;

    Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        anim.Play("IDLE");

        prevPos = transform.position;
    }

    void Translater(Vector3 a)
    {
        body.velocity += a;
        body.maxLinearVelocity = 500;
    }

    float rotPad = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 prevPos = transform.position;
        Vector3 newT = body.velocity;
        newT.y += Physics.gravity.y * (body.mass / 2);
        body.velocity = newT;

        bool standStill = (body.velocity.x >= -3 && body.velocity.x <= 3) && (body.velocity.z >= -3 && body.velocity.z <= 3);
        bool moving = !standStill;
        bool pressLeft = Input.GetKey(KeyCode.LeftArrow);
        bool pressRight = Input.GetKey(KeyCode.RightArrow);
        bool pressUp = Input.GetKey(KeyCode.UpArrow);
        bool pressDown = Input.GetKey(KeyCode.DownArrow);
        bool pressMove = pressLeft || pressRight || pressUp || pressDown;

        /*if (pressMove)
        {
            float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right for strafing
            float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down for forward/backward

            Vector3 move = new Vector3(moveX, 0, moveZ) * 5;
            move = transform.TransformDirection(move); // Adjust movement relative to player's direction
            Vector3 velocity = body.velocity;
            velocity.x = move.x;
            velocity.z = move.z;
            body.velocity = velocity;
        }*/

        if (pressUp)
        {
            rotPad = 0;
        }
        else if (pressDown)
        {
            rotPad = 180;
        }

        /*if (pressLeft)
        {
            rotPad = 270;
        }
        if (pressRight)
        {
            rotPad = 90;
        }*/

        Quaternion newRot;
        newRot.x = 0;
        newRot.z = 0;
        newRot.w = 0;
        newRot.y = rotPad;
        transform.rotation = newRot;

        if (pressMove)
        {
            float xmove = 0;
            float zmove = 0;
            if (pressUp || pressDown) zmove = 50;
            if (pressLeft || pressRight) xmove = 50;
            Vector3 move = new Vector3(xmove, 0, zmove) * 2;
            move = transform.TransformDirection(move); // Adjust movement relative to player's direction
            Vector3 velocity = body.velocity;
            velocity.x = move.x;
            velocity.z = move.z;
            body.velocity = velocity;
        }

        cameraPos.position += transform.position - prevPos;

        // animation
        if (standStill)
        {
            anim.Play("IDLE");
        }
        else
        {
            anim.Play("RUN");
        }

        if (transform.position.y <= -500)
        {
            Vector3 newPos = transform.position;
            newPos.y = 0;
            transform.position = newPos;
        }

        prevPos = transform.position;
    }
}
