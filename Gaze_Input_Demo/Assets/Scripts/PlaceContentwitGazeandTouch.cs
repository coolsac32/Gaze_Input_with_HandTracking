using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceContentwitGazeandTouch : MonoBehaviour
{


    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;
    public Transform pointer;


    void Update()
    {
        if (Input.touchCount != 1) return;

        var t = Input.GetTouch(0);

        if (t.phase != TouchPhase.Began) return;
        if (EventSystem.current.IsPointerOverGameObject(t.fingerId)) return;

        if (t.phase == TouchPhase.Began)
        {
            ScreenSelect();
        }


    }



    public void ScreenSelect()
    {
        List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
        var screenCenter = Camera.main.ScreenPointToRay(pointer.transform.position);
        raycastManager.Raycast(screenCenter, hitPoints, TrackableType.Planes);



        if (hitPoints.Count > 0)
        {
            Pose pose = hitPoints[0].pose;
            transform.rotation = pose.rotation;
            transform.position = pose.position;
        }
    }

}
