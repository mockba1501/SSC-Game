using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class for all buildings
public class Building : MonoBehaviour
{

    [SerializeField]
    SustKeys keys;

    // Start is called before the first frame update
    void Start()
    {
        keys.energy = 0;
        keys.poverty = 0;
        keys.entertainment = 0;
        keys.waste = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SustKeys GetKeys()
    {
        return keys;
    }
}
