using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastObjects))]
public class RaycastHandler : MonoBehaviour
{
    public Color defaultColor = Color.white;
    public Color overColor = Color.cyan;
    public Color downColor = Color.yellow;
    public Color clickedColor = Color.magenta;

    private RaycastObjects raycastObject;
    private Renderer objectRenderer;

    private void Awake()
    {
        raycastObject = gameObject.GetComponent<RaycastObjects>();
        objectRenderer = gameObject.GetComponent<Renderer>();
        objectRenderer.material.color = defaultColor;
    }

    private void OnEnable()
    {
        if (raycastObject)
        {
            raycastObject.OnPointerOff.AddListener(Off);
            raycastObject.OnPointerOver.AddListener(Over);
            raycastObject.OnPointerUp.AddListener(Clicked);
            raycastObject.OnPointerDown.AddListener(Down);
        }
    }

    private void OnDisable()
    {
        if (raycastObject)
        {
            raycastObject.OnPointerOff.RemoveListener(Off);
            raycastObject.OnPointerOver.RemoveListener(Over);
            raycastObject.OnPointerUp.RemoveListener(Clicked);
            raycastObject.OnPointerDown.RemoveListener(Down);
        }
    }

    private void Off()
    {
        objectRenderer.material.color = defaultColor;
    }

    private void Over()
    {
        objectRenderer.material.color = overColor;
    }

    private void Down()
    {
        objectRenderer.material.color = downColor;
    }

    private void Clicked()
    {
        objectRenderer.material.color = clickedColor;
    }
}
