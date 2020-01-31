using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraSizeHandler : MonoBehaviour
{
    public Camera cam;
    public float height, width;
    public bool keepBottomPos = true;
    public Transform thisTransform;
    public float defaultScreenSize = 15.4f;
    public float targetAspect = 1.77778f;
    private float posDiff;
    private Vector3 targetPos;

    // Use this for initialization
    void Start()
    {
        defaultScreenSize = cam.orthographicSize;
        thisTransform = this.transform;
        UpdateCameraSize();
    }

    public void UpdateCameraSize()
    {
        float size = cam.aspect;
        Debug.Log("Size---->" + size);
        if (size < targetAspect)
            cam.orthographicSize = defaultScreenSize;
        else
            cam.orthographicSize = ( height / (Screen.width * Screen.height) ) / 2;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        posDiff = width / 2 - cam.orthographicSize;
        targetPos = thisTransform.position - Vector3.up * posDiff;
        if (keepBottomPos) thisTransform.position = targetPos;
    }
}