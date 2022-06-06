using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawnMaquina;
    public GameObject arObjectToSpawnBrazo;
    public GameObject placementIndicator;
    private GameObject maquina;
    private GameObject brazo;
    private Pose PlacementPoseMaquina;
    private Pose PlacementPoseBrazo;
    private ARRaycastManager aRRaycastManager;
    private bool PlacementPoseMaquinaIsValid = false;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
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
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.All);

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
    }
}