using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWITCHBrain : MonoBehaviour
{
    public int state = 0;
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
            switch (state)
            {
                case 0:
                    {
                        COLINControl.Get(collision).Hurt(5);
                        break;
                    }
                case 1:
                    {
                        COLINControl.Get(collision).Hurt(1);
                        break;
                    }
                case 2:
                    {
                        //nothing
                        break;
                    }
                case 3:
                    {
                        GameObject.Find("THE_ONE").transform.position = new Vector3(-66, 24.9f, 13);
                        break;
                    }
            }
        }
    }
}
