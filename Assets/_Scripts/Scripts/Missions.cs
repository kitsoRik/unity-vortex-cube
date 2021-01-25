using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Missions : MonoBehaviour {

    public const int MAX_MISSIONS = 50;

    public Animation _animation;
    public TextMeshProUGUI MissionText, MissionNumberText;
    public static MissionStats missionStats;
    public static TypeMission typeMission;

    void Awake()
    {
        _animation = GetComponent<Animation>();
        MissionText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        MissionNumberText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public string GetMissionText(int number)
    {
        switch (number)
        {
            case 1: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 25, 1, Lang.Phrase("game"));
            case 2: return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"), 
                Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 3: return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
                (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 4: return Lang.Phrase("Get new record");
            case 5: return Lang.Phrase("Playing to meddium stage of game");
            case 6: return Lang.Phrase("Playing to high stage of game");
            case 7: return Lang.Phrase("Playing to ultra high stage of game");
            case 8: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 75, 1, Lang.Phrase("game"));
            case 9:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 10:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 11: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 90, 1, Lang.Phrase("game"));
            case 12:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 13:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 14: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 100, 1, Lang.Phrase("game"));
            case 15:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 16:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 17: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 125, 1, Lang.Phrase("game"));
            case 18:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 19:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 20: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 130, 1, Lang.Phrase("game"));
            case 21:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 22:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 23: return Lang.Phrase("Get new record");
            case 24:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 25:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 26: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 135, 1, Lang.Phrase("game"));
            case 27:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 28:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 29: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 140, 1, Lang.Phrase("game"));
            case 30:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 31:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 32: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 150, 1, Lang.Phrase("game"));
            case 33:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 34:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 35: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 160, 1, Lang.Phrase("game"));
            case 36:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 37:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 38: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 170, 1, Lang.Phrase("game"));
            case 39:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 40:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 41: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 180, 1, Lang.Phrase("game"));
            case 42:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 43:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 44: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 190, 1, Lang.Phrase("game"));
            case 45:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 46:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 47: return string.Format(Lang.Phrase("Get score {0} in {1} {2}"), 200, 1, Lang.Phrase("game"));
            case 48:
                return string.Format(Lang.Phrase("Ride {0}/{1} degrees"),
            (int)Helper.SummFromArray(missionStats.has), missionStats.need);
            case 49:
                return string.Format(Lang.Phrase("Pickup {0}/{1} gems in {2}/{3} {4}"),
            Helper.SummFromArray(missionStats.has), missionStats.need, missionStats.hasGames, missionStats.has.Length, Lang.Phrase("games"));
            case 50: return Lang.Phrase("Get new record");
        }
        return "Out of range mission";
    }

    public void SetMissionTexts()
    {
        typeMission = GetTypeMission(GameManager.NowMission);
        if (GameManager.NowMission <= MAX_MISSIONS)
        {
            MissionText.text = GetMissionText(GameManager.NowMission);
            MissionNumberText.text = string.Format(Lang.Phrase("Mission #{0}"), GameManager.NowMission);
        }
        else
        {
            MissionText.transform.parent.gameObject.SetActive(false);
        }
    }

    public bool CheckComplete(int numberMission)
    {
        switch (numberMission)
        {
            case 1:
                if (GameController.Score > 25)
                {
                    return true;
                } break;
            case 2:
                return CheckComplete_InFewGames();
            case 3:
                return CheckComplete_InInfinityGames();
            case 4:
                if (GameController.Score > GameController.statistics.BestScore)
                {
                    return true;
                }
                break;
            case 5: 
                if(Creating.complexityCreated == Complexity.Meddium || Creating.complexityCreated == Complexity.High || Creating.complexityCreated == Complexity.UltraHigh)
                {
                    return true;
                } break;
            case 6:
                if (Creating.complexityCreated == Complexity.High || Creating.complexityCreated == Complexity.UltraHigh)
                {
                    return true;
                }
                break;
            case 7:
                if (Creating.complexityCreated == Complexity.UltraHigh)
                {
                    return true;
                }
                break;
            case 8:
                if (GameController.Score > 75)
                {
                    return true;
                }
                break;
            case 9:
                return CheckComplete_InInfinityGames();
            case 10:
                return CheckComplete_InFewGames();
            case 11:
                if (GameController.Score > 90)
                {
                    return true;
                }
                break;
            case 12:
                return CheckComplete_InInfinityGames();
            case 13:
                return CheckComplete_InFewGames();
            case 14:
                if (GameController.Score > 100)
                {
                    return true;
                }
                break;
            case 15:
                return CheckComplete_InInfinityGames();
            case 16:
                return CheckComplete_InFewGames();
            case 17:
                if (GameController.Score > 125)
                {
                    return true;
                }
                break;
            case 18:
                return CheckComplete_InInfinityGames();
            case 19:
                return CheckComplete_InFewGames();
            case 20:
                if (GameController.Score > 130)
                {
                    return true;
                }
                break;
            case 21:
                return CheckComplete_InInfinityGames();
            case 22:
                return CheckComplete_InFewGames();
            case 23:
                if (GameController.Score > GameController.statistics.BestScore)
                {
                    return true;
                }
                break;
            case 24:
                return CheckComplete_InInfinityGames();
            case 25:
                return CheckComplete_InFewGames();
            case 26:
                if (GameController.Score > 135)
                {
                    return true;
                }
                break;
            case 27:
                return CheckComplete_InInfinityGames();
            case 28:
                return CheckComplete_InFewGames();
            case 29:
                if (GameController.Score > 140)
                {
                    return true;
                }
                break;
            case 30:
                return CheckComplete_InInfinityGames();
            case 31:
                return CheckComplete_InFewGames();
            case 32:
                if (GameController.Score > 150)
                {
                    return true;
                }
                break;
            case 33:
                return CheckComplete_InInfinityGames();
            case 34:
                return CheckComplete_InFewGames();
            case 35:
                if (GameController.Score > 160)
                {
                    return true;
                }
                break;
            case 36:
                return CheckComplete_InInfinityGames();
            case 37:
                return CheckComplete_InFewGames();
            case 38:
                if (GameController.Score > 170)
                {
                    return true;
                }
                break;
            case 39:
                return CheckComplete_InInfinityGames();
            case 40:
                return CheckComplete_InFewGames();
            case 41:
                if (GameController.Score > 180)
                {
                    return true;
                }
                break;
            case 42:
                return CheckComplete_InInfinityGames();
            case 43:
                return CheckComplete_InFewGames();
            case 44:
                if (GameController.Score > 190)
                {
                    return true;
                }
                break;
            case 45:
                return CheckComplete_InInfinityGames();
            case 46:
                return CheckComplete_InFewGames();
            case 47:
                if (GameController.Score > 200)
                {
                    return true;
                }
                break;
            case 48:
                return CheckComplete_InInfinityGames();
            case 49:
                return CheckComplete_InFewGames();
            case 50:
                if (GameController.Score > GameController.statistics.BestScore)
                {
                    return true;
                }
                break;

        }
        return false;
    }

    private bool CheckComplete_InFewGames()
    {
        float sum = 0;
        for (int i = 0; i < missionStats.has.Length; i++)
        {
            sum += missionStats.has[i];
        }

        if (sum >= missionStats.need)
        {
            return true;
        }
        missionStats.has[0] = 0;
        return false;
    }

    private bool CheckComplete_InInfinityGames()
    {
        if (missionStats.has[0] >= missionStats.need)
        {
            return true;
        }
        return false;
    }

    public void AddComplete(int numberMission)
    {
        switch (numberMission)
        {
            case 2:
                if(missionStats.hasGames < missionStats.games - 1)
                missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 3:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 10:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 9:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 13:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 12:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 16:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 15:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 19:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 18:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 22:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 21:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 25:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 24:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 27:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 26:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 31:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 30:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 34:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 33:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 37:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 36:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 40:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 39:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 43:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 42:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 46:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 45:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
            case 49:
                if (missionStats.hasGames < missionStats.games - 1)
                    missionStats.hasGames++;
                System.Array.Sort(missionStats.has);
                missionStats.has[0] = GameController.PickupGemsInGame;
                System.Array.Sort(missionStats.has);
                break;
            case 48:
                missionStats.has[0] += Control.typeMissionHelper;
                break;
        }
    }

    public void SetComplete(int numberMission)
    {
        switch (numberMission)
        {
            case 2:
                missionStats.games = 3; // count games
                missionStats.need = 7; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 3:
                missionStats.need = 15000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 10:
                missionStats.games = 4; // count games
                missionStats.need = 10; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 9:
                missionStats.need = 20000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 13:
                missionStats.games = 3; // count games
                missionStats.need = 10; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 12:
                missionStats.need = 25000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 16:
                missionStats.games = 3; // count games
                missionStats.need = 11; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 15:
                missionStats.need = 30000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 19:
                missionStats.games = 4; // count games
                missionStats.need = 16; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 18:
                missionStats.need = 35000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 22:
                missionStats.games = 7; // count games
                missionStats.need = 25; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 21:
                missionStats.need = 40000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 25:
                missionStats.games = 5; // count games
                missionStats.need = 20; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 24:
                missionStats.need = 45000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 28:
                missionStats.games = 2; // count games
                missionStats.need = 5; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 27:
                missionStats.need = 50000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 31:
                missionStats.games = 1; // count games
                missionStats.need = 5; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 30:
                missionStats.need = 55000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 34:
                missionStats.games = 4; // count games
                missionStats.need = 20; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 33:
                missionStats.need = 60000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 37:
                missionStats.games = 5; // count games
                missionStats.need = 25; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 36:
                missionStats.need = 65000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 40:
                missionStats.games = 7; // count games
                missionStats.need = 35; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 39:
                missionStats.need = 70000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 43:
                missionStats.games = 6; // count games
                missionStats.need = 35; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 42:
                missionStats.need = 75000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 46:
                missionStats.games = 8; // count games
                missionStats.need = 50; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 45:
                missionStats.need = 85000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
            case 49:
                missionStats.games = 10; // count games
                missionStats.need = 100; // count need
                missionStats.has = new float[missionStats.games]; // count try
                break;
            case 48:
                missionStats.need = 150000; // count need
                missionStats.has = new float[1]; // just save progress
                break;
        }

        s_code.ToSave(missionStats, "Missions", "Missions");
    }

    public TypeMission GetTypeMission(int numberMission)
    {
        switch(numberMission)
        {
            case 2: return TypeMission.PickupGems;
            case 3: return TypeMission.Ride;
            case 4: return TypeMission.BestScore;
            case 5:
            case 6:
            case 7: return TypeMission.RideToStage;
            case 9: return TypeMission.Ride;
            case 10: return TypeMission.PickupGems;
            case 12: return TypeMission.Ride;
            case 13: return TypeMission.PickupGems;
            case 15: return TypeMission.Ride;
            case 16: return TypeMission.PickupGems;
            case 18: return TypeMission.Ride;
            case 19: return TypeMission.PickupGems;
            case 21: return TypeMission.Ride;
            case 22: return TypeMission.PickupGems;
            case 24: return TypeMission.Ride;
            case 25: return TypeMission.PickupGems;
            case 27: return TypeMission.Ride;
            case 28: return TypeMission.PickupGems;
            case 30: return TypeMission.Ride;
            case 31: return TypeMission.PickupGems;
            case 33: return TypeMission.Ride;
            case 34: return TypeMission.PickupGems;
            case 36: return TypeMission.Ride;
            case 37: return TypeMission.PickupGems;
            case 39: return TypeMission.Ride;
            case 40: return TypeMission.PickupGems;
            case 42: return TypeMission.Ride;
            case 43: return TypeMission.PickupGems;
            case 45: return TypeMission.Ride;
            case 46: return TypeMission.PickupGems;
            case 48: return TypeMission.Ride;
            case 49: return TypeMission.PickupGems;
            default: return TypeMission.Other;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Write all mission text")]
    public void WriteAllMissionText()
    {
        for(int i = 1; i < MAX_MISSIONS; i++)
        {
            GetMissionText(i);
        }
    }
#endif
}

public class MissionStats
{
    public int need;
    public float[] has;
    public int hasGames;
    public int games;

    public MissionStats()
    {

    }

    public MissionStats(int _need, float[] _has, int _hasGames, int _games)
    {
        need = _need;
        has = _has;
        hasGames = _hasGames;
        games = _games;
    }
}

public enum TypeMission
{
    Laps,
    Ride,
    PickupGems,
    RideToStage,
    BestScore,
    Other
}