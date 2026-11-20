using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPos;
    public static MouseInput instance;
    public Vector3 mouseN;

    void Start() 
    { 
        instance = this;
    }

    void FixedUpdate()
    {
        mouseN = new Vector3((Input.mousePosition.x / 960) - 1, (Input.mousePosition.y / 540) - 1, 0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue)) 
        {
            mouseInputPos = hit.point;
        }
    }
}
