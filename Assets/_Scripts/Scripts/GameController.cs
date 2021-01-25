
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void LosesCause();
    public static LosesCause losesCause;
    public delegate void PlayingCause();
    public static PlayingCause playingCause;
    public delegate void ReplayCause();
    public static ReplayCause replayCause;
    public delegate void MainMenuCause();
    public static MainMenuCause mainMenuCause;
    public delegate void PlayHere();
    public static PlayHere playHereCause;
    public delegate void TryMorePlay();
    public static TryMorePlay tryMorePlayCause;
    public delegate void NoTryMorePlay();
    public static NoTryMorePlay noTryMorePlayCause;

    public static Missions missions;
    public static MainMenu mainMenu;
    public static Shop shop;
    public static Control control;
    public static Creating creating;
    public static BackGround backGround;
    public static Statistics statistics;
    public static Settings settings;
    public static LoseS loseS;
    public static TryMoreS tryMoreS;

    public static int Score;

    public static int PowOfTryMoreNumber;

    public static bool Playing;

    public static bool Lose { get; set; }

    public static int PickupGemsInGame;

    public static bool missionComplete;

    private void Awake()
    {

        mainMenu = GameObject.Find("Main menu").GetComponent<MainMenu>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        control = GameObject.Find("Player").GetComponent<Control>();
        creating = GameObject.Find("GameManager").GetComponent<Creating>();
        backGround = GameObject.Find("BackGround").GetComponent<BackGround>();
        statistics = GameObject.Find("Statistics").GetComponent<Statistics>();
        settings = GameObject.Find("Settings").GetComponent<Settings>();
        loseS = GameObject.Find("Lose").GetComponent<LoseS>();
        missions = GameObject.Find("Missions").GetComponent<Missions>();
        tryMoreS = loseS.transform.GetChild(1).GetComponent<TryMoreS>();

        settings.StartAwake();

        mainMenuCause += AfterMainMenu;
        replayCause += AfterReplay;
        playingCause += Play;
        losesCause += Losing;
        tryMorePlayCause += AfterTryMore;
        noTryMorePlayCause += AfterLose;

        AdR.Initialization(this);
    }

    public void Play()
    {
        Playing = true;
        PowOfTryMoreNumber = 0;
    }

    public void Losing()
    {
        if (GameManager.TryMore >= (int)Mathf.Pow(2, PowOfTryMoreNumber))
        {
            tryMoreS.ShowButton();
        }
        else
        {
            AfterLose();
        }
        if (settings.IsVibration)
        VibrationManager.Vibrate(0.7f);
        backGround.Exit();
        Playing = false;

    }

    public void AfterTryMore()
    {
        GameManager.TryMore -= (int)Mathf.Pow(2, PowOfTryMoreNumber++);
        mainMenu.SetTryMore(GameManager.TryMore);
        backGround.Starting();
    }

    public static void AfterLose()
    {
        Lose = true;

        missions.AddComplete(GameManager.NowMission);
        if (missionComplete = missions.CheckComplete(GameManager.NowMission))
        {
            SocialManager.GetAchiv(GPGSIds.achievement_all_missions, GameManager.NowMission / (double)Missions.MAX_MISSIONS);
            missions.SetComplete(++GameManager.NowMission);
        }

        statistics.PlayedGames++;
        statistics.PickupGemes += PickupGemsInGame;
        statistics.TotalScore += Score;


        loseS.Starting();
    }

    public void AfterReplay()
    {
        Lose = false;
        Score = 0;
        PickupGemsInGame = 0;

        GameObject[] destroyGameObjects = GameObject.FindGameObjectsWithTag("HamperParent");
        foreach (GameObject destroyGameObject in destroyGameObjects)
        {
            Destroy(destroyGameObject);
        }

        destroyGameObjects = GameObject.FindGameObjectsWithTag("CrystalParent");
        foreach (GameObject destroyGameObject in destroyGameObjects)
        {
            Destroy(destroyGameObject);
        }
        Play();
    }

    public void AfterMainMenu()
    {
        Lose = false;
        Score = 0;
        PickupGemsInGame = 0;

        GameObject[] destroyGameObjects = GameObject.FindGameObjectsWithTag("HamperParent");
        foreach (GameObject destroyGameObject in destroyGameObjects)
        {
            Destroy(destroyGameObject);
        }

        destroyGameObjects = GameObject.FindGameObjectsWithTag("CrystalParent");
        foreach (GameObject destroyGameObject in destroyGameObjects)
        {
            Destroy(destroyGameObject);
        }

        if (missionComplete)
        {
             missions._animation.Play("MissionCompleted");
        }
    }
}
