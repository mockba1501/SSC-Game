using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public string levelDescription;
    public string levelSceneName;
    public int targetPopulation;
    public int targetEnergy;
    public int targetPoverty;
    public int targetClean;

}



public class LevelManager : MonoBehaviour
{
    
    //public static LevelManager instance;

    public Button[] lvlButtons;
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
        int levelAt = PlayerPrefs.GetInt("levelAt", 2); 

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                lvlButtons[i].interactable = false;
        }
        InitializeLevels();
      /*
        //initialize empty game tiles
        for (int i = 0; i < LEVELS; i++)
        {            
            boards[i].SetBoard(i + 1, new Building[20, 20]);
        }
      */
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

        int i = 0;
        

        levels[i].targetPopulation= 50;
        levels[i].targetEnergy = 0;
        levels[i].targetPoverty = 0;
        levels[i].targetClean = 0;
        levels[i].levelDescription = string.Format("Level 1: this is an easy one. You must reach a population of {0} people", levels[i].targetPopulation);
        levels[i].levelSceneName = "Level" + i;

        i++;

        levels[i].targetPopulation = 50;
        levels[i].targetEnergy = 10;
        levels[i].targetPoverty = 30;
        levels[i].targetClean = 20;
        levels[i].levelDescription = string.Format("Level 2: now you have to be careful. You must reach a population of {0} people, and the cleaness has to be {1}", levels[i].targetPopulation, levels[i].targetClean);
        levels[i].levelSceneName = "Level" + i;

        i++;
        levels[i].targetPopulation = 50;
        levels[i].targetEnergy = 10;
        levels[i].targetPoverty = 30;
        levels[i].targetClean = 20;
        levels[i].levelDescription = string.Format("Level 3: this is serious. You must reach a population of {0} people, energy minimum of {1}, wealth should be {2} and the cleaness has to be {3}", levels[i].targetPopulation, levels[i].targetEnergy, levels[i].targetPoverty, levels[i].targetClean);
        levels[i].levelSceneName = "Level" + i;

        i++;
        levels[i].targetPopulation = 50;
        levels[i].targetEnergy = 10;
        levels[i].targetPoverty = 30;
        levels[i].targetClean = 20;
        levels[i].levelDescription = string.Format("Level 4: this is pro series. You must reach a population of {0} people, energy minimum of {1}, wealth should be {2} and the cleaness has to be {3}", levels[i].targetPopulation, levels[i].targetEnergy, levels[i].targetPoverty, levels[i].targetClean);
        levels[i].levelSceneName = "Level" + i;

        i++;
        levels[i].targetPopulation = 50;
        levels[i].targetEnergy = 10;
        levels[i].targetPoverty = 30;
        levels[i].targetClean = 20;
        levels[i].levelDescription = string.Format("Level 5: Final One! You must reach a population of {0} people, energy minimum of {1}, wealth should be {2} and the cleaness has to be {3}", levels[i].targetPopulation, levels[i].targetEnergy, levels[i].targetPoverty, levels[i].targetClean);
        levels[i].levelSceneName = "Level" + i;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*

    void PlaceBuilding(Building b, int x, int y)
    {
        if (IsTileEmpty(currentLevel, x, y))
        {
            boards[currentLevel].SetTile(x, y, b);
            SetKeys(b);
        }
    }
    */
    public void SetKeys(Building b)
    {
        SustKeys bKeys = b.GetKeys();
        sKeys.energy += bKeys.energy;
        sKeys.population += bKeys.population;
        sKeys.poverty += bKeys.poverty;
        sKeys.clean += bKeys.clean;
        string log= String.Format("Updating KPIs (population={0}, energy={1}, poverty={2}, clean={3}", sKeys.population, sKeys.energy, sKeys.poverty, sKeys.clean);
        Debug.Log(log);
    }

    /*
    bool IsTileEmpty(int level, int x, int y)
    {
        return boards[level].GetTiles()[x,y] == null;
    }
    */

    public bool IsLevelFinished()
    {
        return true;
        bool levelFinished = true;
        levelFinished &= levels[currentLevel].targetClean <= sKeys.clean;
        levelFinished &= levels[currentLevel].targetPopulation <= sKeys.population;
        levelFinished &= levels[currentLevel].targetEnergy <= sKeys.energy;
        levelFinished &= levels[currentLevel].targetPoverty <= sKeys.poverty;
        return levelFinished;
    }
    public bool NextLevel()
    {
        currentLevel++;
        if (currentLevel == LEVELS)
        {
            Debug.Log("Max levels reached. Endgame");
            return false;
        }
        else
        {
            Debug.Log("NextLevel: Current level= " + currentLevel);
            SceneManager.LoadScene(levels[currentLevel].levelSceneName, LoadSceneMode.Single);
            return true;
        }

    }

    /// <summary>
    /// Call this method from UI to show level description and 
    /// objetives
    /// </summary>
    /// <returns>Level description</returns>
    public string GetLevelDescription()
    {
        return levels[currentLevel].levelDescription;
    }
}
