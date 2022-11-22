using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SustKeys
{
    public int population;
    public int energy;
    public int poverty;    
    public int clean;

}
public struct LevelBoard
{
    int Level;
    Building[,] Tiles;

    public void SetBoard(int level, Building[,] tiles)
    {
        Level = level;
        Tiles = tiles;
    }

    public Building[,] GetTiles()
    {
        return Tiles;
    }

    public void SetTile(int x, int y, Building b)
    {
        Tiles[x, y] = b;
    }
}


public struct Level
{
    public string objective;
    public int targetPopulation;
    public int targetEnergy;
    public int targetPoverty;
    public int targetClean;

}



public class LevelManager : MonoBehaviour
{
    
    //public static LevelManager instance;

    const int LEVELS = 5;
    public int currentLevel = 0;

    Level[] levels = new Level[LEVELS];

    //initialize empty game tiles
    LevelBoard[] boards =
    {
            new LevelBoard(),
            new LevelBoard(),
            new LevelBoard(),
            new LevelBoard(),
            new LevelBoard(),
        };




    public static LevelManager Instance { get; private set; }


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    SustKeys sKeys;

    // Start is called before the first frame update
    void Start()
    {

        InitializeLevels();
        //initialize empty game tiles
        for (int i = 0; i < LEVELS; i++)
        {            
            boards[i].SetBoard(i + 1, new Building[20, 20]);
        }

        sKeys.energy = 0;
        sKeys.poverty = 0;
        sKeys.population= 0;
        sKeys.clean = 0;

        currentLevel = 0;
    }

    public Level getLevel(int numLevel)
    {
        if (numLevel - 1 < 0 || numLevel - 1 > LEVELS - 1)
            throw new ArgumentOutOfRangeException();
        else
        {
            return levels[numLevel - 1];
        
        }
    }

    private void InitializeLevels()
    {
        levels[0].objective = "level 0 description...";
        levels[1].objective = "level 1 description...";
        levels[2].objective = "level 2 description...";
        levels[3].objective = "level 3 description...";
        levels[4].objective = "level 4 description...";

        levels[0].targetPopulation= 50;
        levels[0].targetEnergy = 0;
        levels[0].targetPoverty = 0;
        levels[0].targetClean = 0;

        levels[1].targetPopulation = 50;
        levels[1].targetEnergy = 10;
        levels[1].targetPoverty = 30;
        levels[1].targetClean = 20;

        levels[2].targetPopulation = 50;
        levels[2].targetEnergy = 10;
        levels[2].targetPoverty = 30;
        levels[2].targetClean = 20;


        levels[3].targetPopulation = 50;
        levels[3].targetEnergy = 10;
        levels[3].targetPoverty = 30;
        levels[3].targetClean = 20;

        levels[4].targetPopulation = 50;
        levels[4].targetEnergy = 10;
        levels[4].targetPoverty = 30;
        levels[4].targetClean = 20;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceBuilding(Building b, int x, int y)
    {
        if (IsTileEmpty(currentLevel, x, y))
        {
            boards[currentLevel].SetTile(x, y, b);
            SetKeys(b);
        }
    }

    public void SetKeys(Building b)
    {
        SustKeys bKeys = b.GetKeys();
        sKeys.energy += bKeys.energy;
        sKeys.population += bKeys.population;
        sKeys.poverty += bKeys.poverty;
        sKeys.clean += bKeys.clean;
        Debug.Log("Updating KPIs (population=" +  sKeys.population.ToString());
    }

    bool IsTileEmpty(int level, int x, int y)
    {
        return boards[level].GetTiles()[x,y] == null;
    }

    public bool isLevelFinished()
    {
        bool levelFinished = true;
        levelFinished &= levels[currentLevel].targetClean <= sKeys.clean;
        levelFinished &= levels[currentLevel].targetPopulation <= sKeys.population;
        levelFinished &= levels[currentLevel].targetEnergy <= sKeys.energy;
        levelFinished &= levels[currentLevel].targetPoverty <= sKeys.poverty;
        return levelFinished;
    }
}
