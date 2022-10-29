using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SustKeys
{
    public int energy;
    public int poverty;
    public int entertainment;
    public int waste;

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



public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    const int LEVELS = 5;
    int currentLevel = 1;
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
        //initialize empty game tiles
        for (int i = 0; i < LEVELS; i++)
        {
            boards[i].SetBoard(i + 1, new Building[20, 20]);
        }

        sKeys.energy = 0;
        sKeys.poverty = 0;
        sKeys.entertainment = 0;
        sKeys.waste = 0;
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

    void SetKeys(Building b)
    {
        SustKeys bKeys = b.GetKeys();
        sKeys.energy += bKeys.energy;
        sKeys.entertainment += bKeys.entertainment;
        sKeys.poverty += bKeys.poverty;
        sKeys.waste += bKeys.waste;
    }

    bool IsTileEmpty(int level, int x, int y)
    {
        return boards[level].GetTiles()[x,y] == null;
    }

    bool isLevelFinished()
    {
        return false;
    }
}
