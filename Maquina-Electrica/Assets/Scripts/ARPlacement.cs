using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawnMaquina;
    public GameObject arObjectToSpawnBrazo;
    public GameObject placementIndicator;
    public Camera aRCamera;

    private GameObject maquina;
    private GameObject brazo;
    private Pose PlacementPoseMaquina;
    private Pose PlacementPoseBrazo;
    private ARSessionOrigin sessionOrigin;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits;
    private bool PlacementPoseMaquinaIsValid = false;
    private Image handle;

    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        handle = GameObject.Find("handle").GetComponent<Image>();
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (maquina == null && PlacementPoseMaquinaIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }        
    }

    void UpdatePlacementIndicator()
    {
        if (maquina == null && PlacementPoseMaquinaIsValid)
        {
            var cameraForward = aRCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 8, cameraForward.z).normalized;
            PlacementPoseMaquina.rotation=Quaternion.LookRotation(cameraBearing);
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPoseMaquina.position, PlacementPoseMaquina.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        PlacementPoseMaquinaIsValid = hits.Count > 0;
        if (PlacementPoseMaquinaIsValid)
        {
            PlacementPoseMaquina = hits[0].pose;
            PlacementPoseBrazo = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        PlacementPoseMaquina.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        maquina = Instantiate(arObjectToSpawnMaquina, PlacementPoseMaquina.position, PlacementPoseMaquina.rotation);
        
        PlacementPoseBrazo.position[0] += 0.08f;
        PlacementPoseBrazo.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        brazo = Instantiate(arObjectToSpawnBrazo, PlacementPoseBrazo.position, PlacementPoseBrazo.rotation);

        RotateManager.GetInstance().SetHandle(handle);
        RotateManager.GetInstance().SetBobina(maquina.transform.GetChild(2).gameObject);
    }
    
}