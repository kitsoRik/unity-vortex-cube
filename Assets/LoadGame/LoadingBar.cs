using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class LoadingBar : MonoBehaviour {

    public float Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            SetValue(value);
        }
    }
    private float _value;

    public Image Fill;
    private float width, height;

    public void Awake()
    {
        width = GetComponent<RectTransform>().rect.width;
        height = GetComponent<RectTransform>().rect.height;
    } 

    public void SetValue(float value)
    {
        Fill.GetComponent<RectTransform>().offsetMax = new Vector2((-1 + value) * width, 0);
    }
}
