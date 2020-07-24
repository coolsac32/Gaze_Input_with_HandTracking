using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceContent : MonoBehaviour
{
    public ManoGestureTrigger interactionTrigger1;
    public ManoGestureTrigger interactionTrigger2;
    public ManoGestureTrigger interactionTrigger3;
    public ManoGestureTrigger interactionTrigger4;
    public ManoGestureTrigger interactionTrigger5;
    public ManoClass movingManoclass;

    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;
    public Transform pointer;
    public Camera arCam;




    void Update()
    {
        if (Input.touchCount != 1) return;

        var t = Input.GetTouch(0);

        if (t.phase != TouchPhase.Began) return;
        if (EventSystem.current.IsPointerOverGameObject(t.fingerId)) return;

        DetectHandGestureClick();
        HandleManoMotionFrameProcessed();

        if (t.phase == TouchPhase.Began)
        {
            ScreenClick();
        }


    }

        /// <summary>
        /// Handles the information from the processed frame in order to use the gesture information and tracking information in moving the cursor and firing at the blocks.
        /// </summary>
        void HandleManoMotionFrameProcessed()
        {
            GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
            TrackingInfo trackingInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
            Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

            //MoveCursorAt(gesture, trackingInfo, warning);
            FireAt(gesture);
        }

        void DetectHandGestureClick()
        {

            //All the information of the hand
            HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

            //The click happens when I perform the Click Gesture : Open Pinch -> Closed Pinch -> Open Pinch 
         if (detectedHand.gesture_info.mano_gesture_trigger == interactionTrigger1)
         {
                //Logic that should happen when I click
                ScreenClick();

         }
       
         if (detectedHand.gesture_info.mano_gesture_trigger == interactionTrigger2)
         {
            //Logic that should happen when I click
            ScreenClick();

         }
       
         if (detectedHand.gesture_info.mano_gesture_trigger == interactionTrigger3)
         {
            //Logic that should happen when I click
            ScreenClick();

         }
  
        
         if (detectedHand.gesture_info.mano_gesture_trigger == interactionTrigger4)
         {
            //Logic that should happen when I click
            ScreenClick();

         }
    
         if (detectedHand.gesture_info.mano_gesture_trigger == interactionTrigger5)
         {
            //Logic that should happen when I click
            ScreenClick();

         }

    }

        void FireAt(GestureInfo gestureInfo)
        {
            if (gestureInfo.mano_gesture_trigger == interactionTrigger1)
            {
                ScreenClick();
            }
        }



        public void ScreenClick()
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

        bool IsClickOverUI()
        {

            //dont place content if pointer is over ui element
            PointerEventData data = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(data, results);
            return results.Count > 0;
        }
}
