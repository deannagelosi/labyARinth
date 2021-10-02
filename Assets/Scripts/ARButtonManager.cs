using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using cs294_137.hw2;
using UnityEngine.XR.ARFoundation; //TO_ADD
using UnityEngine.XR.ARSubsystems;

public class ARButtonManager : MonoBehaviour
{
    private Camera arCamera;
    private PlaceLabyrinthBoard placeLabyrinthBoard;

    void Start()
    {
        // Here we will grab the AR camera 
        // This camera acts like any other camera in Unity.
        // arCamera = FindObjectOfType<ARCamera>().GetComponent<Camera>();
        arCamera = GetComponent<ARSessionOrigin>().camera; //TO_ADD
        // We will also need the PlaceLabyrinthBoard script to know if
        // the Labyrinth board exists or not.
        placeLabyrinthBoard = GetComponent<PlaceLabyrinthBoard>();
    }

    void Update()
    {
        // if (placeLabyrinthBoard.Placed() && Input.GetMouseButtonDown(0))
        if (placeLabyrinthBoard.Placed() && Input.touchCount > 0) //TO_ADD
        {
            // Vector2 touchPosition = Input.mousePosition;
            Vector2 touchPosition = Input.GetTouch(0).position; //TO_ADD
            // Convert the 2d screen point into a ray.
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            // Check if this hits an object within 100m of the user.
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit,100))
            // RaycastHit[] hits;
            // hits = Physics.RaycastAll(ray, 100.0F);
            // for (int i = 0; i < hits.Length; i++)
            // {
            //     // Check that the object is interactable.
            //     if(hits[i].transform.tag=="Interactable")
            //         // Call the OnTouch function.
            //         // Note the use of OnTouch3D here lets us
            //         // call any class inheriting from OnTouch3D.
            //         hits[i].transform.GetComponent<OnTouch3D>().OnTouch();
            // }
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check that the object is interactable.
                if (hit.transform.tag == "Interactable")
                    // Call the OnTouch function.
                    // Note the use of OnTouch3D here lets us
                    // call any class inheriting from OnTouch3D.
                    hit.transform.GetComponent<OnTouch3D>().OnTouch();
            }
        }
    }
}
