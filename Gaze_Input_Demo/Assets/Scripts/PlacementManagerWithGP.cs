using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class PlacementManagerWithGP : MonoBehaviour
{

    public ARRaycastManager raycastManager;
    public GameObject planePointer;
    public Transform pointer;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        planePointer = this.transform.GetChild(0).gameObject;
        planePointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
        //raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitPoints, TrackableType.Planes);

        var screenCenter = Camera.main.ScreenPointToRay(pointer.transform.position);
        raycastManager.Raycast(screenCenter, hitPoints, TrackableType.Planes);

        if (hitPoints.Count > 0)
        {
            transform.position = hitPoints[0].pose.position;
            transform.rotation = hitPoints[0].pose.rotation;
            if (!planePointer.activeInHierarchy)
            {
                planePointer.SetActive(true);
            }
        }

        if (hitPoints.Count < 0)
        {
            if (planePointer.activeInHierarchy)
            {
                planePointer.SetActive(false);
            }
         
        }
       
    }
}
