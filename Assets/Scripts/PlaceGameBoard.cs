using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This allows us to user the AR Foundation simulator functions
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems;

public class PlaceGameBoard : MonoBehaviour
{
    // Public variables can be set from the unity UI.
    // We will set this to our Labyrinth Board object.
    public GameObject gameBoard;
    // These will store references to our other components.
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private PlayerController playerController;
    // This will indicate whether the labyrinth board is set.
    private bool placed = false;

    // Start is called before the first frame update.
    void Start()
    {
        // GetComponent allows us to reference other parts of this game object.
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        // Find the player object
        GameObject playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        playerController.setPosition(); // Save players initial position
        playerController.deactivateText(); // Hide win/lose text;

        // start with game turned off after finding Player
        gameBoard.SetActive(false);

        // Turn on horizontal plane detection
        planeManager.detectionMode = PlaneDetectionMode.Horizontal;
    }

    // Update is called once per frame.
    void Update()
    {
        if (!placed)
        {
            // if (Input.GetMouseButtonDown(0))
            if (Input.touchCount > 0)
            {
                // Vector2 touchPosition = Input.mousePosition;
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Raycast will return a list of all planes intersected by the
                // ray as well as the intersection point.
                List<ARRaycastHit> hits = new List<ARRaycastHit>();

                if (raycastManager.Raycast(
                    touchPosition, hits, TrackableType.PlaneWithinPolygon)) 
                {
                    var hitPosition = hits[0].pose.position; 

                    // Activate labyrinth board and place at chosen location
                    gameBoard.SetActive(true);
                    playerController.resetPlayer(); // player in starting position
                    gameBoard.transform.position = hitPosition;
                    playerController.setPosition(); // get new starting position
                    placed = true;

                    // After placed, disable plane detection and hide found planes
                    planeManager.detectionMode = PlaneDetectionMode.None; // pause detection
                    // planeManager.enabled = !planeManager.enabled;
                    SetAllPlanesActive(false);
                }
            }
        }
        else // board is already placed
        {
            // make sure plane detection is off
            planeManager.detectionMode = PlaneDetectionMode.None;
            SetAllPlanesActive(false);
        }
    }

    // Allow the player to move the board to a new location
    public void AllowMoveGameBoard()
    {
        // Reset player and Disable board
        playerController.resetPlayer(); // back to starting position
        gameBoard.SetActive(false);

        // Turn plane detection back on and show old planes
        planeManager.detectionMode = PlaneDetectionMode.Horizontal;
        SetAllPlanesActive(true);
        
        // Turn touch detection back on
        placed = false;
    }

    private void SetAllPlanesActive(bool value)
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

    // Lastly we will later need to allow other components to check whether the
    // labyrinth board has been places so we will add an accessor to this.
    public bool Placed()
    {
        return placed;
    }
}

