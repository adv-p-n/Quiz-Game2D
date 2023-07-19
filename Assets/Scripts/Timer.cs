using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    public float fillAmount=1f;
    [SerializeField] float answeringTime=30f;
    [SerializeField] float revealAnswerTime=10f;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion=false;

    void Update()
    {
        UpdateTimer();
        
    }
    public void CancelTimer()
    {
        time=0f;
    }
    void UpdateTimer()
    {
        time-=Time.deltaTime;
        if(isAnsweringQuestion)                    //check if we are in answering state and the timer time is "0"
        {
            if(time>0)
            {
                fillAmount= time/answeringTime;
            }                                  
            else
            {
                isAnsweringQuestion=false;         //set the answering state to "false" and set timer time to "revealAnswerTime"
                time=revealAnswerTime;
            }
        }
        else
        {
            if(time>0)
            {
                fillAmount= time/revealAnswerTime ;
            }
            else
            {
                loadNextQuestion=true;
                isAnsweringQuestion=true;         //set the answering state to "true" and set timer time to "AnsweringTime"
                time=answeringTime;
            }
        }
    } 
}
