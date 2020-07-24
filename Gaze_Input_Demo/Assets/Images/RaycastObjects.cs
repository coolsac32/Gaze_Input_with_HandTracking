using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RaycastObjects : MonoBehaviour
{
    public UnityEvent OnPointerDown;
    public UnityEvent OnPointerUp;
    public UnityEvent OnPointerOver;
    public UnityEvent OnPointerOff;

    public void PointerDown()
    {
        OnPointerDown.Invoke();
    }

    public void PointerUp()
    {
        OnPointerUp.Invoke();
    }

    public void PointerOver()
    {
        OnPointerOver.Invoke();
    }

    public void PointerOff()
    {
        OnPointerOff.Invoke();
    }
}
