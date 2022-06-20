using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARCircleSlider : MonoBehaviour
{
    private static ARCircleSlider instance;
    private Renderer topCoilRenderer;
    private Image handle;
    private const float r = 0.7529412f;
    private const float g = 0.140016f;
    private const float b = 0.1304288f;
    private const float val = 0.04f;
    private const float val_r = 0.08f;

    private void Awake()
    {
        instance = this;
        handle = GameObject.Find("handle").GetComponent<Image>();  
    }

    public static ARCircleSlider GetInstance(){
        return instance;
    }

    public void SetTopCoilRenderer(GameObject topCoil){
        topCoilRenderer = topCoil.GetComponent<Renderer>();
    }

    public void OnHandleDrag()
    {
        Vector3 fingerPos = Input.GetTouch(0).position;
        Vector2 dir = fingerPos - handle.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        angle = (angle >= 0) ? angle : (360 + angle);

        if(angle <= 180 || angle >= 360){
            ChangeColorAndRotation(angle);
            angle = ((angle >= 180) ? (angle - 360) : angle);
        }   
    }  

    private void ChangeColorAndRotation(float angle){
        if(angle <= 180f && angle >= 150f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); 
            topCoilRenderer.material.color = new Color(r, g, b, 1f);
        }else if(angle < 150f && angle >= 120f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
            topCoilRenderer.material.color = new Color(r + val_r, g - (val * 2), b - (val * 2), 1f);
        }else if(angle < 120f && angle >= 60f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -120));
            topCoilRenderer.material.color = new Color(r + (val_r * 2), g - (val * 3), b - (val * 3), 1f);
        }else if(angle < 60f && angle >= 0f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            topCoilRenderer.material.color = new Color(1, 0, 0, 1f);
        }      
    }

}
