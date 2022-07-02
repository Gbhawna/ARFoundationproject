using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    //public GameObject modeltobespawned;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }
    private void UpdatePlacementPose()
    { 
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }

    }

}
