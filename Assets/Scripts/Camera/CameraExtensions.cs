using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class CameraExtensions
{
    public static Bounds OrthographicBounds(this Camera camera, bool is2D = true)
    {
        float screenAspect = camera.aspect;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new(
            camera.transform.position,
            is2D ? new Vector3(cameraHeight * screenAspect, cameraHeight, 0f) : new Vector3(cameraHeight * screenAspect, 0f, cameraHeight));
        return bounds;
    }
}
