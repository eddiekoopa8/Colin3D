using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRATEBrian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (COLINControl.Verify(collision))
        {
            if (COLINControl.Get(collision).Punching())
            {
                Debug.Log("PUNCH");
                Destroy(gameObject);
                MAINGame.EnemyFollow(true);
            }
        }
    }
}
