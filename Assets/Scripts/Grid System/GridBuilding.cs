using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridBuilding : MonoBehaviour
{


    public static GridBuilding gridBuilding;
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    public GameObject testBuilding;

    public Tile buildingAreaTile;
    public Tile free;
    public Tile occupied;


    public void Awake(){
        gridBuilding = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.BuildingArea, buildingAreaTile);
        tileBases.Add(TileType.FreeIndicator, free);
        tileBases.Add(TileType.OccupiedIndicator, occupied);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            InitializeBuilding(testBuilding);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(temp.CanBePlaced()){
                temp.Place();
            }
        }
    }



    public void InitializeBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    public enum TileType
    {
        BuildingArea,
        FreeIndicator,
        OccupiedIndicator,
        Empty
    }


    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }


    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }



    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }


    public void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++){
            if(baseArray[i] == null){
                tileArray[i] = tileBases[TileType.FreeIndicator];
            }else{
                FillTiles(tileArray, TileType.OccupiedIndicator);
            }
        }

        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);
        foreach(var b in baseArray)
        {
            if(b != null){
                Debug.Log("cant place here!");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area){
        SetTilesBlock(area, TileType.Empty, tempTilemap);
        SetTilesBlock(area, TileType.FreeIndicator, mainTilemap);
    }
}
