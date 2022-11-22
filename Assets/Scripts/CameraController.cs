using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool allowMovingCamera = true;
    public int moveCameraOffset = 30;
    public float moveSpeed = 0.01f;
    Camera camera;
    Canvas canvas;
    float w;
    float h;

    void Awake()
    {
        camera = this.GetComponent<Camera>();
        canvas = FindObjectOfType<Canvas>(); 
        h = canvas.GetComponent<RectTransform>().rect.height;
        w = canvas.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowMovingCamera){
            MoveCamera();
        }
    }



    public void MoveCamera(){

        // Move right
        if((w - Input.mousePosition.x) <= moveCameraOffset){
            camera.transform.position += new Vector3(moveSpeed,0,0);
        }

        // Move left
        if((w - Input.mousePosition.x) >= (w-moveCameraOffset)){
            camera.transform.position -= new Vector3(moveSpeed,0,0);
        }


        // Move top
        if((h - Input.mousePosition.y) <= moveCameraOffset){
            camera.transform.position += new Vector3(0,moveSpeed,0);
        }

        // Move bottom
        if((h - Input.mousePosition.y) >= (h-moveCameraOffset)){
            camera.transform.position -= new Vector3(0,moveSpeed,0);
        }

    }
}
