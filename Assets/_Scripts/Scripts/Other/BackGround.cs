#pragma warning disable IDE0044

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif


public class BackGround : MonoBehaviour {

    public Missions missions;
    public GameObject Pipe;
    
    public GameObject backGroundPanel;
    public Animator animator;
    public TextMeshProUGUI Text321, ScoreText;

    public TextMeshProUGUI ContextText, ContextTextLose;


    public RawImage background;
    private WebCamTexture camTexture;

    public bool isAR;
    private Coroutine text321Cor;

	void Awake ()
    {
        background = transform.GetChild(0).GetComponent<RawImage>(); 
        if (Screen.height < 1920)
        {

            if (Screen.height / Screen.width > 1.9)
                background.rectTransform.sizeDelta = new Vector2(2220, 1080);
            else background.rectTransform.sizeDelta = new Vector2(1920, 1080);
        }
        else
        {
            background.rectTransform.sizeDelta = new Vector2(Screen.height, Screen.width);
        }

        Activate();

        GameController.playingCause += AfterStart;
        GameController.losesCause += AfterLose;
        GameController.replayCause += AfterStart;
    }

    private void AfterLose()
    {
        if (Missions.typeMission != TypeMission.Other)
        {
            ContextTextLose.text = ContextText.text;
            ContextTextLose.gameObject.SetActive(true);
        }else
        {
            ContextTextLose.gameObject.SetActive(false);
        }
        ContextText.gameObject.SetActive(false);
    }

    private void AfterStart()
    {
        if(Missions.typeMission != TypeMission.Other)
        {
            switch (Missions.typeMission)
            {
                case TypeMission.Laps: ContextText.text = Lang.Phrase("Laps{0} {1}", ":", Control.typeMissionHelper.ToString("0.00")); break;
                case TypeMission.Ride: ContextText.text = Lang.Phrase("Ride{0} {1}", ":", 0); break;
                case TypeMission.RideToStage: ContextText.text = string.Format("{0}", Lang.Phrase(Creating.ComplexityToString(Creating.complexityPlayer))); break;
                case TypeMission.PickupGems: ContextText.text = Lang.Phrase("Gems{0} {1}", ":", GameController.PickupGemsInGame.ToString()); break;
                case TypeMission.BestScore: ContextText.text = (GameController.statistics.BestScore - GameController.Score).ToString(); break;
            }
            ContextText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        switch(Missions.typeMission)
        {
            case TypeMission.Laps: ContextText.text = Lang.Phrase("Laps{0} {1}", ":", Control.typeMissionHelper.ToString("0.00")); break;
            case TypeMission.Ride: ContextText.text = Lang.Phrase("Ride{0} {1}", ":", Control.typeMissionHelper.ToString("0.00")); break;
            case TypeMission.RideToStage: ContextText.text = string.Format("{0}", Lang.Phrase(Creating.ComplexityToString(Creating.complexityPlayer))); break;
            case TypeMission.PickupGems: ContextText.text = Lang.Phrase("Gems{0} {1}", ":", GameController.PickupGemsInGame.ToString()); break;
            case TypeMission.BestScore:
                int bS = GameController.statistics.BestScore - GameController.Score;
                ContextText.text = bS > 0 ? (GameController.statistics.BestScore - GameController.Score).ToString() : Lang.Phrase("Now");
                break;
        }
    }

    public void Starting(bool WithText321 = true, bool toPlay = false)
    {
        StopText321();
        text321Cor = StartCoroutine(Starting32IE(WithText321, toPlay));
    }

    public void StopText321()
    {
        if (Text321.gameObject.activeSelf)
        Text321.gameObject.SetActive(false);
        if (text321Cor != null)
            StopCoroutine(text321Cor);
    }

    public void SetScore(int score)
    {
        ScoreText.text = (score).ToString();
    }

    private IEnumerator Starting32IE(bool WithText321 = true, bool toPlay = false)
    {
        GameController.Playing = false;
        if (WithText321)
        {
            int _timer = 4;
            Text321.text = "";
            Text321.gameObject.SetActive(true);
            while (_timer > 0)
            {
                Text321.text = string.Format(Lang.Phrase("Starting in {0}"), --_timer);
                yield return new WaitForSecondsRealtime(1f);
            }
            Text321.gameObject.SetActive(false);
        }
        backGroundPanel.SetActive(true);
        if (Missions.typeMission != TypeMission.Other)
            ContextText.gameObject.SetActive(true);
        Control.MouseClickPositionsX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        GameController.Playing = true;
        animator.Play("StartingBackGround");
    }

    public void Exit()
    {
        animator.Play("ExitBackGround");
    }

    public void Activate()
    {
        if (GameController.settings.IsAR)
        {
            ActivateAR();
        }
        else
        {
            ActivateImage();
        }

    }

    private void ActivateAR()
    {
        //Pipe.gameObject.SetActive(false);
#if !UNITY_EDITOR
        
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        camTexture = new WebCamTexture();
        camTexture.requestedWidth = Screen.width;
        camTexture.requestedHeight = Screen.height;
        background.texture = camTexture;
        camTexture.Play();
#else
        background.texture = null;
#endif
        isAR = true;
    }

    private void ActivateImage()
    {
#if !UNITY_EDITOR
        if(camTexture != null)
        camTexture.Stop();
#endif
        background.texture = null;
        isAR = false;
    }
}
