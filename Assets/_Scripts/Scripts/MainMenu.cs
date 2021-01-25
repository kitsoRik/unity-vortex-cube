#pragma warning disable IDE1006

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public static bool YouMayClick = true;

    public Settings settings;
    public Missions missions;
    public BackGround backGround;
    public Shop shop;
    public Animator animator;
    public GameObject MainPanel, UpperPanel;
    public Button FortuneButton;
    public TextMeshProUGUI MoneyText, TryMoreText, WaitRotateText;
    public Image TryMoreCountImage;

    public Fortune.Fortune fortune;

    private Coroutine setMoney, waitRotate;
    
    
	void Awake ()
    {
        GameController.playingCause += playingCause_MAIN;
        GameController.playHereCause += playHereCause_MAIN;
        GameController.losesCause += losesCause_MAIN;
        GameController.replayCause += replayCause_MAIN;
        GameController.mainMenuCause += mainMenuCause_MAIN;
        GameController.tryMorePlayCause += tryMorePlayCause_MAIN;

        Fortune.Other.FortuneWheel.OnFortuneStop += OnFortuneStop;


        Starting();

        StartWaitingRotate();

        ReInitializedFortune();
    }

    private void mainMenuCause_MAIN()
    {
        FortuneButton.gameObject.SetActive(true);
        Starting();
    }

    private void replayCause_MAIN()
    {
        UpperPanel.SetActive(false);
    }

    private void losesCause_MAIN()
    {
        FortuneButton.gameObject.SetActive(false);
        UpperPanel.SetActive(true);
    }

    private void playHereCause_MAIN()
    {

    }

    private void playingCause_MAIN()
    {

    }

    private void tryMorePlayCause_MAIN()
    {
        UpperPanel.SetActive(false);
    }

    public void Starting(string startinganimation = "")
    {
        if (!TryMoreCountImage.gameObject.activeSelf)
        {
            FortuneButton.gameObject.SetActive(true);
            TryMoreCountImage.gameObject.SetActive(true);
        }
        MoneyText.text = GameManager.Money;
        TryMoreText.text = GameManager.TryMore;
        missions.SetMissionTexts();
        MainPanel.SetActive(true);
        animator.Play("MainmenuStarting" + startinganimation);
    }

    public void ClickRotateButton()
    { 
        fortune.rotateButton.interactable = GameController.settings.NoAds == Boolean.True || AdR.fortuneVideoAd.IsLoaded();

        Exit();
        fortune.Show();
    }

    public void ClickRotateButtonInRotate()
    {
        if (GameController.settings.NoAds == Boolean.True)
        {
            fortune.Rotate();
            return;
        }
        AdR.fortuneVideoAd.Show();
    }

    public void OnClickRotateButtonInRotate(object sender, EventArgs args)
    {
        fortune.Rotate();
    }

    public void ClickExitButtonRotate()
    {
        Starting();
        fortune.Exit();
    }

    public void StartWaitingRotate()
    {
        if (waitRotate != null)
            StopCoroutine(waitRotate);
        StartCoroutine(StartWaitingRotateIE());
    }

    private IEnumerator StartWaitingRotateIE()
    {
        DateTime rotateTime = DateTime.Parse(PlayerPrefs.GetString("RotateTime", DateTime.UtcNow.ToString()));
        FortuneButton.interactable = false;
        yield return TimeR.GetTime(this);
        while (TimeR.time < rotateTime)
        {
            yield return TimeR.GetTime(this);
            WaitRotateText.text = Helper.ToMyTimeString(rotateTime - TimeR.time);
        }
        WaitRotateText.text = string.Empty;
        FortuneButton.interactable = true;
    }

    public void ClickPlay()
    {
        if (!YouMayClick)
            return;
        
        if (GameController.playingCause != null)
            GameController.playingCause();
        YouMayClick = false;
        
        HideMoney();
        backGround.Starting(false);
        Exit();
    }

    private void HideMoney()
    {
        UpperPanel.SetActive(false);
    }

    public void SetMoney(int money, bool withPlus = false)
    {
        if (setMoney != null)
            StopCoroutine(setMoney);
        setMoney = StartCoroutine(SetMoneyIE(money, withPlus));
    }

    public void SetTryMore(int tryMore)
    {
        TryMoreText.text = tryMore.ToString();
    }

    private IEnumerator SetMoneyIE(int money, bool withPlus)
    {
        float _now = int.Parse(MoneyText.text);
        int _toNow = _now < money ? 1 : -1;
        if (!withPlus)
        {
            while ((int)_now != money)
            {
                _now = Mathf.Lerp(_now, money + _toNow, 0.1f);
                MoneyText.text = ((int)_now).ToString();
                yield return null;
            }
        }
        else
        {
            while ((int)_now != money)
            {
                _now = Mathf.Lerp(_now, money + _toNow, 0.1f);
                MoneyText.text = _now  + " + " + (money - _now);
                yield return null;
            }
            MoneyText.text = ((int)_now).ToString();
        }
    }

    public void ClickSettingsButton()
    {
        if (!YouMayClick)
            return;
        YouMayClick = false;
        Exit();
        settings.Starting();
    }

    public void ClickStatisticsButton()
    {
        if (!YouMayClick)
            return;
        YouMayClick = false;
        Exit();
        GameController.statistics.Starting();
    }

    public void ClickShopButton()
    {
        if (!YouMayClick)
            return;
        YouMayClick = false;
        
        TryMoreCountImage.gameObject.SetActive(false);
        FortuneButton.gameObject.SetActive(false);

        Exit();
        shop.Starting();
    }

    public void Exit()
    {
        animator.Play("MainmenuExit");
    }

    private void OnFortuneStop(Fortune.Other.FortuneItem winFortuneItem)
    {
        StartCoroutine(OnFortuneStopIE());
        PlayerPrefs.SetString("RotateTime", DateTime.UtcNow.AddMinutes(5).ToString());
        StartWaitingRotate();
        switch (winFortuneItem.fortuneItemType)
        {
            case Fortune.Other.FortuneItemType.Money:
                GameManager.Money += winFortuneItem.Money;
                SetMoney(GameManager.Money);
                break;
            case Fortune.Other.FortuneItemType.TryMore:
                GameManager.TryMore += winFortuneItem.TryMore;
                SetTryMore(GameManager.TryMore);
                break;
        }
    }

    private void ReInitializedFortune()
    {
        fortune.Initialization(GetRandomFortuneItem(),
            GetRandomFortuneItem(),
            GetRandomFortuneItem(),
            GetRandomFortuneItem(),
            GetRandomFortuneItem(),
            GetRandomFortuneItem(),
            GetRandomFortuneItem(), 
            GetRandomFortuneItem());
    }

    private Fortune.Other.FortuneItem GetRandomFortuneItem()
    {
        switch(UnityEngine.Random.Range(0,2))
        {
            case 0: return new Fortune.Other.FortuneItem()
            {
                fortuneItemType = Fortune.Other.FortuneItemType.Money,
                Money = UnityEngine.Random.Range(10, 35),
                Color = UnityEngine.Random.ColorHSV()
            };
            default:
                return new Fortune.Other.FortuneItem()
                {
                    fortuneItemType = Fortune.Other.FortuneItemType.TryMore,
                    TryMore = UnityEngine.Random.Range(1, 3),
                    Color = UnityEngine.Random.ColorHSV()
                };
        }
    }

    private IEnumerator OnFortuneStopIE()
    {
        yield return new WaitForSeconds(2);
        Starting();
        
        yield return new WaitForSeconds(2f);
        ReInitializedFortune();
    }
}
