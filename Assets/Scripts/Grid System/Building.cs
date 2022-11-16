using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string buildingName;
    public int price;
    public int goldProduction;
    public int woodProduction;
    
    
    //KPIs for level target
    public int population;
    public int poverty;
    public int energy;
    public int clean;

    public bool placed {get; private set; }
    public GameObject destroyParticles;
    public BoundsInt area;


    private void Awake(){
        this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
    }

    void Start()
    {
        
    }

    void Update(){

    }

    public bool CanBePlaced(){
        if(GridBuilding.gridBuilding.CanTakeArea(area) && ResourcesManager.resourcesManager.gold >= price)
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
        ResourcesManager.resourcesManager.gold -= price;
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
