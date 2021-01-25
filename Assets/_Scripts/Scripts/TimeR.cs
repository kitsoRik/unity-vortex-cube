#pragma warning disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Globalization;

public class TimeR {

    private const string formatDateTimeFromServer = "yyyy-MM-dd-HH-mm-ss";

    private static float secondFromStart;
    private static bool isTime = false;

    private static DateTime _time;

	public static DateTime time
    {
        get
        {
            return _time.AddSeconds(Time.time - secondFromStart);
        }
    }

    public static Coroutine GetTime(MonoBehaviour monoBehaviour)
    {
        return monoBehaviour.StartCoroutine(GetTimeIE());
    }

    public static IEnumerator GetTimeIE()
    {
        if (isTime)
            yield break;
        bool tryMore = true;
        do
        {
            UnityWebRequest www = new UnityWebRequest("https://testingrostik.000webhostapp.com/time.php")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };
            try
            {
                yield return www.SendWebRequest();
                if(!(tryMore = !(www.error == null && !www.isHttpError && !www.isNetworkError)))
                {
                    secondFromStart = Time.time;
                    _time = DateTime.ParseExact(www.downloadHandler.text, formatDateTimeFromServer, CultureInfo.InvariantCulture);
                    isTime = true;
                }
            }finally { }
        } while (tryMore);
    }

    public static void End()
    {
        isTime = false;
    }
}
