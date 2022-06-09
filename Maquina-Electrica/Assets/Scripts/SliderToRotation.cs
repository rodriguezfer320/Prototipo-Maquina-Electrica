using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToRotation : MonoBehaviour
{
	private Slider slider;
	
	public float xLimit = 45f;
	public float yLimit = 45f;
	public float zLimit = 45f;
	
	public bool xRot;
	public bool yRot;
	public bool zRot;
	
    // Start is called before the first frame update
    void Start()
    {
		slider = GameObject.Find("SliderAngles").GetComponent<Slider>();
		
        slider.onValueChanged.AddListener(delegate{
			RotateMe();
		});
    }
	
	public void RotateMe(){
		if(xRot)
			transform.localEulerAngles = new Vector3(slider.value * xLimit, transform.localEulerAngles.y, transform.localEulerAngles.z);
		if(yRot)
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, slider.value * yLimit, transform.localEulerAngles.z);
		if(zRot)
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, slider.value * zLimit);
	}
}