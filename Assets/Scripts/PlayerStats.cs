using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Color[] hasColors;
    public Vector3 shopRotation;
    public Vector3 playRotation;

    [Space]
    public Vector3 rotationVector;

    public void SetShopRotation()
    {
        transform.eulerAngles = shopRotation;
    }

    public void SetPlayRotation()
    {
        transform.eulerAngles = playRotation;
    }
}
