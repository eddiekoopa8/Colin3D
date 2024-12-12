using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class STORYManager : MonoBehaviour
{
    public TMP_Text text;
    bool fading = false;

    public string[] storyScene;

    int waitTick = 0;
    int wait = 120;
    int typeWait = 2;

    int storyIdx = 0;
    int charIdx = 0;

    int state = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (storyIdx >= storyScene.Length && fading == false)
        {
            SCENEManager.ChangeScene("Scenes/Level01");

        }

        if (storyIdx >= storyScene.Length)
        {
            return;
        }

        switch (state)
        {
            case 0:
                {
                    text.text = "";
                    state++;
                    break;
                }
            case 1:
                {
                    if (waitTick++ >= typeWait)
                    {
                        text.text += storyScene[storyIdx][charIdx++];
                        if (charIdx >= storyScene[storyIdx].Length)
                        {
                            waitTick = 0;
                            state++;
                        }
                        waitTick = 0;
                    }
                    break;
                }
            case 2:
                {
                    if (waitTick++ >= wait)
                    {
                        state++;
                    }
                    break;
                }
            case 3:
                {
                    storyIdx++;
                    state = 0;
                    charIdx = 0;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SCENEManager.ChangeScene("Scenes/Level01");
        }
    }
}