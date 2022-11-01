using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public bool placed {get; private set; }
    public BoundsInt area;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public bool CanBePlaced(){

        Vector3Int positionInt = GridBuilding.gridBuilding.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(GridBuilding.gridBuilding.CanTakeArea(areaTemp))
        {
            return true;
        }
        
        return false;
    }

    public void Place(){
        Vector3Int positionInt = GridBuilding.gridBuilding.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        placed = true;
        GridBuilding.gridBuilding.TakeArea(areaTemp);
    }




    private void OnMouseDown(){
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag(){
        if(!placed){
            Vector3 pos = GetMouseWorldPosition() + offset;
            transform.position = SnapCoordinateToGrid(pos);
            GridBuilding.gridBuilding.FollowBuilding();
        }
    }

    public static Vector3 GetMouseWorldPosition(){

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit){
            return hit.point;
        }else{
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position){
        Vector3Int cellPos = GridBuilding.gridBuilding.gridLayout.WorldToCell(position);
        position = GridBuilding.gridBuilding.gridLayout.GetComponent<Grid>().GetCellCenterWorld(cellPos);
        return position;
    }
}
