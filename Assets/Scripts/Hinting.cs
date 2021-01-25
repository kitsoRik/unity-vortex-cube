using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hinting : MonoBehaviour
{
    private const string StartHint = "StartHint", EndHint = "EndHint";

    private bool mayHint = false, isTurned;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Slider slider;

    private void Awake()
    {
        slider.value = 1 / 2f;
        GameController.playingCause += Active;
        GameController.replayCause += Active;
    }

    private void Active()
    {
        StartCoroutine(ActiveHint());
    }

    private IEnumerator ActiveHint()
    {
        animator.Play(StartHint);
        yield return StartCoroutine(TurningHandle());
        yield return new WaitForSeconds(2);
        animator.Play(EndHint);
    }

    private IEnumerator TurningHandle()
    {
        isTurned = true;
        bool toRight = true;
        while (!Input.GetMouseButtonDown(0))
        {
            if(toRight)
            {
                slider.value += 0.01f;
                if (slider.value == 1f)
                    toRight = false;
            }
            else
            {
                slider.value -= 0.01f;
                if (slider.value == 0)
                    toRight = true;
            }
            yield return 0;
        }
        GameController.playHereCause();
        isTurned = false;
    }

    private void Update()
    {
        if(!isTurned)
        slider.value = Input.mousePosition.x / Screen.width;
    }
}
