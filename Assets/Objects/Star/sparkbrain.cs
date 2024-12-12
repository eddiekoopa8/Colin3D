using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkbrain : MonoBehaviour
{
    public bool isGold = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,4));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (COLINControl.Verify(collision) && MAINGame.Health > 0)
        {
            COLINControl.PlaySnd("CollectSound");
            if (isGold)
            {
                MAINGame.GoldStars++;
            }
            else
            {
                MAINGame.IncStar(1);
            }

            if (MAINGame.GoldStars >= 3)
            {
                SCENEManager.ChangeScene("Scenes/DEMOEND");
            }
            else
            {

                Destroy(gameObject);
            }
        }
    }
}
