using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText; //This references the "text gameObject" on the canvas we are modifying 
    QuestionsSO currentQuestion; //This references the "ScriptableObject" which contains the question
    [SerializeField] List<QuestionsSO> questions = new List<QuestionsSO>(); // declare a list to store the SO questions 

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton; //creates an array to contain referanaces to multiple gameObjects(buttons)
    int correctAnswerIndex;
    bool hasAnsweredEarly = false;  

    [Header("Button Sprites")]
    [SerializeField]Sprite correctAnswerSprite;      // variables to store the sprites
    [SerializeField]Sprite defaultSprite;

    [Header("Timer")]
    [SerializeField]Image timerImage;                //variable to store the image of timer 
    Timer timer;                                     

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
   
   [Header("Progress Bar")]
   [SerializeField] Slider progressBar;
   public bool isComplete;


    void Awake()
    {
        timer=FindObjectOfType<Timer>();
        scoreKeeper=FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue= questions.Count;
        progressBar.value= 0 ;

    }
    void Update()
    {
        if (!isComplete)
            timerImage.fillAmount = timer.fillAmount;
            UpdateAnswer();
    }

    private void UpdateAnswer()
    {
        if (timer.loadNextQuestion)
        {
            if (progressBar.value==progressBar.maxValue)
            {
                isComplete= true;
            }
            hasAnsweredEarly=false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }

        else if (!timer.isAnsweringQuestion && !hasAnsweredEarly)
        {
            DisplayAnswer(-1); // we use-1 here because it is the common practice when we dont want to accidently pass a correct value
            SetButtonState(false);
        }
    }

    private void DisplayAnswer(int index)
    {
        if(index==currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct !!!";
            Image buttonImage = answerButton[index].GetComponent<Image>(); // Here "Image" is a component off the button while "sprite" is a component of Image.
            buttonImage.sprite= correctAnswerSprite;
            scoreKeeper.IncrementAnswerCount();
        }
        else
        {
            correctAnswerIndex= currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text =  "Wrong Answer !!!\n The correct answer is:\n"+correctAnswer;// we use the "+" to join two strings.

            Image buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();// The index button is not the correct button 
            buttonImage.sprite= correctAnswerSprite;

        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            GetRandomQuestion();
            DisplayQuestion();
            SetDefaultButtonSpirtes();
            SetButtonState(true);
            progressBar.value++;            
            scoreKeeper.IncerementQuenstionsSeen();
            //Debug.Log("answered:" + scoreKeeper.GetAnswerCount() + "\n Ques :" + scoreKeeper.GetQuestionCount() + "score: "+scoreKeeper.ScorePercent());
            scoreText.text = "Score :" + scoreKeeper.ScorePercent() + "%";
        }
    
    }

    private void GetRandomQuestion()
    {
        int randomIndex = Random.Range(0,questions.Count);   // we get a random number and take the question with that index from the list
        currentQuestion= questions[randomIndex];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);               //we remove the question from the list after selecting it once
        }
    }

    void SetDefaultButtonSpirtes()
    {
        for (int i = 0; i < answerButton.Length; i++)                       // loop through all the buttons
        {
            Image defaultImage = answerButton[i].GetComponent<Image>();     // get the image component of each button.
            defaultImage.sprite=defaultSprite;                              // change sprite back to default sprite.
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion(); //we are passing the question from "SO" to the "text UI"

        for (int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText1 = answerButton[i].GetComponentInChildren<TextMeshProUGUI>(); // the <TextMeshProUGUI> is because the game object is a UI element ie, button
            buttonText1.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelect(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        hasAnsweredEarly= true;
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();  //get the "button" component of the button
            button.interactable = state;                             // set the interact state
        }
        
    }
}
