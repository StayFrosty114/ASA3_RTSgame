using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CamController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.2f;

    Rigidbody rigBody;
    
    void Start()
    {
        // Capture a reference to the camera rigidbody.
        rigBody = GetComponent<Rigidbody>();
    }

    // Camera Movement
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            rigBody.MovePosition(transform.position += (transform.up * moveSpeed));

        if (Input.GetKey(KeyCode.A))
            rigBody.MovePosition(transform.position -= (transform.right * moveSpeed));

        if (Input.GetKey(KeyCode.S))
            rigBody.MovePosition(transform.position -= (transform.up * moveSpeed));

        if (Input.GetKey(KeyCode.D))
            rigBody.MovePosition(transform.position += (transform.right * moveSpeed));

        if (Input.GetKey(KeyCode.Q))
            rigBody.MovePosition(transform.position -= (transform.forward * moveSpeed));

        if (Input.GetKey(KeyCode.E))
            rigBody.MovePosition(transform.position += (transform.forward * moveSpeed));
    }
}
