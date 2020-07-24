using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    List<InfoBehavior> infos = new List<InfoBehavior>();

    public Transform pointer;

    // Start is called before the first frame update
    void Start()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

        var ray = Camera.main.ScreenPointToRay(new Vector2(0.5f, 0.5f)); //replace input.mouseposition
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //replace input.mouseposition
        //Ray ray = new Ray(pointer.position, pointer.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            //var selection = hit.transform;
            //var selectionRenderer = selection.GetComponent<Renderer>();
            //if (selectionRenderer != null)
            //{

            //}
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                OpenInfo(go.GetComponent<InfoBehavior>());
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(InfoBehavior desiredInfo)
    {
        foreach (InfoBehavior info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.ClosedInfo();
            }
        }
    }

    void CloseAll()
    {
        foreach (InfoBehavior info in infos)
        {
            info.ClosedInfo();
        }
    }
}
