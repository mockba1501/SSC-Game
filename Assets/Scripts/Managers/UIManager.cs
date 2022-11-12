using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    public GameObject house_small;
    public GameObject house_big;

    public GameObject deleteButton;
    public bool showDeleteButton;

    public GameObject placeButton;
    public GameObject cancelPlacementButton;

    public GameObject BuildingInfoPanel;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingAttributesText;
    public Image buildingImage;

    public TextMeshProUGUI goldAmountText;
    public TextMeshProUGUI woodAmountText;
    public TextMeshProUGUI turnNumberText;



    void Awake()
    {
        uiManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        deleteButton.SetActive(showDeleteButton);
        placeButton.SetActive(ShowPlaceButton());
        cancelPlacementButton.SetActive(ShowPlaceButton());
        SetBuildingInfoPanel();

        goldAmountText.SetText("Gold: "+ResourcesManager.resourcesManager.gold.ToString());
        woodAmountText.SetText("Wood: " +ResourcesManager.resourcesManager.wood.ToString());
        turnNumberText.SetText("Turn: "+TurnManager.turnManager.currentTurnNumber.ToString());
    }

    public void setSmallHouse(){
        GridBuilding.gridBuilding.buildingToBuild = house_small;
        GridBuilding.gridBuilding.TryCreateBuildingPrototype();
    }

    public void setBugHouse(){
        GridBuilding.gridBuilding.buildingToBuild = house_big;
        GridBuilding.gridBuilding.TryCreateBuildingPrototype();
    }


    public void DeleteButtonPressed(){
        BuildingsManager.buildingManager.currentBuilding.DestroyBuilding();
    }

    public void PlaceButtonPressed(){
        GridBuilding.gridBuilding.TryToPlaceBuilding();
    }

    public void CancelPlacementButtonPressed(){
        GridBuilding.gridBuilding.CancelPlacement();
    }

    public bool ShowPlaceButton(){
        if(GridBuilding.gridBuilding.buildingToBuildInstance != null){
            return true;
        }else{
            return false;
        }
    }

    public void SetBuildingInfoPanel(){
        Building building = null;
        
        if(GridBuilding.gridBuilding.buildingToBuildInstance != null){
            building = GridBuilding.gridBuilding.buildingToBuildInstance;
        }else{
            if(BuildingsManager.buildingManager.currentBuilding != null){
                building = BuildingsManager.buildingManager.currentBuilding;
            }else{
                BuildingInfoPanel.SetActive(false);
            }
        }

        if(building != null){
            buildingNameText.SetText(building.buildingName);
            buildingAttributesText.SetText("Price: \n"+building.price+"\n\n"+"Gold production: \n"+building.goldProduction+"\n\n"+"Wood production: \n"+building.woodProduction);
            buildingImage.sprite = building.gameObject.GetComponent<SpriteRenderer>().sprite;
            BuildingInfoPanel.SetActive(true);
        }
    }

    public void NextTurnButtonPressed(){
        TurnManager.turnManager.NextTurn();
    }
}
