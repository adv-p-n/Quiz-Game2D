using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int answerCount;
    int questionsSeen;

    public int GetAnswerCount()
    {
        return answerCount;
    }
    public void IncrementAnswerCount()
    {
        answerCount ++; 
    }

    public int GetQuestionCount()
    {
        return questionsSeen;
    }
    public void IncerementQuenstionsSeen()
    {
        questionsSeen++ ;
    }
    public int ScorePercent()
    {
        return Mathf.RoundToInt(answerCount/(float)questionsSeen *100) ;
    }

}
