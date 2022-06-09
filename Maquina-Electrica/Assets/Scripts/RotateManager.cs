using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateManager : MonoBehaviour
{
    private Image handle;
    private Vector3 fingerPos;

    void Start()
    {
        handle = GameObject.Find("handle").GetComponent<Image>();
    }

    public void SetZaxis()
    {
        fingerPos = Input.GetTouch(0).position;
        Vector2 dir = fingerPos - handle.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        
        if(angle <= 180 || angle >= 360){
            Quaternion r = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
            handle.transform.rotation = r;
            angle = (angle >= 360) ? (angle - 360) : angle;
        }
              
    }   
}
