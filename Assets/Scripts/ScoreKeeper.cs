using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{

    public int score= 0;
    public TMP_Text secoreText;
    //public TextMeshProUGUI nameField;
   // public static ScoreKeeper instance;
    
    
   
 void Start()
    {
       TMP_Text secoreText=this.GetComponent<TMP_Text>();
       // Reset();
       // secoreText.text=score.ToString();
       score=0;

    }

    // Update is called once per frame
  public void ChangeScore(int points)
  {
      score+=points;
      secoreText.text=score.ToString();
  }

  public  void Reset()
  {
      score=0;
    
  }
    
}
