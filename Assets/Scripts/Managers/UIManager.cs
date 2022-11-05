using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject house_small;
    public GameObject house_big;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSmallHouse(){
        GridBuilding.gridBuilding.buildingToBuild = house_small;
    }

    public void setBugHouse(){
        GridBuilding.gridBuilding.buildingToBuild = house_big;
    }
}
