using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    // General
    public string buildingName;
    public int price;
    public int taxIncome;
    
    //KPIs for level target
    public int population;
    public int poverty;
    public int energy;
    public int clean;

    public bool placed {get; private set; }
    public GameObject destroyParticles;
    public BoundsInt area;
    public bool hasClearCenter = true;

    // Solar panels
    public bool canHaveSolarPanels = true;
    public bool hasSolarPanels;
    public int solarPanelPrice;
    public int solarPanelEletricityProduction;
    public GameObject noEletricityWarning;
    public GameObject solarPanels;


    // Eletricity
    public bool hasEletricity;
    public int electricityProduction;
    public int electricityConsumption;



    private void Awake(){
        this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.7f);
    }

    void Start()
    {
        
    }

    void Update(){
        IndicatePowerAvailability();
    }

    public bool CanBePlaced(){
        if(GridBuilding.gridBuilding.CanTakeArea(area) && ResourcesManager.resourcesManager.freeMoney >= price)
        {
            return true;
        }
        
        return false;
    }


    public void Place(){
        placed = true;
        GridBuilding.gridBuilding.TakeArea(area);
        this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        BuildingsManager.buildingManager.buildings.Add(this);
        ResourcesManager.resourcesManager.freeMoney -= price;
        LevelManager.Instance.SetKeys(this);
    }


    public void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && placed){
            SelectBuilding();
        }
    }

    public void SelectBuilding(){
        UIManager.uiManager.showDeleteButton = true;
        BuildingsManager.buildingManager.currentBuilding = this;
    }

    public void DestroyBuilding(){
        BuildingsManager.buildingManager.buildings.Remove(this);
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
        GridBuilding.gridBuilding.ClearBuildingArea(area);
        Destroy(this.gameObject);
    }


    public void BuildSolarPanels(){

        ResourcesManager.resourcesManager.freeMoney -= solarPanelPrice;
        hasSolarPanels = true;
        solarPanels.SetActive(true);

        if(solarPanelEletricityProduction > electricityConsumption){
            electricityProduction += solarPanelEletricityProduction-electricityConsumption;
        }
    }


    public int GetConsumptionWithSolarPanels(){
        return electricityConsumption - solarPanelEletricityProduction;
    }

    public void IndicatePowerAvailability(){
        if(electricityConsumption > 0){
            if(hasEletricity){
                noEletricityWarning.SetActive(false);
            }else{
                if(placed){
                    noEletricityWarning.SetActive(true);
                }else{
                    noEletricityWarning.SetActive(false);
                }
            }
        }
    }
    
    public SustKeys GetKeys()
    {
        SustKeys keys;
        keys.population = population;
        keys.energy= energy;
        keys.poverty= poverty;
        keys.clean= clean;

        return keys;
    }
}
