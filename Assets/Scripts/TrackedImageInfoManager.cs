using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
// and overlays a cube on each of the tracked image
// Note that this code assumes that all tracked images are unique and named differently

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    
    public GameObject pickUpParent;

    ARTrackedImageManager m_TrackedImageManager;
    Dictionary<string,GameObject> gameobjectDict = new Dictionary<string,GameObject>();

    void Awake()
    {
        //This gets a reference to the AR Tracked Image Manager attached to the 'AR session Origin' gameobject
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }


    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        //eventArgs.added contains all the newly trackedImages that were found this frame
        foreach (var trackedImage in eventArgs.added)
        {
            //Write code here to deploy stuff whenever a new image is found in the tracking
            //e.g. Create a new virtual object and/or attach it to the tracked image
            //trackedImage.referenceImage.name -> Name of the tracked image
            //trackedImage.transform.position -> Position of the tracked image in the real world 
            //trackedImage.transform.rotation -> Rotation of the tracked image in the real world 
            pickUpParent.SetActive(true);
            
        }

        //eventArgs.removed contains all the trackedImages that were not found by the AR camera, either because it was removed from the camera's views or because the camera could not detect it.
            foreach (var trackedImage in eventArgs.removed)
        {
            //If we loose tracking of the image
            pickUpParent.SetActive(false);

        }
    }
}

