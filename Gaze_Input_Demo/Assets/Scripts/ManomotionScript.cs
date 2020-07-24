using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManomotionScript : MonoBehaviour
{
    [SerializeField]
    //private PlacementObject[] placedObjects;

    public GameObject cursor;

    RectTransform cursorRectTransform;
    public ManoGestureTrigger interactionTrigger;
    public ManoClass movingManoclass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Handles the information from the processed frame in order to use the gesture information and tracking information in moving the cursor and firing at the blocks.
    /// </summary>
    void HandleManoMotionFrameProcessed()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        TrackingInfo trackingInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

        MoveCursorAt(gesture, trackingInfo, warning);
        InteractTrigger(gesture);
    }

    /// <summary>
    /// Moves the cursor according to the gesture information in the center of the detected bounding box.
    /// The cursor will disapear if there is no hand detected -> Warning Hand not found.
    /// </summary>
    /// <param name="gestureInfo">Gesture info.</param>
    /// <param name="trackingInfo">Tracking info.</param>
    /// <param name="warning">Warning.</param>
    void MoveCursorAt(GestureInfo gestureInfo, TrackingInfo trackingInfo, Warning warning)
    {
        if (warning != Warning.WARNING_HAND_NOT_FOUND && gestureInfo.mano_class == movingManoclass)
        {
            if (!cursor.activeInHierarchy)
            {
                cursor.SetActive(true);
            }
            cursorRectTransform.position = Camera.main.ViewportToScreenPoint(trackingInfo.palm_center);
        }
        else
        {
            if (cursor.activeInHierarchy)
            {
                cursor.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Fires a raycast from the position of the cursor forward seeking to hit an example block.
    /// The fire will only happen with the user performes the interaction trigger.
    /// </summary>
    /// <param name="gestureInfo">Gesture info.</param>
    /// <param name="trackingInfo">Tracking info.</param>
    void InteractTrigger(GestureInfo gestureInfo)
    {
        if (gestureInfo.mano_gesture_trigger == interactionTrigger)
        {


            Ray ray = Camera.main.ScreenPointToRay(cursorRectTransform.position);
            RaycastHit hit;

            Debug.Log("Ray is fired");

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.Log("Ray has hit: " + hit.transform.name);

                //if (hit.transform.tag == interactableTag)
                //{
                //    hit.transform.GetComponent<CubeSpawn>().AwardPoints();
                //    Handheld.Vibrate();
                //}
            }

            //if (Physics.Raycast(ray, out hit))
            //{
            //    PlacementObject placementObject = hit.transform.GetComponent<PlacementObject>();
            //    if (placementObject != null)
            //    {
            //        ChangeSelectedObject(placementObject);
            //    }
            //}
            //else
            //{
            //    ChangeSelectedObject();
            //}
        }
    }
}
