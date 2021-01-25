using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public const int ID_REWARDED_NEXT_100 = 500;

    public static GameController gameController;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
#if UNITY_EDITOR
            Debug.Log("AwakeGM");
#endif
            InitPlayScene();
            gameController = GetComponent<GameController>();
        }
        else
        {
            InitLoadScene();
        }
    }

    private void InitLoadScene()
    {
        SocialManager.Activate();
    }

    private void InitPlayScene()
    {
#if UNITY_EDITOR
        Debug.Log("InitPlay");
#endif
        //        AdsR.Initialization();
        GameManagerVariables gameManagerVariables = s_code.FromSave("GameManager", "GameManager", new GameManagerVariables());
        Money = gameManagerVariables._money;
        TryMore = gameManagerVariables._tryMore;
        NowMission = gameManagerVariables._nowMission;

        StatisticsVariables statisticsVariables = s_code.FromSave("Statistics", "Statistics", new StatisticsVariables());
        GameController.statistics.BestScore = statisticsVariables._bestScore;
        GameController.statistics.PlayedGames = statisticsVariables._playedGames;
        GameController.statistics.PickupGemes = statisticsVariables._pickupGems;
        GameController.statistics.TotalScore = statisticsVariables._totalScore;

        SettingsVariables settingsVariables = s_code.FromSave("Settings", "Settings", new SettingsVariables());
        GameController.settings.IsAR = settingsVariables._isAR;
        GameController.settings.IsVibration = settingsVariables._vibrationIsOn;
        GameController.settings.SoundIsOn = settingsVariables._soundIsOn;
        GameController.settings.Language = Helper.SystemLanguageParse(settingsVariables._language);

        RewardedVariables rewardedVariables = s_code.FromSave("Rewarded", "Rewarded", new RewardedVariables());
        Rewarded.RewardedDay = rewardedVariables._rewardedDay;
        Rewarded.RewardedMoment = DateTime.Parse(rewardedVariables._rewardedMoment);

        ShopVariables shopVariables = s_code.FromSave("Shop", "Shop", new ShopVariables());
        Shop.ShopHasObjects = shopVariables._shopHasObjects;
        Shop.RandomCharacter = shopVariables._randomCharacter;

        Missions.missionStats = s_code.FromSave("Mission", "Mission", new MissionStats());

#if !UNITY_EDITOR
        Lang.Starting(GameController.settings.Language);
#else
        Lang.Starting(GameController.settings.Language, SystemLanguage.English, SystemLanguage.Russian, SystemLanguage.Ukrainian);
#endif
    }

    public static s_int Money;

    public static s_int TryMore;

    public static s_int NowMission;

    public static void Save()
    {
        if (SceneManager.GetActiveScene().name != "PlayScene")
            return;

        s_code.ToSave(new GameManagerVariables(Money, TryMore, NowMission), "GameManager", "GameManager");
        s_code.ToSave(new StatisticsVariables(GameController.statistics.PlayedGames, GameController.statistics.BestScore,
            GameController.statistics.PickupGemes, GameController.statistics.TotalScore), "Statistics", "Statistics");
        s_code.ToSave(new SettingsVariables(GameController.settings.SoundIsOn, GameController.settings.IsAR, GameController.settings.IsVibration, GameController.settings.Language), "Settings", "Settings");
        s_code.ToSave(new RewardedVariables(Rewarded.RewardedDay, Rewarded.RewardedMoment), "Rewarded", "Rewarded");
        s_code.ToSave(new ShopVariables(Shop.ShopHasObjects, Shop.RandomCharacter), "Shop", "Shop");
        s_code.ToSave(Missions.missionStats, "Mission", "Mission");
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        TimeR.End();
        SocialManager.SignOut();
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
            TimeR.End();
        }
        else
        {
             
        }
    }

#if UNITY_EDITOR

    [ContextMenu("DeleteAllKeys")]
    public void PlayerPrefs_DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

#endif
}

public class GameManagerVariables
{
    public int _money,
        _tryMore,
        _nowMission;

    public GameManagerVariables()
    {
        _money = 0;
        _tryMore = 0;
        _nowMission = 1;
    }

    public GameManagerVariables(int __money, int __tryMore, int __nowMission)
    {
        _money = __money;
        _tryMore = __tryMore;
        _nowMission = __nowMission;
    }
}
