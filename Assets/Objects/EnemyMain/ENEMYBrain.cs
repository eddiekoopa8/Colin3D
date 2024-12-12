using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMYBrain : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Rigidbody body;
    bool follow = MAINGame.IsEnemyFollow();
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newT = body.velocity;
        newT.y += Physics.gravity.y * (body.mass / 2);
        body.velocity = newT;
        follow = MAINGame.IsEnemyFollow();
        if (follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, 0.420f);
            transform.LookAt(GameObject.FindWithTag("Player").transform);
            transform.Rotate(new Vector3(0, 90));
        }

        if (!follow)
        {
            anim.Play("IDLE");
        }
        else
        {
            anim.Play("WALK");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (COLINControl.Verify(collision) && MAINGame.Health > 0)
        {
            body.velocity = new Vector3(0, 0, 0);
            if (COLINControl.Get(collision).Punching())
            {
                Debug.Log("PUNCH");
                Destroy(gameObject);
                COLINControl.PlaySnd("EnemySound");
                MAINGame.EnemyFollow(true);
            }
            else
            {
                COLINControl.Get(collision).Hurt();
            }
        }
    }
}
