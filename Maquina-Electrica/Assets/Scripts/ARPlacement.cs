using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawnMachine;
    public GameObject placementIndicator;
    public Camera aRCamera;
    private GameObject machine;
    private GameObject topCoil;
    private GameObject arm;
    private Pose placementPose;
    private ARSessionOrigin sessionOrigin;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits;
    private bool placementPoseIsValid = false;

    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (machine == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }        
    }

    void UpdatePlacementPose()
    {
        var screenCenter = aRCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        if (machine == null && placementPoseIsValid)
        {
            var cameraForward = aRCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void ARPlaceObject()
    {
        machine = Instantiate(arObjectToSpawnMachine, placementPose.position, placementPose.rotation);
        topCoil = machine.transform.GetChild(0).GetChild(2).gameObject;
        arm = machine.transform.GetChild(1).gameObject;

        ARCircleSlider.GetInstance().SetTopCoilRenderer(topCoil);
        ARAngleSlider.GetInstance().SetArm(arm);
    }
    
}