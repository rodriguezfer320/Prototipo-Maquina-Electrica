using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARCircleSlider : MonoBehaviour
{
    private static ARCircleSlider instance;

    private Renderer sheetRenderer;

    private Renderer topCoilRenderer;

    private Renderer sideRenderer;

    private Renderer armRenderer;

    private Image handle;

    private Image fill;

    private const float r = 0.7529412f;

    private const float g = 0.140016f;

    private const float b = 0.1304288f;

    private float alpha = 1f;

    private void Awake()
    {
        instance = this;
        handle = GameObject.Find("handle").GetComponent<Image>();
        fill = GameObject.Find("fill").GetComponent<Image>();
    }

    // Function that get the ARCircleSlider's class instance
    public static ARCircleSlider GetInstance()
    {
        return instance == null? instance = new ARCircleSlider() : instance;
    }

    // Function that set the sheetRenderer's machine renderer
    public void SetSheetRenderer(GameObject sheet)
    {
        sheetRenderer = sheet.GetComponent<Renderer>();
    }

    // Function that set the topCoil's machine renderer
    public void SetTopCoilRenderer(GameObject topCoil)
    {
        topCoilRenderer = topCoil.GetComponent<Renderer>();
    }

    // Function that set the side's machine renderer
    public void SetSideRenderer(GameObject side)
    {
        sideRenderer = side.GetComponent<Renderer>();
    }

    // Function that set the arm's machine renderer
    public void SetArmRenderer(GameObject arm)
    {
        armRenderer = arm.GetComponent<Renderer>();
    }

    // Function that listen the Handel's drag event set the armRotation's machine game object
    public void OnHandelDrag()
    {
        Vector3 fingerPos = Input.GetTouch(0).position;
        Vector2 dir = fingerPos - handle.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle >= 0f) ? angle : (360f + angle);

        if (angle <= 180f || angle >= 360f)
        {
            ChangeColorAndRotation (angle);
            ChangeAlphaMachine();
            AnimationController.GetInstance().RunAnimation();
        }
    }

    // Function that change the color and rotation of the knob button
    private void ChangeColorAndRotation(float angle)
    {
        alpha = 0.25f;
        if(angle <= 180f && angle >= 150f){
            alpha = 1f;
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); 
            topCoilRenderer.material.color = new Color(r, g, b, alpha);
            fill.fillAmount = 0f;
            AnimationController.GetInstance().SetVolt(0);
        }else if(angle < 150f && angle >= 120f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -60));
            topCoilRenderer.material.color = new Color(r + 0.08f, g - (0.04f * 2f), b - (0.04f * 2f), alpha);
            fill.fillAmount = 0.444f;
            AnimationController.GetInstance().SetVolt(5);
        }else if(angle < 120f && angle >= 60f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -120));
            topCoilRenderer.material.color = new Color(r + (0.08f * 2f), g - (0.04f * 3f), b - (0.04f * 3f), alpha);
            fill.fillAmount = 0.62f;
            AnimationController.GetInstance().SetVolt(15);
        }else if(angle < 60f && angle >= 0f){
            handle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            topCoilRenderer.material.color = new Color(1f, 0f, 0f, alpha);
            fill.fillAmount = 1f;
            AnimationController.GetInstance().SetVolt(30);
        }      
    }

    // Function that change the trasparency of the machine.
    private void ChangeAlphaMachine()
    {
        sheetRenderer.material.color = GetRendererMaterialColor(sheetRenderer);
        topCoilRenderer.material.color =
            GetRendererMaterialColor(topCoilRenderer);
        sideRenderer.material.color = GetRendererMaterialColor(sideRenderer);
        armRenderer.material.color = GetRendererMaterialColor(armRenderer);
    }

    // Function that get the material of each game object
    private Color GetRendererMaterialColor(Renderer renderer)
    {   
        Color color = renderer.material.color;
        color.a = alpha;
        return color;
    }
}
