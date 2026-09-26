using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPos;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue)) 
        {
            mouseInputPos = hit.point;
        }
    }
}
