using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlaceContentwithHandsandGaze : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    public ManoGestureTrigger interactionTrigger1;

    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;
    public Transform pointer;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {

        if (Input.touchCount != 1) return;

        var t = Input.GetTouch(0);

        if (t.phase != TouchPhase.Began) return;
        if (EventSystem.current.IsPointerOverGameObject(t.fingerId)) return;



        if (placementPoseIsValid && t.phase == TouchPhase.Began)
        {
            //ScreenSelect();
            PlaceObject();
        }

        HandClick();

        UpdatePlacementPose();
        UpdatePlacementIndicator();

        //if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    PlaceObject();
        //}
    }


    void HandClick()
    {

        //All the information of the hand
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

        //The click happens when I perform the Hand Gesture
        if (detectedHand.gesture_info.mano_gesture_trigger == interactionTrigger1)
        {
            //Logic that should happen when I click
            //ScreenSelect();
            PlaceObject();
        }

    }





    //public void ScreenSelect()
    //{
    //    List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
    //    var screenCenter = Camera.main.ScreenPointToRay(pointer.transform.position);
    //    raycastManager.Raycast(screenCenter, hitPoints, TrackableType.Planes);

    //    if (hitPoints.Count > 0)
    //    {
    //        Pose pose = hitPoints[0].pose;
    //        transform.rotation = pose.rotation;
    //        transform.position = pose.position;
    //    }

    //}

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        //var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var screenCenter = Camera.main.ScreenPointToRay(pointer.transform.position);
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
