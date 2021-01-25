#pragma warning disable IDE1006,CS0108 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fortune.Other
{
    [ExecuteInEditMode]
    public class FortuneWheel : MonoBehaviour
    {
        public delegate void onFortuneStop(FortuneItem winFortuneItem);

        public static onFortuneStop OnFortuneStop;

        public FortuneItem[] fortuneItems = new FortuneItem[8];

        private Coroutine rotateCor;

        [SerializeField]
        private Sprite MoneySprite, TryMoreSprite;

        public bool isRotating;

        private void Awake()
        {
#if UNITY_EDITOR
            SetAllItem();
#endif
        }

        public void Initialization(params FortuneItem[] setItems)
        {
            for (int i = 0; i < setItems.Length; i++)
            {
                fortuneItems[i].Money = setItems[i].Money;
                fortuneItems[i].TryMore = setItems[i].TryMore;
                fortuneItems[i].fortuneItemType = setItems[i].fortuneItemType;
                fortuneItems[i].Name = GetFortuneItemName(fortuneItems[i]);
                fortuneItems[i].color = setItems[i].Color;
                fortuneItems[i].iconSprite = GetFortuneItemSprite(fortuneItems[i]);
            }
        }

        [HideInInspector]
        public void Rotate()
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - transform.eulerAngles.z % 45);
            if (rotateCor != null)
            {
                isRotating = false;
                StopCoroutine(rotateCor);
            }
            rotateCor = StartCoroutine(rotatingFortune());
        }


        private IEnumerator rotatingFortune()
        {
            isRotating = true;
            const float TIME_OF_ROTATING = 5f;
            //Quaternion quaternion = Quaternion.Euler(0, 0, transform.eulerAngles.z + (Random.Range(25,55) * 45) - (transform.eulerAngles.z % 45));
            Vector3 endOfPath = new Vector3(0, 0, Random.Range(0, 6) * 45 + Random.Range(3, 5) * 360);
            float t = TIME_OF_ROTATING;
            //while (Quaternion.Angle(transform.rotation, quaternion) >= Quaternion.kEpsilon)
            float delta = Time.time;
            while (t > 0)
            {
                delta = Time.deltaTime;
                float step = Mathf.Clamp(Mathf.Abs(Mathf.Sin(t / 5)), 0.001f, 1) * 10;
                t -= delta * step;
                transform.Rotate(endOfPath / 5 * delta * step);
                //transform.GetComponent<RectTransform>().rotation = Quaternion.Lerp(transform.rotation, quaternion, 1/Quaternion.Angle(quaternion, transform.rotation));
                yield return null;
            }
            isRotating = false;
            OnFortuneStop(fortuneItems[GetNumberOfWin(transform.eulerAngles.z)]);
        }

        private int GetNumberOfWin(float angle)
        {
            return (int)((angle) / 45);
        }

        public string GetFortuneItemName(FortuneItem itemType)
        {
            switch (itemType.fortuneItemType)
            {
                case FortuneItemType.Money: return itemType.Money.ToString();
                case FortuneItemType.TryMore: return itemType.TryMore.ToString();
            }
            return "NULL";
        }

        public Sprite GetFortuneItemSprite(FortuneItem itemType)
        {
            switch (itemType.fortuneItemType)
            {
                case FortuneItemType.Money: return MoneySprite;
                case FortuneItemType.TryMore: return TryMoreSprite;
            }
            return null;
        }

#if UNITY_EDITOR
        public void SetAllItem()
        {
            if (Application.isPlaying)
                return;
            fortuneItems = new FortuneItem[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                fortuneItems[i] = transform.GetChild(i).GetComponent<FortuneItem>();
                fortuneItems[i].name = i.ToString();
                transform.GetChild(i).eulerAngles = new Vector3(0, 0, -360 / transform.childCount * i);
            }
        }
#endif
    }

    public enum FortuneItemType
    {
        Money,
        TryMore
    }
}