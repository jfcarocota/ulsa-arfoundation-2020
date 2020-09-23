using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTracking : MonoBehaviour
{
    [SerializeField]
    GameObject srcObject;

    GameObject spawnedObj;
    ARRaycastManager aRRaycastManager;
    Vector2 touchPos;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        if(IsTouching(out Vector2 touchPos))
        {
            if(aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;

                if(!spawnedObj)
                {
                    spawnedObj = Instantiate(srcObject, pose.position, pose.rotation);
                }
                else
                {
                    spawnedObj.transform.position = pose.position;
                    spawnedObj.transform.rotation = pose.rotation;
                }
            }
        }    
    }


    bool IsTouching(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }
}
