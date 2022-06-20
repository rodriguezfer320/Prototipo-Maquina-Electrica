using UnityEngine;
using UnityEngine.UI;
 
public class ARAngleSlider : MonoBehaviour
{
	private static ARAngleSlider instance;
    private Slider slider;
    private Text textComp;
    private GameObject arm;

    private void Awake()
    {
        instance = this;
        slider = GameObject.Find("AngleSlider").GetComponent<Slider>();
        textComp = slider.transform.GetChild(3).GetComponent<Text>();
    }

    public static ARAngleSlider GetInstance()
    {   
        return instance;
    }

    public void SetArm(GameObject _arm)
    {
        arm = _arm;
    }

    public void OnSliderValueChanged(){
        //0-2-4-8-15-30-45-90
        float val = slider.value;
        UpdateText(val);
        UpdateArmRotation(val);
    }
 
    private void UpdateText(float val)
    {
        textComp.text = val.ToString();
    }

    private void UpdateArmRotation(float val)
    {
		arm.transform.rotation = Quaternion.Euler(new Vector3(arm.transform.rotation.x, arm.transform.rotation.y, arm.transform.rotation.z + val));
	}
    
}