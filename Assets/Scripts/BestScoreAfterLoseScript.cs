using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScoreAfterLoseScript : MonoBehaviour
{
    private string bestScoreString;
    private TMPro.TextMeshProUGUI bestScoreText;

    public void Play(string bestscoreString, TMPro.TextMeshProUGUI textMesh)
    {
        GetComponent<Animator>().Play("BestScoreAfterLose");
        bestScoreString = bestscoreString;
        bestScoreText = textMesh;
    }

    public void SetSave()
    {
        GameManager.Save();
    }

    public void SetText()
    {
        bestScoreText.text = bestScoreString;
        GetComponent<UnityEngine.UI.Outline>().enabled = true;
    }
    private void OnEnable()
    {
        if(bestScoreText != null)
        bestScoreText.transform.parent.GetComponent<UnityEngine.UI.Outline>().enabled = false;
    }

}
