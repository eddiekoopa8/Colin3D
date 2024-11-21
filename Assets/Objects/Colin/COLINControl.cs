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

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        anim.Play("IDLE");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.Play("RUN");
            transform.Translate(new Vector3(0, 0, 10));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.Play("RUN");
            transform.Translate(new Vector3(0, 0, -10));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.Play("RUN");
            transform.Translate(new Vector3(10, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.Play("RUN");
            transform.Translate(new Vector3(-10, 0));
        }

        body.velocity += Physics.gravity * body.mass;

        onGround = body.velocity.y == 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround) body.velocity = new Vector3(0, 150 * 10, 0);
            Debug.Log("body.velocity = " + body.velocity);
        }
    }
}
