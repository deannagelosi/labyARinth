using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public GameObject loseTextObject;
    public GameObject winTextObject;

    private Vector3 startPosition;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int lastScore;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        setPosition();
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);

        // Set the initial score to zero
        lastScore = 0;
    }

    public void setPosition()
    {
        startPosition = transform.position;
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }

    public void resetPlayer() 
    {
        transform.position = startPosition;
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Loser")) 
        {
            loseTextObject.SetActive(true);
            SetScoreText();
        }

        else if (other.gameObject.CompareTag("Winner")) 
        {
            winTextObject.SetActive(true);
            lastScore = 18; // Score for reaching the end
            SetScoreText();
        }

        else if (other.gameObject.CompareTag("Detector"))
        {
            Detector detectorController = other.gameObject.GetComponent<Detector>();
            lastScore = detectorController.getScore();
        }
   
        // if the ball enters one of the holes, it's encountering a collider
        // maybe each side of the hole has its own tag?
        // when the ball goes through a hole, it changes its score from nothing to the value
    }

    void SetScoreText()
    {
        print("Score: " + lastScore.ToString());
        // something here about only changing the score when the ball goes through the hole or reaches the end
        // does not display otherwise
        // score.Text = "Score: " + score.ToString();

    }
}
