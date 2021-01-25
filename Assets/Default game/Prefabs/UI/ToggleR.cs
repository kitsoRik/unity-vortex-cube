using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class ToggleR : MonoBehaviour
{
    public Animator animator;
    public Action whenOn, whenOff;

    public TextMeshProUGUI OnOffText;

    private bool isOn;

    void Awake()
    {
        SetText();
        if (isOn = Shop.RandomCharacter)
        {
            animator.Play("ToggleOn");
            // _animation.GetClip("ToggleOn").SampleAnimation(gameObject, _animation["ToggleOn"].length);
        }
        else
        {
            animator.Play("ToggleOff");
            //_animation.GetClip("ToggleOff").SampleAnimation(gameObject, _animation["ToggleOff"].length);
        }
    }

    public void OnPointerClick()
    {
        if (isOn != Shop.RandomCharacter)
            SetText();
        Shop.RandomCharacter = !Shop.RandomCharacter;
        if (Shop.RandomCharacter)
        {
            animator.Play("ToggleOn");
        }
        else
        {
            animator.Play("ToggleOff");
        }
    }

    public void SetText()
    {
        if (isOn = Shop.RandomCharacter)
        {
            OnOffText.text = Lang.Phrase("On");
            if (whenOn != null)
                whenOn();
        }
        else
        {
            OnOffText.text = Lang.Phrase("Off");
            if (whenOff != null)
                whenOff();
        }
    }
}
