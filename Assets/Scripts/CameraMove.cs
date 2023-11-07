using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


/*
 * This script done by Thomas Decreus and Adrian Wisniewski allows to control the camera
 */

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 20.0f; 
    public float rotationSpeed = 0.1f; 
    public float zoomSpeed = 0.01f;
    public float zoomMin = 0.0f;
    public float zoomMax = 0.8f;
    private float currentZoom = 0.4f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Tilt using the mouse wheel
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        transform.Rotate(Vector3.left, zoomInput * rotationSpeed);
        currentZoom -= zoomInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, zoomMin, zoomMax);
        transform.position = new Vector3(transform.position.x, 20, transform.position.z);
    }
}
