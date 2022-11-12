using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{

    public static ResourcesManager resourcesManager;
    public int gold = 0;
    public int wood = 0;

    void Awake()
    {
        resourcesManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
