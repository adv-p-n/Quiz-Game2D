using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endText;
    ScoreKeeper score;
    void Awake()
    {
        score = FindObjectOfType<ScoreKeeper>();
    }
    public void ShowFinalScore()
    {
        endText.text = "Congrats \n Your Score is: " + score.ScorePercent() + "%";
    }

}
