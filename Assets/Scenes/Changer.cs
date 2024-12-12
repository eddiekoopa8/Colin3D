using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SettingsMain.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeNext()
    {
        SCENEManager.ChangeScene("Scenes/Story");
    }

    public void ToOptions()
    {
        SettingsMain.Save();
        SettingsMain.Load();
        SCENEManager.ChangeScene("Scenes/Menu");
    }

    public void ToTitle()
    {
        SettingsMain.Save();
        SettingsMain.Load();
        SCENEManager.ChangeScene("Scenes/Title");
    }

    public void Goodbye()
    {
        SCENEManager.ExitGame();
    }
}
