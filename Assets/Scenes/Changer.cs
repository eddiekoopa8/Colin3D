using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeNext()
    {
        SCENEManager.ChangeScene("Scenes/Level01");
    }

    public void ToOptions()
    {
        SCENEManager.ChangeScene("Scenes/Menu");
    }

    public void ToTitle()
    {
        SCENEManager.ChangeScene("Scenes/Title");
    }

    public void Goodbye()
    {
        SCENEManager.ExitGame();
    }
}
