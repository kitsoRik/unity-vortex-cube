using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoseS : MonoBehaviour
{
    public GameObject LoseSPanel;
    public Animator animator;
    public TextMeshProUGUI MissionCompletedText,
        ScoreText, BestScoreText, PickUpgemsInGameText;

    public void Starting()
    {
        if (GameController.missionComplete)
        {
            MissionCompletedText.transform.parent.gameObject.SetActive(true);
            MissionCompletedText.text = string.Format(Lang.Phrase("Mission #{0} completed"), GameManager.NowMission-1);
        }
        else
        {
            MissionCompletedText.transform.parent.gameObject.SetActive(false);
        }

        BestScoreText.transform.parent.localScale = new Vector3(1, 1, 1);

        ScoreText.text = string.Format(Lang.Phrase("Score{0} {1}"), ":", GameController.Score);
        BestScoreText.text = string.Format(Lang.Phrase("Best score{0} {1}"), ":", GameController.statistics.BestScore);
        PickUpgemsInGameText.text = string.Format(Lang.Phrase("Pickup gems{0} {1}"), ":", GameController.PickupGemsInGame);

        LoseSPanel.SetActive(true);
        if (GameController.statistics.BestScore < GameController.Score)
        {
            if (GameController.settings.IsVibration)
            {
                VibrationManager.Cancel();
                VibrationManager.Vibrate(new float[] { 0.3f, 0.3f, 0.3f });
            }
            SocialManager.ReportScore(GPGSIds.leaderboard_best_score, GameController.Score);
            GameController.statistics.BestScore = GameController.Score;
            BestScoreText.transform.parent.GetComponent<BestScoreAfterLoseScript>().
                Play(string.Format(Lang.Phrase("Best score{0} {1}"), ":", GameController.statistics.BestScore), BestScoreText);
        }
    }



    public void ClickReplay()
    {
        if(GameController.replayCause != null)
        GameController.replayCause();
        Exit();
    }

    public void ClickMainMenu()
    {
        if (GameController.mainMenuCause != null)
        GameController.mainMenuCause();
        Exit();
    }

    public void Exit()
    {
        animator.Play("ExitLose");
    }
}
