using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool placed {get; private set; }
    public BoundsInt area;


    private void Awake(){
        this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
    }


    public bool CanBePlaced(){
        if(GridBuilding.gridBuilding.CanTakeArea(area))
        {
            return true;
        }
        
        return false;
    }


    public void Place(){
        placed = true;
        GridBuilding.gridBuilding.TakeArea(area);
        this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
    }
}
