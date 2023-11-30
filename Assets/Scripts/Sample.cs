using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Sample : MonoBehaviour
{

    

    public void OnValueChange(TMP_InputField ifif)
    {
      // ifif.GetComponent<TMP_InputField>().text;
      Debug.Log(ifif.text);
    }
    public void OnEndEdit(TMP_InputField ifif)
    {
        Debug.Log(ifif.text);
    }
}
