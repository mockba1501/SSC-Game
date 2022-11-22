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


    public TextMeshProUGUI eletricProductionText;
    public TextMeshProUGUI electricityConsumptionText;
    public TextMeshProUGUI taxIncomeText;
    public TextMeshProUGUI freeMoneyText;
    public TextMeshProUGUI turnNumberText;
    public TextMeshProUGUI populationText;

    public GameObject buySolarPanelsButton;
    public TextMeshProUGUI buySolarPanelsButtonText;

    public GameObject levelDescription;
    public bool showLevelDescription;


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
        SetBuySolarPanelsButton();


        eletricProductionText.SetText("Eletric production: "+ResourcesManager.resourcesManager.GetEletricProduction().ToString()+" kw ");
        electricityConsumptionText.SetText("Eletric consumption: "+ResourcesManager.resourcesManager.GetEletricConsumption().ToString()+" kw ");

        freeMoneyText.SetText("Money: "+ResourcesManager.resourcesManager.freeMoney.ToString()+"M");
        taxIncomeText.SetText("Tax income: "+ResourcesManager.resourcesManager.GetTaxIncome().ToString()+"M/turn ");
        turnNumberText.SetText("Turn: "+TurnManager.turnManager.currentTurnNumber.ToString());
        populationText.SetText("Population: "+ResourcesManager.resourcesManager.GetTotalPopulation().ToString());
    }


    public void SetBuildingToBuild(GameObject building){
        GridBuilding.gridBuilding.buildingToBuild = building;
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
            buildingImage.sprite = building.gameObject.GetComponent<SpriteRenderer>().sprite;


            buildingAttributesText.SetText("");

            if(building.placed == false){
                buildingAttributesText.SetText("Price: "+building.price+"\n\n");
            }

            if(building.hasEletricity == false && building.placed){
                buildingAttributesText.SetText(buildingAttributesText.text+"HAS NO ELETRICITY!\n\n");
            }

            buildingAttributesText.SetText(buildingAttributesText.text+"Population: "+building.population+"\n\n");
            buildingAttributesText.SetText(buildingAttributesText.text+"Tax income: "+building.taxIncome+"\n\n");

            if(building.hasSolarPanels){
                buildingAttributesText.SetText(buildingAttributesText.text+"Solar panel production: "+building.solarPanelEletricityProduction+" kw\n\n");
            }

            buildingAttributesText.SetText(buildingAttributesText.text+"Eletricity production: "+building.electricityProduction+"\n\n");
            buildingAttributesText.SetText(buildingAttributesText.text+"Eletricity consumption: "+building.electricityConsumption+"\n\n");





            BuildingInfoPanel.SetActive(true);
        }
    }

    public void NextTurnButtonPressed(){
        TurnManager.turnManager.NextTurn();
    }

    public void BuySolarPanelsButtonPressed(){
        Building building = null;
        
        if(BuildingsManager.buildingManager.currentBuilding != null){
            building = BuildingsManager.buildingManager.currentBuilding;
        }else{
            return;
        }

        building.BuildSolarPanels();
    }

    public void SetBuySolarPanelsButton(){
        Building building = null;
        
        if(BuildingsManager.buildingManager.currentBuilding != null){
            building = BuildingsManager.buildingManager.currentBuilding;
        }else{
            buySolarPanelsButton.SetActive(false);
            return;
        }

        if(building.hasSolarPanels || !building.canHaveSolarPanels){
            buySolarPanelsButton.SetActive(false);
            return;
        }


        buySolarPanelsButtonText.SetText("Buy solar panels ("+building.solarPanelPrice.ToString()+"M)");
        buySolarPanelsButton.SetActive(true);

        if(ResourcesManager.resourcesManager.freeMoney >= building.solarPanelPrice){
            buySolarPanelsButton.GetComponent<Button>().interactable = true;
        }else{
            buySolarPanelsButton.GetComponent<Button>().interactable = false;
        }
    }
}
