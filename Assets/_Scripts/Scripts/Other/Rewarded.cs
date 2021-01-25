using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rewarded : MonoBehaviour {

    public MainMenu mainMenu;
    public Animator animator;
    public TextMeshProUGUI CountRewardText, Title;
    public GameObject RewardedPanel;
    public RewardItem[] Items;

    public static s_int RewardedDay;
    public static DateTime RewardedMoment;

    void Awake ()
    {
        StartCoroutine(CheckReward());
	}

    private IEnumerator CheckReward()
    {
        yield return TimeR.GetTime(this);
        while (RewardedMoment > TimeR.time)
        {
            yield return TimeR.GetTime(this);
        }

        if (RewardedMoment < TimeR.time)
        {
            while (GameController.Playing)
            {
                yield return null;
            }

            Reward();
        }
    }

    private IEnumerator StartTimer() // unused 
    {
        while (RewardedPanel.activeSelf)
        {
            TimeSpan _ts = RewardedMoment - TimeR.time;
            //TimeToRewardedText.text = string.Format("{0}:{1}:{2}", Helper.GetBeginZero(_ts.Hours), Helper.GetBeginZero(_ts.Minutes), Helper.GetBeginZero(_ts.Seconds));
            yield return null;
        }
    }

    public void Starting()
    {
        MainMenu.YouMayClick = false;
        RewardedPanel.SetActive(true);
        Title.text = Lang.Phrase("Rewarded");
    }

    private void Reward()
    {
        if ((RewardedMoment - TimeR.time).Days < 0)
            RewardedDay = 1;
        DateTime nextReward = TimeR.time.AddDays(1);
        int _getcountreward = GetCountRewarded(RewardedDay);
        InitializationItems();
        
        GameManager.Money += _getcountreward;
        RewardedDay++;
        RewardedMoment = Helper.GetToDay(nextReward);   
        GameManager.Save();

        SetNotification();

        //StartCoroutine(StartTimer());
        CountRewardText.text = Lang.Phrase("Daily reward") + ": " + _getcountreward;
        Starting();
    }

    private void SetNotification()
    {
        DateTime dayRewardedNumber = Helper.GetToDay(DateTime.UtcNow.AddDays(1)).ToLocalTime();
        if (dayRewardedNumber.Hour > 22)
        {
            dayRewardedNumber = dayRewardedNumber.AddHours(24 - dayRewardedNumber.Hour + 9);
        } else if (dayRewardedNumber.Hour < 9)
        {
            dayRewardedNumber = dayRewardedNumber.AddHours(9 - dayRewardedNumber.Hour);
        }
        Managers.NotificationManager.Send(2, dayRewardedNumber, Lang.Phrase("Daily bonus"), Lang.Phrase("Your daily bonus is ready."), Color.blue, NotTouch.NotificationIcon.Bell);
        for (int i = GameManager.ID_REWARDED_NEXT_100 + 2; i < GameManager.ID_REWARDED_NEXT_100 + 100; i++)
        {
            Managers.NotificationManager.Send(i, dayRewardedNumber.AddDays(i - GameManager.ID_REWARDED_NEXT_100), Lang.Phrase("Skipping days"), Lang.Phrase("You skipped {0} {1}, get your rewarded", i - GameManager.ID_REWARDED_NEXT_100, i - GameManager.ID_REWARDED_NEXT_100 == 1 ? Lang.Phrase("day") : Lang.Phrase("days")), Color.blue, NotTouch.NotificationIcon.Bell);
        }
    }
    private void InitializationItems()
    {
        int _rewDay = Mathf.Clamp(RewardedDay, 1, 5);
        for (int i = 0; i < 5; i++)
        {
            if(i < _rewDay-1)
            {
                Color _cl = Items[i].backGround.color;
                _cl.a = 0.5f;
                Items[i].backGround.color = _cl;
            }
            Items[i].CountRewardText.text = 
                GetCountRewarded(i + 1).ToString();
        }

        Outline _outline = Items[_rewDay - 1].gameObject.AddComponent<Outline>();
        _outline.effectDistance = new Vector2(15, 15);
        _outline.effectColor = Color.yellow;
    }

    private int GetCountRewarded(int num_day)
    {
        switch (num_day)
        {
            case 1: return 100;
            case 2: return 200;
            case 3: return 400;
            case 4: return 600;
            case 5: return 750;
            default: return 0;
        }
    }

    public void Exit()
    {
        if(!MainMenu.YouMayClick)
            return;
        mainMenu.SetMoney(GameManager.Money);
        animator.Play("ExitRewardedPanel");
    }
}

public class RewardedVariables
{
    public int _rewardedDay;
    public string _rewardedMoment;

    public RewardedVariables()
    {
        _rewardedDay = 1;
        _rewardedMoment = new DateTime(1, 1, 1, 0, 0, 0).ToLongDateString();
    }

    public RewardedVariables(int __rewardedDay, DateTime __rewardedMoment)
    {
        _rewardedDay = __rewardedDay;
        _rewardedMoment = __rewardedMoment.ToString();
    }
}
