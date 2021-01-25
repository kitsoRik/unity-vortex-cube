using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{
    public BackGround backGround;
    public Animator animator;
    
    public void Exit()
    {
        backGround.Starting(true, true);
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause && GameController.Playing)
        {
            Exit();
        }
        else if (pause)
        {

        }
    }
}

