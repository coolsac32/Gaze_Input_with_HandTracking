using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReticleInput : MonoBehaviour
{
    [HeaderAttribute("Reticle")]
    public GameObject prefab;

    public bool timedClick = true;
    public bool showDebugRay = true;
    public float debugRayLength = 5f;
    public float maxRaycastLength = 25f;
    public float reticleDistance = 1f;
    public float timeToClick = 0.8f;
    public LayerMask excludeLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
