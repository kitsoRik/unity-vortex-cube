using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingR : MonoBehaviour
{
    public float speedRotating = -1;
    void FixedUpdate()
    {
        if (!GameController.Playing)
            return;
        transform.Rotate(Vector3.up * speedRotating);
    }
}
