using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public GameObject loseTextObject;
    public GameObject winTextObject;
    public GameObject scoreTextObject;
    public GameObject bonusScoreTextObject;
    public GameObject[] deactivatedPickUps; 

    private Vector3 startPosition;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int lastScore;
    private int bonusCount;  
    
    // Start is called before the first frame update
    void Start()
    {
        print("player controller start");

        rb = GetComponent<Rigidbody>();
        setPosition();
        
        deactivateText();

        // Set the initial score to zero
        lastScore = 0;
        bonusCount = 0;
    }

    public void deactivateText() 
    {
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);
        scoreTextObject.SetActive(false);
        bonusScoreTextObject.SetActive(false);
    }

    public void setPosition()
    {
        startPosition = transform.position;
    }

    // void OnMove(InputValue movementValue) 
    // {
    //     Vector2 movementVector = movementValue.Get<Vector2>();

    //     movementX = movementVector.x;
    //     movementY = movementVector.y;
    // }

    void FixedUpdate() 
    {
        // Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        // rb.AddForce(movement * speed);

        Vector3 acc = Input.acceleration;
        rb.AddForce(acc.x * speed, 0, acc.y * speed);
    }

    public void resetPlayer() 
    {
        transform.position = startPosition;
        deactivateText();
        // add resetting collectibles 
        foreach (GameObject PickUp in deactivatedPickUps)
        {
            PickUp.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Loser")) 
        {
            loseTextObject.SetActive(true);
            SetPointText(scoreTextObject, "Score: " + lastScore.ToString());
            bonusCount = 0;
        }

        else if (other.gameObject.CompareTag("Winner")) 
        {
            winTextObject.SetActive(true);
            lastScore = 18; // Score for reaching the end
            SetPointText(scoreTextObject, "Score: " + lastScore.ToString());
            bonusCount = 0;
        }

        else if (other.gameObject.CompareTag("Detector"))
        {
            Detector detectorController = other.gameObject.GetComponent<Detector>();
            lastScore = detectorController.getScore();
        }

        else if (other.gameObject.CompareTag("PickUp"))
        {
            print("touched the pickup");
            other.gameObject.SetActive(false);
            bonusCount += 1;
            SetPointText(bonusScoreTextObject, "Bonus Score: " + bonusCount.ToString());
        }
   
        // if the ball enters one of the holes, it's encountering a collider
        // maybe each side of the hole has its own tag?
        // when the ball goes through a hole, it changes its score from nothing to the value
    }

    void SetPointText(GameObject pointTextObject, string text)
    {
        pointTextObject.GetComponent<TextMeshProUGUI>().text = text;
        pointTextObject.SetActive(true);
    }
}
