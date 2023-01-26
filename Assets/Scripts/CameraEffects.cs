using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Camera gameCamera;

    [SerializeField]
    float zoomInSize, zoomOutSize;

    public void ZoomIn()
    {
        gameCamera.orthographicSize -= zoomInSize;
    }

    public void ZoomOut()
    {
        gameCamera.orthographicSize += zoomOutSize;
    }


}
