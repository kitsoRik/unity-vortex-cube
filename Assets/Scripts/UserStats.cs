#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class UserStats
{
    static UserStatsServer userStats;

    public static void Initialization(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(PostServer());
    }

    private static IEnumerator PostServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserName", SocialManager.UserName);
        UnityWebRequest www = UnityWebRequest.Post("https://testingrostik.000webhostapp.com/UserStats.php", form);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        Debug.Log("ID = " + Social.localUser.id);
        Debug.Log("NAME = " + Social.localUser.userName);
        userStats = JsonUtility.FromJson<UserStatsServer>(www.downloadHandler.text);
    }

    public static bool BlockAd
    {
        get
        {
            try
            {
                return userStats.blockAd;
            }catch(System.Exception)
            {
                return false;
            }
        }
    }
}

class UserStatsServer
{
    public bool blockAd;
}
#endif
