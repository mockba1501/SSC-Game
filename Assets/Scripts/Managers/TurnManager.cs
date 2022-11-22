using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static TurnManager turnManager;
    public int currentTurnNumber = 0;

    void Awake()
    {
        turnManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTurn(){
        currentTurnNumber++;
        
        for(var i = 0; i < BuildingsManager.buildingManager.buildings.Count; i++){
            ResourcesManager.resourcesManager.freeMoney += BuildingsManager.buildingManager.buildings[i].taxIncome;
        }

        // Reset building indicators
        if(GridBuilding.gridBuilding.buildingToBuildInstance != null){
            GridBuilding.gridBuilding.BuildingIndicators(GridBuilding.gridBuilding.buildingToBuildInstance.transform.position);
        }
    }
}
