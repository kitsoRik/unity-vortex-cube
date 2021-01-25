using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public static class SocialManager
{

    public static string UserName
    {
        get
        {
            return Social.localUser.userName;
        }
    }

    public static bool Authenticated
    {
        get
        {
#if !UNITY_EDITOR
            return Social.localUser.authenticated;
#else
            return false;
#endif
        }
    }

    public static void Activate(bool playGamesPlatform = true, bool authenticate = true)
    {
#if !UNITY_EDITOR
        if(playGamesPlatform)
        PlayGamesPlatform.Activate();
        if (authenticate)
            Authenticate();
#endif
    }

    public static void Authenticate(Action _action = null)
    {
#if !UNITY_EDITOR
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                if (_action != null)
                    _action();
            }
        });
#endif
    }

    public static void GetAchiv(string _id, double _progress, Action _action = null)
    {
#if !UNITY_EDITOR
        Social.ReportProgress(_id, _progress, (bool success) =>
        {
            if (success)
            {
                if (_action != null)
                    _action();
            }
        });
#endif
    }

    public static void GetAchiv(string _id, Action _action = null)
    {
#if !UNITY_EDITOR
        Social.ReportProgress(_id, 100.0d, (bool success) =>
        {
            if (success)
            {
                if (_action != null)
                    _action();
            }
        });
#endif
    }

    public static void ReportScore(string _id, long _value, Action _action = null)
    {
#if !UNITY_EDITOR
        Social.ReportScore(_value, _id, delegate (bool success)
        {
            if (success)
            {
                if (_action != null)
                    _action();
            }
        });
#endif
    }

    public static void ShopAchivments()
    {
#if !UNITY_EDITOR
        Social.ShowAchievementsUI();
#endif
    }

    public static void ShowLeaderboard()
    {
#if !UNITY_EDITOR
        Social.ShowLeaderboardUI();
#endif
    }

    public static void SignOut()
    {
#if !UNITY_EDITOR
        PlayGamesPlatform.Instance.SignOut();
#endif
    }
}
