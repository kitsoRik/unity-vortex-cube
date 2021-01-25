using UnityEngine;

public static class VibrationManager
{
#if UNITY_ANDROID
    static AndroidJavaObject vibrator = null;
#endif
    static VibrationManager()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityPlayerActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        vibrator = unityPlayerActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
    }

    public static bool HasVibrator()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return vibrator.Call<bool>("hasVibrator");
#else
        return false;
#endif
    }

    public static void Cancel() // cancel of vibration
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (HasVibrator()) vibrator.Call("cancel");
#endif
    }

    public static void Vibrate(float time) // time of vibration in seconds
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(FloatToLongTime(time));
#endif
    }

    public static void Vibrate(float[] pattern, int repeate = -1)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        long[] longPattern = new long[pattern.Length];
        for (int x = 0; x < longPattern.Length; x += 1)
        {
            longPattern[x] = FloatToLongTime(pattern[x]);
        }
        Vibrate(longPattern, repeate);
#endif
    }

    public static void Vibrate(long[] pattern, int repeate = -1)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (HasVibrator()) vibrator.Call("vibrate", pattern, repeate);
#endif
    }

    public static void Vibrate(long time)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (HasVibrator()) vibrator.Call("vibrate", time);
#endif
    }

    static long FloatToLongTime(float time)
    {
        time *= 1000f;
        return (long)time;
    }
}