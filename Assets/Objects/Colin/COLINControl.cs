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

    public string sceneName;

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

    static void turnAround(string gameObj, float value)
    {
        Quaternion newRot = new Quaternion(-90, value,0,0);
        GameObject.Find(gameObj).transform.rotation = newRot;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (pressUp)
        {
            turnAround("COLINBody", 0);
        }
        else if (pressDown)
        {
            turnAround("COLINBody", 180);
        }
        else if (pressLeft)
        {
            turnAround("COLINBody", 90);
        }
        else if (pressRight)
        {
            turnAround("COLINBody", 270);
        }

        if (pressMove)
        {
            float moveX = Input.GetAxis("Horizontal") * 2.5f; // A/D or Left/Right
            float moveZ = Input.GetAxis("Vertical") * 2.5f; // W/S or Up/Down

            Vector3 move = new Vector3(moveX, 0, moveZ) * 65;
            move = transform.TransformDirection(move); // Adjust movement relative to player's direction
            Vector3 velocity = body.velocity;
            velocity.x = move.x;
            velocity.z = move.z;
            body.velocity = velocity;
        }

        cameraPos.position += transform.position - prevPos;

        Quaternion newRot = transform.rotation;
        newRot.y = cameraPos.rotation.y;
        transform.rotation = newRot;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TELEPORTER"))
        {
            SCENEManager.ChangeScene(sceneName);
        }
    }
}
