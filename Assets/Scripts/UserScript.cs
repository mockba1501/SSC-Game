using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Globalization;


public class UserScript : MonoBehaviour
{

 public InputField inputField;
 public Text textDisplay;
 public string secneName;
 public string module;
 public string secneRedirector;
 private int hour;
 private int minutes;
 private int seconds;
 private string timeStamp;
 public int score;
 private int wrong_answer_count=0;
 private int right_answer=0;
 public string answer;
 //public int wrong_answer_count=0;

// hour +":" +minutes ":" + minutes + ":"+ seconds
 
void Start()
{

//ifif.text = this.playerName;

}

   public void addUser()
   {

     StartCoroutine(callUser());
   }
   IEnumerator callUser()
     {
        GameObject ifgo = GameObject.Find("InputField (TMP)");
        TMP_InputField ifif = ifgo.GetComponent<TMP_InputField>();
        
        GameObject ifgo2 = GameObject.Find("InputField (TMP)1");
        TMP_InputField ifif2 = ifgo2.GetComponent<TMP_InputField>();
       

       
       if(ifif2.text!="" && ifif.text!="" ){

        WWWForm form = new WWWForm();
        form.AddField("email",ifif2.text);
        form.AddField("name",ifif.text);
        form.AddField("secne_name","Start");
       // hour = System.DateTime.Now.Hour;
        //minutes = System.DateTime.Now.minutes;
        //seconds = System.DateTime.Now.seconds;
        //Debug.Log(hour +"" +);
       // form.AddField("per_secne_time",);
       // form.AddField("overall_count_time",);
        WWW www = new WWW("https://aasianpood.ee/add_user.php",form);
        yield return www;
        yield return new WaitForSeconds(1.0f);

         PlayerPrefs.SetString("userEmail", ifif2.text);
         PlayerPrefs.SetString("userName", ifif.text);


         Debug.Log(www.text);
         SceneManager.LoadScene("Play");
       }else{
         GameObject text = GameObject.Find("EmailRequired");
         TextMeshProUGUI textShow = text.GetComponent<TextMeshProUGUI>();
 
         GameObject text2 = GameObject.Find("NickNameRequired");
         TextMeshProUGUI textShow2 = text2.GetComponent<TextMeshProUGUI>();

          if(PlayerPrefs.HasKey("language"))
         {
          var language=PlayerPrefs.GetInt("language");
          Debug.Log("Language is aaaa: "+language);

            if(language==0 )
            {
               textShow.text="Please Enter Email";
               textShow2.text="Please Enter NickName";
             }

             if(language==1 )
            {
               // turkishNumberSound.Play();
                textShow.text="giriniz e-posta Lütfen";
                textShow2.text="Girin Adını Kullanıcı Lütfen";
            
                
                
            }

             if(language==2 )
             {
                 textShow.text="براہ کرم ای میل درج کریں۔";              
                 textShow2.text="براہ کرم عرفی نام درج کریں۔  ";
             
              // urdunNmberSound.Play();
             }

        }
         
       }

     }

 public void addDataThroughComponent()
 {
     StartCoroutine(dataThroughComponent());
 }

  IEnumerator dataThroughComponent()
  {
     Debug.Log(secneRedirector);
    
     Debug.Log(PlayerPrefs.GetString("userEmail"));
     Debug.Log(PlayerPrefs.GetString("userName"));

       // hour = System.DateTime.Now.Hour;
       // minutes = System.DateTime.Now.Minute;
        //seconds = System.DateTime.Now.Second;
        timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
     
        WWWForm form = new WWWForm();
        form.AddField("email",PlayerPrefs.GetString("userEmail"));
        form.AddField("name",PlayerPrefs.GetString("userName"));
        form.AddField("score",0);
        form.AddField("per_secne_time",timeStamp);
        form.AddField("overall_count_time",timeStamp);
        form.AddField("secne_name",secneName);
        form.AddField("module",module);
        form.AddField("wrong_answer_count",wrong_answer_count);
        form.AddField("right_answer",right_answer);
       

        WWW www = new WWW("https://aasianpood.ee/add_user1.php",form);
        yield return www;
        yield return new WaitForSeconds(1.0f);
        
        PlayerPrefs.SetString("userId", www.text);


          Debug.Log("Game Saved.");
           if(answer==""){
               SceneManager.LoadScene(secneRedirector);
        }
       

  }
 
 public void addExamThroughComponent(string answer)
 {
  
     Debug.Log(secneRedirector);
    
    if(answer=="wrong")
    {
      wrong_answer_count++;
      Debug.Log(wrong_answer_count);
  
      if(wrong_answer_count==0) {
          StartCoroutine(examThroughComponent(answer));
      }else{
        StartCoroutine(examUpdateThroughComponent(answer));
      }
    }else{
      if(right_answer==0) {
         right_answer++;
          StartCoroutine(examThroughComponent(answer));
      }else{
        StartCoroutine(examUpdateThroughComponent(answer));
      }

    // StartCoroutine(examThroughComponent());
    
     
    }   

    // StartCoroutine(examThroughComponent());
 }


  IEnumerator examThroughComponent(string answer)
  {

    
     Debug.Log(secneRedirector);
    
     Debug.Log(PlayerPrefs.GetString("userEmail"));
     Debug.Log(PlayerPrefs.GetString("userName"));

       // hour = System.DateTime.Now.Hour;
       // minutes = System.DateTime.Now.Minute;
        //seconds = System.DateTime.Now.Second;
        timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
     
        WWWForm form = new WWWForm();
        form.AddField("email",PlayerPrefs.GetString("userEmail"));
        form.AddField("name",PlayerPrefs.GetString("userName"));
        form.AddField("score",0);
        form.AddField("per_secne_time",timeStamp);
        form.AddField("overall_count_time",timeStamp);
        form.AddField("secne_name",secneName);
        form.AddField("module",module);
        form.AddField("wrong_answer_count",wrong_answer_count);
        form.AddField("right_answer",right_answer);
       
        WWW www = new WWW("https://aasianpood.ee/add_user.php",form);
        yield return www;
        yield return new WaitForSeconds(1.0f);
        
        PlayerPrefs.SetString("userId", www.text);
        Debug.Log("Game Saved.");
        

         if(answer==""){
               SceneManager.LoadScene(secneRedirector);
        }
       
          // SceneManager.LoadScene(secneRedirector);

  }
 

 IEnumerator examUpdateThroughComponent(string answer)
  {


     Debug.Log(secneRedirector);
    
     Debug.Log("Update");
     Debug.Log(PlayerPrefs.GetString("userId"));
     Debug.Log(PlayerPrefs.GetString("userId"));

       // hour = System.DateTime.Now.Hour;
       // minutes = System.DateTime.Now.Minute;
        //seconds = System.DateTime.Now.Second;
        timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        WWWForm form = new WWWForm();
        form.AddField("id",PlayerPrefs.GetString("userId"));
        form.AddField("email",PlayerPrefs.GetString("userEmail"));
        form.AddField("name",PlayerPrefs.GetString("userName"));
        form.AddField("score",score);
        form.AddField("per_secne_time",timeStamp);
        form.AddField("overall_count_time",timeStamp);
        form.AddField("secne_name",secneName);
        form.AddField("module",module);
        form.AddField("wrong_answer_count",wrong_answer_count);
        form.AddField("right_answer",0);
       
        WWW www = new WWW("https://aasianpood.ee/add_user1.php",form);
        yield return www;
        yield return new WaitForSeconds(1.0f);
        
        PlayerPrefs.SetString("userId", www.text);
        Debug.Log(www.text);
           //SceneManager.LoadScene(secneRedirector);
        if(answer=="")
       {
               SceneManager.LoadScene(secneRedirector);
       }

  }
 
  

}
