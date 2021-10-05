using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

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
            pickUpParent.SetActive(true);
        }
    }
}

