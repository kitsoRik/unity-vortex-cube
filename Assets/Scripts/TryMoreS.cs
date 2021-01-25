#pragma warning disable IDE0044

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TryMoreS : MonoBehaviour
{
    public Animator animator;
    public Button tryMoreButton;
    public Image tryMoreBack;
    public TextMeshProUGUI tryMoreButtonText;

    private Coroutine showButtonCor;

    public void ShowButton()
    {
        if (showButtonCor != null)
            StopCoroutine(showButtonCor);
        showButtonCor = StartCoroutine(ShowButtonIE());
    }

    public void ClickTryMoreButton()
    {
        if (showButtonCor != null)
            StopCoroutine(showButtonCor);
        Exit();
        GameController.tryMorePlayCause();
    }

    private IEnumerator ShowButtonIE()
    {
        Image imageTM = tryMoreButton.GetComponent<Image>();
        int priceTM = (int)Mathf.Pow(2, GameController.PowOfTryMoreNumber);
        tryMoreButtonText.text = priceTM.ToString();
        tryMoreButton.gameObject.SetActive(true);
        tryMoreBack.gameObject.SetActive(true);
        Starting();
        float t = 3f;
        while (t > 0)
        {
            imageTM.fillAmount = kitsoRik.MathR.PercentLerp(1, 0, (3 - t) / 3);//Mathf.Lerp(1, 0, (3 - t)/3 * Time.deltaTime);
            yield return null;
            t -= Time.deltaTime;
        }
        tryMoreBack.gameObject.SetActive(false);
        tryMoreButton.gameObject.SetActive(false);
        GameController.AfterLose();
    }

    public void Starting()
    {
        animator.Play("StartingTM");
    }

    public void Exit()
    {
        animator.Play("ExitTM");
    }
}
