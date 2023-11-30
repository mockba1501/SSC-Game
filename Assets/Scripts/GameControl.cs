using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public List<Button> btn = new List<Button>();
    [SerializeField]
    private Sprite bgImage; 
    public List<Sprite> gamePuzzle = new List<Sprite>();
    public Sprite[] puzzles;
    private int countGuesses;
    private int countCorrectGuess;
    private int gameGuesses;
    private bool firstGuess,secondGuess;
    private int firstGuessIndex,secondGuessIndex;
    private string firstGuessPuzzle,secondGuessPuzzle;

    void Awake()
    {
       puzzles = Resources.LoadAll<Sprite>("Sprites/Counting");
    }    

    void Start()
    {
      getButtons();
      AddListeners();
      AddGamePuzzle();
      gameGuesses = gamePuzzle.Count / 2;
     
    }    

     void getButtons()
    {
       GameObject[] objects =  GameObject.FindGameObjectsWithTag("PuzzleButton");
       for(int i=0; i<objects.Length; i++)
       {
            btn.Add(objects[i].GetComponent<Button>());
            btn[i].image.sprite = bgImage;
       }
     
     }

     void AddGamePuzzle()
     {
        int looper = btn.Count;
        int index = 0;
         Debug.Log("Here!!!!");
         Debug.Log(looper);
        for(int i =0; i<looper; i++)
        {
            if(index==looper/2){
                index=0;
            }
            Debug.Log(puzzles[index]);
            gamePuzzle.Add(puzzles[index]);
            index++;

        }

     }   

     void AddListeners()
     {

        foreach(Button b in btn)
        {

            b.onClick.AddListener(()=>pickAPuzzle());
        }
     }

     public void pickAPuzzle()
     {
           string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
           if(!firstGuess)
           {
             firstGuess=true;
             firstGuessIndex= int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
             btn[firstGuessIndex].image.sprite = gamePuzzle[firstGuessIndex];
           }else if(!secondGuess){

             secondGuess=true;
             secondGuessIndex= int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
             btn[secondGuessIndex].image.sprite = gamePuzzle[secondGuessIndex];
             countGuesses++;    
             StartCoroutine(CheckIfThePuzzlesMatch());

           }

            Debug.Log("I am here!!"+name);
     }    

     IEnumerator CheckIfThePuzzlesMatch()
     {

        yield return new WaitForSeconds(1f);
        if(firstGuessPuzzle==secondGuessPuzzle){
          yield return new WaitForSeconds(.5f);
          btn[firstGuessIndex].interactable = false;
          btn[secondGuessIndex].interactable = false;
          btn[firstGuessIndex].image.color = new Color(0,0,0,0);
          btn[secondGuessIndex].image.color = new Color(0,0,0,0);
 
           CheckIfTheGameIsFinished();
        }else{
            btn[firstGuessIndex].image.sprite = bgImage;
            btn[secondGuessIndex].image.sprite = bgImage;
        }

          yield return new WaitForSeconds(.5f);
          firstGuess  = secondGuess = false;
          //secondGuess  = false;
     }

     void CheckIfTheGameIsFinished(){
        countCorrectGuess++;
        if(countCorrectGuess==gameGuesses){
            Debug.Log("Game finished");
            Debug.Log("It took you"+countGuesses);
        }
     }
}
