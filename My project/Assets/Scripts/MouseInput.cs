using System;
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

    public GameObject moveMarker;
    AnimatorStateInfo animState;
    public float time;

    void Start() 
    { 
        instance = this;
    }

    void FixedUpdate()
    {
        mouseN = new Vector3((Input.mousePosition.x / 960) - 1, (Input.mousePosition.y / 540) - 1, 0);
        if (!is_playing()) moveMarker.SetActive(false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue)) 
        {
            mouseInputPos = hit.point;
        }
    }

    private bool is_playing()
    {
        animState = moveMarker.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0);
        time = animState.normalizedTime;
        if (time > 1.0f) { time = 0f; return false; }
        else if (time < 1.0f && time > 0f) return true;
        return false;
    }
}
