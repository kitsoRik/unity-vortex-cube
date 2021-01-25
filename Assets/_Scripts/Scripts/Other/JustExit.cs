using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustExit : MonoBehaviour {

    public void Exit()
    {
        Canvas canvas = transform.parent.GetComponent<Canvas>();
        if (canvas != null)
            canvas.sortingOrder--;
        MainMenu.YouMayClick = true;
        gameObject.SetActive(false);
    }

    public void Load()
    {
        MainMenu.YouMayClick = true;
        Canvas canvas = transform.parent.GetComponent<Canvas>();
        if (canvas != null)
            canvas.sortingOrder++;
    }
}
