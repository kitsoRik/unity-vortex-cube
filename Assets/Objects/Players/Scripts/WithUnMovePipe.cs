using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithUnMovePipe : MonoBehaviour
{
    public Vector3 vector;

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(vector.x == 0 ? transform.eulerAngles.x : vector.x,
            vector.y == 0 ? transform.eulerAngles.y : vector.x,
            vector.z == 0 ? transform.eulerAngles.z : vector.x));
    }
}
