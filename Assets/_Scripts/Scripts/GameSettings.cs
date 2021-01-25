using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings {

    [RuntimeInitializeOnLoadMethod]
    static void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
