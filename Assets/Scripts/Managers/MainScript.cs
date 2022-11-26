using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public string title;
    
    // Start is called before the first frame update
   public void ChangeSecneDynamic(string title)
    {
         SceneManager.LoadScene(title);    
    
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    
}
