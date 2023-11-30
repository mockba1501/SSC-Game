using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BestMatchScript : MonoBehaviour
{
    public string correctAnswer;   
    public string answerAnswer;
    public int scoreValue = 10;
   // private int scoreKeeper;
    public string title;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       // Text text = this.GetComponent<Text>();
        //text.text = ScoreKeeper.instance.score.ToString();
        //ScoreKeeper.Reset();
    }

   public void GetAnswer(string answerAnswer)
    {
       // Debug.Log(answerAnswer);
        if(correctAnswer==answerAnswer)
        {
           // ScoreKeeper.instance.ChangeScore(scoreValue); 
           
            SceneManager.LoadScene(title);    
       
            //
            
        }

    }

}
