using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public static BuildingsManager buildingManager;
    public Building currentBuilding;
    public List<Building> buildings;


    void Awake()
    {
        buildingManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
