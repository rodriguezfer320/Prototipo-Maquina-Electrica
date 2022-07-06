using UnityEngine;
using UnityEngine.UI;
 
public class ARAngleSlider : MonoBehaviour
{
	private static ARAngleSlider instance;
    private Slider slider;
    private Text textComp;
    private GameObject arm;
    private Image fillSliderNums;

    private void Awake()
    {
        instance = this;
        slider = GameObject.Find("AngleSlider").GetComponent<Slider>();
        textComp = slider.transform.GetChild(3).GetComponent<Text>();
        fillSliderNums = GameObject.Find("fillSliderNums").GetComponent<Image>();
    }

    public static ARAngleSlider GetInstance()
    {   
        return instance;
    }

    public void SetArmRotation(GameObject _arm)
    {
        arm = _arm;
    }

    public void OnSliderValueChanged(){
        //0-2-4-8-15-30-45-90
        float val = GetDegrees(slider.value);
        UpdateText(val);
        UpdateArmRotation(val);
    }
    
    private float GetDegrees(float val){
        if(val > 0f && val <= 10f){
            fillSliderNums.fillAmount = 0.207f;
            slider.value = 10f;
            val = 2f;
        }else if(val > 10f && val <= 20f){
            fillSliderNums.fillAmount = 0.34f;
            slider.value = 20f;
            val = 4f;
        }else if(val > 20f && val <= 30f){
            fillSliderNums.fillAmount = 0.471f;
            slider.value = 30f;
            val = 8f;
        }else if(val > 30f && val <= 40f){
            fillSliderNums.fillAmount = 0.604f;
            slider.value = 40f;
            val = 15f;
        }else if(val > 40f && val <= 50f){
            fillSliderNums.fillAmount = 0.736f;
            slider.value = 50f;
            val = 30f;
        }else if(val > 50f && val <= 60f){
            fillSliderNums.fillAmount = 0.868f;
            slider.value = 60f;
            val = 45f;
        }else if(val > 60f && val <= 70f){
            fillSliderNums.fillAmount = 1f;
            slider.value = 70f;
            val = 90f;
        }else {
            fillSliderNums.fillAmount = 0f;
            slider.value = 0f;
            val = 0f;
        }
        
        return val;
    }

    private void UpdateText(float val)
    {
        textComp.text = val.ToString();
    }

    private void UpdateArmRotation(float val)
    {   
		arm.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, val - 180));
	}
    
}