using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateManager : MonoBehaviour
{
    private Image handle;
    private Vector3 fingerPos;
    private static RotateManager instance;
    private GameObject bobina;
    private Renderer _renderer;
    private float r;
    private float g;
    private float b;
    private float val;
    private float val_r;

    public static RotateManager GetInstance(){
        return instance;
    }

    void Awake()
    {
        instance = this;  
    }

    public void SetBobina(GameObject _bobina){
        bobina = _bobina;
    }

    public void SetHandle(Image _handle){
        handle = _handle;
    }

    public void SetZaxis()
    {
        fingerPos = Input.GetTouch(0).position;
        Vector2 dir = fingerPos - handle.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        angle = (angle >= 0) ? angle : (360 + angle);

        if(angle <= 180 || angle >= 360){
            ChangeColor(angle);
            angle = ((angle >= 180) ? (angle - 360) : angle);
        }   
    
    }  

    private void ChangeColor(float angle){
         _renderer = bobina.GetComponent<Renderer>();

        r = 0.7529412f;
        g = 0.140016f;
        b = 0.1304288f;
        val = 0.04f;
        val_r = 0.08f;

        if(angle <= 180f && angle >= 150f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); 
            _renderer.material.color = new Color(r, g, b, 1f);
        }else if(angle < 150f && angle >= 120f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
            _renderer.material.color = new Color(r+(val_r), g-(val*2), b-(val*2), 1f);
        }else if(angle < 120f && angle >= 60f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -120));
            _renderer.material.color = new Color(r+(val_r*2), g-(val*3), b-(val*3), 1f);
        }else if(angle < 60f && angle >= 0f){
             handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            _renderer.material.color = new Color(1, 0, 0, 1f);
        }else{
            
        }      
        
    }

}
