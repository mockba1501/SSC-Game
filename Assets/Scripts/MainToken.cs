using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToken : MonoBehaviour
{
    [SerializeField] Transform puzzleField;
    [SerializeField] GameObject btn;

    void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i; // Without this line, the instantiated objects would just be called "Puzzle Button (Clone)"
            button.transform.SetParent(puzzleField, false);
        }
    }
}
