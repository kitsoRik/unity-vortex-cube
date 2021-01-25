using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Statistics : MonoBehaviour {

    public Animator animator;
    public MainMenu mainMenu;
    public GameObject StatisticPanel;
    public Button LeaderboardButton, AchivmentsButton;

    public TextMeshProUGUI LeftText, RightText, Title; 

    public s_int PlayedGames { get; set; }

    public s_int BestScore { get; set; }

    public s_int PickupGemes { get; set; }

    public float AveragesScore
    {
        get
        {
            if (PlayedGames != 0)
                return (float)TotalScore / PlayedGames;
            else return 0;
        }
    }

    public s_int TotalScore { get; set; }

    public void Starting()
    {
        Title.text = Lang.Phrase("Statistics");
        LeftText.text =
            string.Format(Lang.Phrase("Best score{0}"), ":") + "\n" +
            string.Format(Lang.Phrase("Average score{0}"), ":") + "\n" +
            string.Format(Lang.Phrase("Total score{0}"), ":") + "\n" +
            string.Format(Lang.Phrase("Missions completed{0}"), ":") + "\n" +
            string.Format(Lang.Phrase("Played games{0}"), ":") + "\n" +
            string.Format(Lang.Phrase("Pickup gems{0}"), ":");

        RightText.text =
            BestScore + "\n" +
            AveragesScore.ToString("0.00") + "\n" +
            TotalScore + "\n" +
            (GameManager.NowMission - 1) + string.Format("/{0}\n", Missions.MAX_MISSIONS) +
            PlayedGames + "\n" +
            PickupGemes + "\n";

        LeaderboardButton.interactable = SocialManager.Authenticated;
        AchivmentsButton.interactable = SocialManager.Authenticated;

        StatisticPanel.SetActive(true);
    }

    public void ClickLeaders()
    {
        SocialManager.ShowLeaderboard();
    }

    public void ClickAchiv()
    {
        SocialManager.ShopAchivments();
    }

    public void ClickExitButton()
    {
        if (!MainMenu.YouMayClick)
            return;
        MainMenu.YouMayClick = false;
        animator.Play("StatisticsExit");
        mainMenu.Starting("Settings");
    }

    public void ClickTwitter()
    {
        if (!MainMenu.YouMayClick)
            return;
        Application.OpenURL("https://twitter.com");
    }

    public void ClickFacebook()
    {
        if (!MainMenu.YouMayClick)
            return;
        Application.OpenURL("https://www.facebook.com/");
    }

    public void ClickGPS()
    {
        if (!MainMenu.YouMayClick)
            return;
        Application.OpenURL("https://play.google.com/store/apps/developer?id=kitsoRik+Games");
    }

    public void ClickInstagram()
    {
        if (!MainMenu.YouMayClick)
            return;
        Application.OpenURL("https://www.instagram.com/kitsorik/");
    }
}

public class StatisticsVariables
{
    public int _playedGames,
        _bestScore,
        _pickupGems,
        _totalScore;

    public StatisticsVariables()
    {

    }

    public StatisticsVariables(int __playedGames, int __bestScore, int __pickupGems,
        int __totalScore)
    {
        _playedGames = __playedGames;
        _bestScore = __bestScore;
        _pickupGems = __pickupGems;
        _totalScore = __totalScore;
    }
}
