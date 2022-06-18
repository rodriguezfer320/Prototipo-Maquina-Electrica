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
    private Text textAngle;

    public static RotateManager GetInstance(){
        return instance;
    }

    void Awake()
    {
        instance = this;  
        textAngle = GetComponent<Text>();
    }

    public void SetTextAngle(Text _textAngle){
        textAngle = _textAngle;
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
            textAngle.text = angle.ToString();
            ChangeColor(angle);
            angle = ((angle >= 180) ? (angle - 360) : angle);
        }   
    
    }  

    private void ChangeColor(float angle){
         _renderer = bobina.GetComponent<Renderer>();

        if(angle <= 180f && angle >= 150f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); 
            _renderer.material.color = Color.black;
        }else if(angle < 150f && angle >= 120f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
            _renderer.material.color = Color.yellow;
        }else if(angle < 120f && angle >= 60f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -120));
            _renderer.material.color = Color.blue;
        }else if(angle < 60f && angle >= 0f){
             handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            _renderer.material.color = Color.red;
        }else{
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); 
            _renderer.material.color = Color.green;
        }      
        
    }

}
