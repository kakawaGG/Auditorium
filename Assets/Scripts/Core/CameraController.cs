using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public Vector3 modelInitialRotation;
    public Transform model;

    public void Set3DVision()
    {
        camera.orthographic = false;
        model.eulerAngles = modelInitialRotation;
    }

    public void Set2DVidion()
    {
        camera.orthographic = true;
        model.eulerAngles = modelInitialRotation;
    }
}