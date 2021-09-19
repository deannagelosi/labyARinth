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
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        setPosition();
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);
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
        }

        else if (other.gameObject.CompareTag("Winner")) 
        {
            winTextObject.SetActive(true);
        }
    }
}
