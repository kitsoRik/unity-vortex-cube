#pragma warning disable IDE1006

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fortune;

namespace Fortune.Other
{
    [ExecuteInEditMode]
    public class FortuneItem : MonoBehaviour
    {
        public string Name
        {
            get
            {
                return TMP_Name.text;
            }

            set
            {
                TMP_Name.text = value;
            }
        }

        public Sprite iconSprite
        {
            get
            {
                return icon.sprite;
            }

            set
            {
                icon.sprite = value;
            }
        }

        public Color color
        {
            get
            {
                return thisImage.color;
            }

            set
            {
               thisImage.color = value;
            }
        }

        public FortuneItemType fortuneItemType;

        public int Money;
        public int TryMore;
        public Color Color;

        [SerializeField]
        private TextMeshProUGUI TMP_Name;
        [SerializeField]
        private Image icon;
        [SerializeField]
        private Image thisImage;

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying)
                return;
            icon.sprite = iconSprite;
        }
#endif
    }
}