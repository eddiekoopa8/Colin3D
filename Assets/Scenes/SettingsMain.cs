using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMain : MonoBehaviour
{
    [System.Serializable]
    public class set_data
    {
        public int _0;
        public int _1;
    }
    // Start is called before the first frame update
    public static int MusicVolume = 50;
    public static int SoundVolume = 50;
    public static void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "meta.json");
        Debug.Log("filePath: " + filePath);
        if (!File.Exists(filePath))
        {
            MusicVolume = 50;
            SoundVolume = 50;
            return;
        }
        set_data setData = JsonUtility.FromJson<set_data>(File.ReadAllText(filePath));
        MusicVolume = setData._0;
        SoundVolume = setData._1;
    }
    public static void Save()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "meta.json");
        set_data setData = new set_data
        {
            _0 = MusicVolume,
            _1 = SoundVolume,
        };
        File.WriteAllText(filePath, JsonUtility.ToJson(setData, false));
        Debug.Log("filePath: " + filePath);
    }

    void Start()
    {
        SettingsMain.Load();
        GameObject.Find("MusSlide").GetComponent<Slider>().value = (float)(MusicVolume / 100);
        GameObject.Find("SndSlide").GetComponent<Slider>().value = (float)(SoundVolume / 100);
    }

    void Exit()
    {
        MusicVolume = Convert.ToInt32(GameObject.Find("MusSlide").GetComponent<Slider>().value * 100);
        SoundVolume = Convert.ToInt32(GameObject.Find("SndSlide").GetComponent<Slider>().value * 100);
        SettingsMain.Save();
    }
}
