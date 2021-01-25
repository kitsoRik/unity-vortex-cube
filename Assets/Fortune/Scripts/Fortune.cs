#pragma warning disable IDE1006, CS0108, IDE1006

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fortune.Other;


namespace Fortune
{
    public class Fortune : MonoBehaviour
    {
        public UnityEngine.UI.Button rotateButton, closeButton;

        private Transform CanvasT;
        [SerializeField]
        public FortuneWheel fortuneWheel;

        private Coroutine reduceRotateButtonCor,
            reduceFortunePanelCor,
            reduceCloseButtonCor;
        
        public void Starting()
        {
            CanvasT = transform.root;
            FortuneWheel.OnFortuneStop += OnFortuneStop;
        }

        public void Initialization(params FortuneItem[] fortuneItems)
        {
            fortuneWheel.Initialization(fortuneItems);
        }

        public void Show()
        {
            CanvasT.gameObject.SetActive(true);
            StartCoroutine(reduceFortunePanelStarting());
            rotateButton.transform.localScale = new Vector3(1, 1, 1);
            closeButton.transform.localScale = Vector3.zero;
            fortuneWheel.transform.eulerAngles = Vector3.zero;
        }

        public void Rotate()
        {
            if (reduceCloseButtonCor != null)
                StopCoroutine(reduceCloseButtonCor);
            reduceCloseButtonCor = StartCoroutine(reduceCloseButton(Vector3.zero));
            fortuneWheel.Rotate();
            if (reduceRotateButtonCor != null)
                StopCoroutine(reduceRotateButtonCor);
            reduceRotateButtonCor = StartCoroutine(reduceRotateButton());
        }

        public void Exit()
        {
            if (fortuneWheel.isRotating)
                return;
            if (reduceCloseButtonCor != null)
                StopCoroutine(reduceCloseButtonCor);
            reduceCloseButtonCor = StartCoroutine(reduceCloseButton(Vector3.zero, true));
        }

        private void OnFortuneStop(FortuneItem winItem)
        {
            if (reduceFortunePanelCor != null)
                StopCoroutine(reduceFortunePanelCor);
            reduceFortunePanelCor = StartCoroutine(reduceFortunePanel(2f));
        }

        private IEnumerator reduceRotateButton()
        {
            while (rotateButton.transform.localScale.y != 0)
            {
                rotateButton.transform.localScale = Vector3.Lerp(rotateButton.transform.localScale, Vector3.zero, 1 / 15f);
                yield return 0;
            }
        }

        private IEnumerator reduceCloseButton(Vector3 fSize, bool closePanel = false)
        {
            float duration = 25;
            float step = 1 / duration;
            float covDistance = 0;
            while(covDistance < 1)
            {
                closeButton.transform.localScale = Vector3.Lerp(closeButton.transform.localScale, fSize, covDistance);
                covDistance += step;
                yield return new WaitForFixedUpdate();
            }
            closeButton.transform.localEulerAngles = fSize;
            if(closePanel)
            {
                if (reduceFortunePanelCor != null)
                    StopCoroutine(reduceFortunePanelCor);
                reduceFortunePanelCor = StartCoroutine(reduceFortunePanel(0));
            }
        }

        private IEnumerator reduceFortunePanel(float t)
        {
            yield return new WaitForSeconds(t);
            while (Mathf.Abs(transform.localScale.y) >= 0.1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 1 / 10f);
                yield return new WaitForFixedUpdate();
            }
            CanvasT.gameObject.SetActive(false);
        }

        private IEnumerator reduceFortunePanelStarting()
        {
            Vector3 fSize = new Vector3(1, 1, 1);
            while (Mathf.Abs(transform.localScale.y) <= 0.95)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, fSize, 1 / 10f);
                yield return new WaitForFixedUpdate();
            }
            transform.localScale = fSize;
            if (reduceCloseButtonCor != null)
                StopCoroutine(reduceCloseButtonCor);
            reduceCloseButtonCor = StartCoroutine(reduceCloseButton(fSize));
        }
    }
}
