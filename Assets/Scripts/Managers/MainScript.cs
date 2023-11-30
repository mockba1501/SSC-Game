using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Localization.Settings;

public class MainScript : MonoBehaviour
{
    public string title;
    private string PrevScene;
    private const string lan = "language";
    private int language = 0;
    

    //private string scene1;
   // private string[] scene1 = new string[2];
    private List<string> scene1 = new List<string>();  //running history of scenes
    public AudioSource turkishNumberSound;
    public AudioSource urdunNmberSound;
    public AudioSource engNumberSound;
    public AudioSource clappedSound;

    public Image eng;
    public Image urdu;
    public Image turkish;
    public string secneName;
    Scene scene;  
  public  void Start()
  {

           language = PlayerPrefs.GetInt(lan, 0); 
           scene = SceneManager.GetActiveScene();
           PrevScene = PlayerPrefs.GetString("SceneNumber");
           PlayerPrefs.SetString("SceneNumber", SceneManager.GetActiveScene().name);
           PrevScene = PlayerPrefs.GetString("SceneNumber");
           ChangeDynamicSound();

         if(language==0 && secneName=="Currency")
         {
               
          
                turkish.enabled = false;
                urdu.enabled = false;
                eng.enabled = true;
              
         }

             if(language==1 && secneName=="Currency")
            {
                //turkish = GameObject.Find("TurkishImage").GetComponent<Image>();
               turkish.enabled = true;
                urdu.enabled = false;
                eng.enabled = false;
                      //Debug.Log("i am here22");
                      
            }

             if(language==2 && secneName=="Currency")
             {
                 //urdu.enabled =true;
                
                turkish.enabled = false;
                urdu.enabled = true;
                eng.enabled = false;
             }


    }
    
    // Start is called before the first frame update
   public void ChangeSecneDynamic(string title)
    {  

      
       SceneManager.LoadScene(title);    
       
    
    }


   public void ChangeDynamicSound()
    {
       /// Debug.Log(" name is: " + title);

        if(PlayerPrefs.HasKey("language"))
        {
          var language=PlayerPrefs.GetInt("language");
          //Debug.Log("Language is aaaa: "+language);

            if(language==0 && engNumberSound!=null)
            {
               engNumberSound.Play();
             }

             if(language==1 && turkishNumberSound!=null)
            {
               turkishNumberSound.Play();
            }

             if(language==2 && turkishNumberSound!=null)
             {
               urdunNmberSound.Play();
             }

        }

    }

    public void ChangeScene()
    { 
       //Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
       Debug.Log(" name is22: " + title);
       Debug.Log(" name is225: " + PrevScene);
       SceneManager.LoadScene(title);
    }

    public void ChangeSecneDynamicWithSound(string title)
    {  
       clappedSound.Play();
       SceneManager.LoadScene(title);    
       
    }

   public void onChangeLang(int lang)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lang];
       //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(lang);
        PlayerPrefs.SetInt(lan, lang);
        PlayerPrefs.GetInt(lan, 1);
        Debug.Log("I am here!!");
        SceneManager.LoadScene("Main");
        AudioController.audioController.PlayClickAudio();
    
        
    }

    public void redirecApplication(){
      Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeqFh2ergfzlG0aPjLKC32l1LhO5uHFeYWs6esT9xXBOnJmCw/viewform");
    }

}
