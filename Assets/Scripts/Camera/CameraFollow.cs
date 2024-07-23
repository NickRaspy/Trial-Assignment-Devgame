using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Header("Limitations")]
    [SerializeField] private bool isLimited;
    [SerializeField] private Collider limitBoundsObject;
    private Bounds cameraBounds;
    private Bounds limitBounds;
    private float aspectRatio;
    private float AspectRatio
    {
        set 
        { 
            if (aspectRatio == value) return;
            aspectRatio = value;
            LimitBorder();
        }
    }
    void Start()
    {
        if(isLimited)LimitBorder();
    }
    void Update()
    {
        if (!GameManager.instance.IsGameGoing) return;
        if (isLimited) AspectRatio = Camera.main.aspect;
        Vector3 targetPosition = new(
            isLimited ? Mathf.Clamp(target.position.x, limitBounds.min.x, limitBounds.max.x) : target.position.x, 
            transform.position.y,
            isLimited ? Mathf.Clamp(target.position.z, limitBounds.min.z, limitBounds.max.z) : target.position.z
            );
        transform.position = targetPosition;
    }
    void LimitBorder()
    {
        cameraBounds = CameraExtensions.OrthographicBounds(Camera.main, false);
        Bounds objectBounds = limitBoundsObject.bounds;
        if (objectBounds.extents.x - cameraBounds.extents.x < 0f || objectBounds.extents.z - cameraBounds.extents.z < 0f)
            Debug.LogError("Object is smaller then camera bounds!");
        limitBounds = new Bounds()
        {
            center = Vector3.up * 5f,
            extents = new Vector3(objectBounds.extents.x - cameraBounds.extents.x, 0f, objectBounds.extents.z - cameraBounds.extents.z)
        };
    }
    private void OnDrawGizmos()
    {
        Bounds cBounds = CameraExtensions.OrthographicBounds(Camera.main, false);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(cBounds.center, cBounds.size);
    }
}
