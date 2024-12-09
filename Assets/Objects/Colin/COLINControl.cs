using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class COLINControl : MonoBehaviour
{
    Animator anim;
    Rigidbody body;

    bool onGround = false;

    public Transform cameraPos;

    Vector3 prevPos;

    public string sceneName;

    public static bool Verify(GameObject obj)
    {
        return obj.GetComponent<COLINControl>() != null;
    }

    public static bool Verify(Collision obj)
    {
        return obj.gameObject.GetComponent<COLINControl>() != null;
    }

    public static COLINControl Get(GameObject obj)
    {
        return obj.GetComponent<COLINControl>();
    }

    public static COLINControl Get(Collision obj)
    {
        return obj.gameObject.GetComponent<COLINControl>();
    }

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

    public bool Punching()
    {
        return punching;
    }

    bool hurt = false;
    bool hurtKnockDone = false;
    int hurtTick = 0;
    int hurtTimer = 20;
    bool hurtInvins = false;
    bool hurtInvinsDone = false;
    int hurtInvTick = 0;
    int hurtInvTimer = 50;

    public void Hurt()
    {
        if (hurtInvins == false && hurt == false)
        {
            Debug.Log("HURT");
            hurt = true;
            hurtKnockDone = false;
            hurtTick = 0;
            hurtInvins = false;
            hurtInvinsDone = false;
            hurtInvTick = 0;

            Vector3 move = new Vector3(0, 0, -9f) * 29;
            move = transform.TransformDirection(move); // Adjust movement relative to player's direction
            Vector3 velocity = body.velocity;
            velocity.x = move.x;
            velocity.z = move.z;
            body.velocity = velocity;

            anim.playbackTime = 0;
        }
    }

    bool punching = false;
    bool punchDone = false;
    int punchTick = 0;
    int punchTimer = 15;

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
        bool pressPunch = Input.GetKeyDown(KeyCode.LeftControl);

        if (pressPunch && !punching && !hurt)
        {
            punching = true;
            punchTick = 0;
            punchDone = false;
            anim.playbackTime = 0;
            anim.Play("PUNCH");

            Vector3 move = new Vector3(0, 0, 7.5f) * 29;
            move = transform.TransformDirection(move); // Adjust movement relative to player's direction
            Vector3 velocity = body.velocity;
            velocity.x = move.x;
            velocity.z = move.z;
            body.velocity = velocity;
        }

        if (pressMove && !punching && !hurt)
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

        // animation
        if (!punching && !hurt)
        {
            if (standStill)
            {
                anim.Play("IDLE");
            }
            else
            {
                anim.Play("RUN");
            }
        }
        if(punching)
        {
            anim.Play("PUNCH");
        }
        if (hurt)
        {
            anim.Play("KNOCK");
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

    private void FixedUpdate()
    {
        if (punching && !punchDone)
        {
            if (punchTick++ >= punchTimer)
            {
                punchDone = true;
                punching = false;
            }
        }

        if (hurt && !hurtKnockDone)
        {
            if (hurtTick++ >= hurtTimer)
            {
                hurtKnockDone = true;
                hurt = false;
                hurtInvins = true;
                hurtInvinsDone = false;
                Debug.Log("HURT INV");
            }
        }

        if (hurtInvins && !hurtInvinsDone && hurtKnockDone)
        {
            if (hurtInvTick++ >= hurtInvTimer)
            {
                hurtKnockDone = true;
                hurt = false;
                hurtInvins = false;
                hurtInvinsDone = true;

                Debug.Log("HURT DONE");
            }
        }
    }
}
