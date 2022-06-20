using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARCircleSlider : MonoBehaviour
{
    private static ARCircleSlider instance;
    private Renderer topCoilRenderer;
    private Image handle;
    private Image fill;
    private const float r = 0.7529412f;
    private const float g = 0.140016f;
    private const float b = 0.1304288f;

    private void Awake()
    {
        instance = this;
        handle = GameObject.Find("handle").GetComponent<Image>();  
        fill = GameObject.Find("fill").GetComponent<Image>();  
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
        angle = (angle >= 0f) ? angle : (360f + angle);

        if(angle <= 180f || angle >= 360f){
            ChangeColorAndRotation(angle);
        }   
    }  

    private void ChangeColorAndRotation(float angle){
        if(angle <= 180f && angle >= 150f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); 
            topCoilRenderer.material.color = new Color(r, g, b, 1f);
            fill.fillAmount = 0f;
        }else if(angle < 150f && angle >= 120f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
            topCoilRenderer.material.color = new Color(r + 0.08f, g - (0.04f * 2f), b - (0.04f * 2f), 1f);
            fill.fillAmount = 0.444f;
        }else if(angle < 120f && angle >= 60f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -120));
            topCoilRenderer.material.color = new Color(r + (0.08f * 2f), g - (0.04f * 3f), b - (0.04f * 3f), 1f);
            fill.fillAmount = 0.62f;
        }else if(angle < 60f && angle >= 0f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            topCoilRenderer.material.color = new Color(1f, 0f, 0f, 1f);
            fill.fillAmount = 1f;
        }      
    }

}
