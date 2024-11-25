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

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        anim.Play("IDLE");
    }

    void Translater(Vector3 a)
    {
        body.velocity += a;
        body.maxLinearVelocity = 500;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newT = body.velocity;
        newT.y += Physics.gravity.y * body.mass;
        body.velocity = newT;
        bool standStill = (body.velocity.x >= -25 && body.velocity.x <= 25) && (body.velocity.z >= -25 && body.velocity.z <= 25);

        // Ground check
        {
            onGround = Physics.CheckSphere(GameObject.Find("COLIN_GROUND_CHECKER").transform.position, 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            body.AddForce(new Vector3(0, -1000, 0));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //anim.Play("RUN");
            Translater(new Vector3(0, 0, 100));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //anim.Play("RUN");
            Translater(new Vector3(0, 0, -100));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //anim.Play("RUN");
            Translater(new Vector3(100, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //anim.Play("RUN");
            Translater(new Vector3(-100, 0));
        }

        cameraPos.position = new Vector3(transform.position.x, cameraPos.position.y, transform.position.z + 550);

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
    }
}
