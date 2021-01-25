#pragma warning disable IDE0044

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class Settings : MonoBehaviour {

    public PurchaseManager purchaseManager;

    public MainMenu mainMenu;
    public GameObject SettingsPanel;
    public Animator animator;

    public Button LanguageButton,
        VibrationButton,
        SoundButton,
        ARButton,
        NoAdsButton;

    public BackGround backGround;

    public Sprite LanguageUA, LanguageRU, LanguageEN;
    public Sprite VibrationOn, VibrationOff;
    public Sprite SoundOn, SoundOff;
    public Sprite AROn, AROff;

    public static TextMeshProUGUI[] TextsForTranslate;

    private static Coroutine permisionCam;

    private static SystemLanguage _language;

    public void StartAwake()
    {
        PurchaseManager.OnPurchaseNonConsumable += PurchaseManager_OnPurchaseNonConsumable;
    }

    public bool IsVibration { get; set; }

    public bool SoundIsOn { get; set; }

    public bool IsAR { get; set; }

    public Boolean NoAds
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                return PurchaseManager.CheckBuyState("no_ads") == true ? Boolean.True : Boolean.False;
            }   
            catch (Exception)
            {
                return Boolean.Other;
            }
#elif UNITY_EDITOR
            return Boolean.Other;
#endif
        }
    }

    private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        NoAdsButton.interactable = false;
        GameManager.Save();
    }

    public void Starting()
    {
        VibrationButton.image.sprite = IsVibration ? VibrationOn : VibrationOff;
        NoAdsButton.interactable = NoAds == Boolean.False;
        SettingsPanel.SetActive(true);
        LanguageButton.image.sprite = Language == SystemLanguage.Ukrainian ? LanguageUA : Language == SystemLanguage.Russian ? LanguageRU : LanguageEN;
        SoundButton.image.sprite = SoundIsOn ? SoundOn : SoundOff;
        ARButton.image.sprite = IsAR ? AROn : AROff;
    }

    public void ClickLanguage()
    {
        if (!MainMenu.YouMayClick)
            return;
        switch (Language)
        {
            case SystemLanguage.Ukrainian: Language = SystemLanguage.Russian; LanguageButton.image.sprite = LanguageRU; break;
            case SystemLanguage.Russian: Language = SystemLanguage.English; LanguageButton.image.sprite = LanguageEN; break;
            default: Language = SystemLanguage.Ukrainian; LanguageButton.image.sprite = LanguageUA; break;
        }
        Lang.ChangeLanguage(Language);
    }

    public SystemLanguage Language
    {
        get
        {
            return _language;
        }
        set
        {
            _language = Helper.ReworkLanguage(value, SystemLanguage.Ukrainian, SystemLanguage.Russian, SystemLanguage.English);
        }
    }

    public void ClickVibration()
    {
        if (!MainMenu.YouMayClick)
            return;
        IsVibration = !IsVibration;
        if (IsVibration)
        {
            VibrationButton.image.sprite = VibrationOn;
            VibrationManager.Vibrate(1/2f);
        }
        else
        {
            VibrationButton.image.sprite = VibrationOff;
            VibrationManager.Cancel();
        }
    }

    public void ClickSound()
    {
        if (!MainMenu.YouMayClick)
            return;
        SoundIsOn = !SoundIsOn;

        SoundManager.Volume = SoundIsOn;

        if (SoundIsOn)
        {
            SoundButton.image.sprite = SoundOn;
        }
        else
        {
            SoundButton.image.sprite = SoundOff;
        }
    }

    public void ClickAR()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        if (!IsAR && !Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
         StartCoroutine(CheckPermissionAR(Permission.Camera));
    }

    private IEnumerator CheckPermissionAR(string permission)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        while (!Permission.HasUserAuthorizedPermission(permission))
            yield return 0;
#endif

        IsAR = !IsAR;
        backGround.Activate();
        ARButton.image.sprite = IsAR ? AROn : AROff;
        yield break;
    }
    
    public void ClickAchivmentsButton()
    {
        SocialManager.ShopAchivments();
    }

    public void ClickLeaderBoardButton()
    {
        SocialManager.ShowLeaderboard();
    }

    public void ClickNoAds()
    {
        purchaseManager.BuyNonConsumable(0);
    }

    public void ClickMainMenuButton()
    {
        MainMenu.YouMayClick = false;
        animator.Play("SettingExit");
        mainMenu.Starting("Settings");
    }

    public void Exit()
    {
        SettingsPanel.SetActive(false);
    }
}

public class SettingsVariables
{
    public bool _soundIsOn,
        _isAR,
        _vibrationIsOn;
    public string _language;

    public SettingsVariables()
    {
        _soundIsOn = true;
        _vibrationIsOn = true;

        _language = Application.systemLanguage.ToString();
    }

    public SettingsVariables(bool __soundIsOn, bool __isAR, bool __vibrationIsOn,
        SystemLanguage __language)
    {
        _soundIsOn = __soundIsOn;
        _isAR = __isAR;
        _vibrationIsOn = __vibrationIsOn;

        _language = __language.ToString();
    }

}

public interface ITextTranslator
{
    void ChangeTexts();
}