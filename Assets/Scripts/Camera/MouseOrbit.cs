﻿using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Control;
using ThirdPersonGame.Interact;
using UnityEngine;

public class MouseOrbit : MonoBehaviour
{
    /* These variables are what tell the camera how its going to function by
      * setting the viewing target, collision layers, and other properties
      * such as distance and viewing angles */
    public Transform viewTarget;
    public LayerMask collisionLayers;
    public float distance = 6.0f;
    public float distanceSpeed = 150.0f;
    public float collisionOffset = 0.3f;
    public float minDistance = 4.0f;
    public float maxDistance = 12.0f;
    public float height = 1.5f;
    public float horizontalRotationSpeed = 250.0f;
    public float verticalRotationSpeed = 150.0f;
    public float rotationDampening = 0.75f;
    public float minVerticalAngle = -60.0f;
    public float maxVerticalAngle = 60.0f;
    public bool useRMBToAim = false;

    public float horizontalOffset = 1.5f;

    /* These variables are meant to store values given by the script and
     * not the user */
    private float h, v, smoothDistance;
    private Vector3 newPosition;
    private Quaternion newRotation, smoothRotation;
    private Transform cameraTransform;
    public static float timerot = 0;

    // player rotate variables
    public float angularSpeed;
    

    /* This is where we initialize our script */
    void Start()
    {        
        Initialize();        
    }

    /* This is where we set our private variables, check for null errors,
     * and anything else that needs to be called once during startup */
    void Initialize()
    {
        h = this.transform.eulerAngles.x;
        v = this.transform.eulerAngles.y;

        cameraTransform = this.transform;
        smoothDistance = distance;
        timerot = TimeSignature((1 / rotationDampening)) * 100.0f;
        NullErrorCheck();
    }

    /* We check for null errors or warnings and notify the user to fix them */
    void NullErrorCheck()
    {
        if (!viewTarget)
        {
            Debug.LogError("Please make sure to assign a view target!");
            Debug.Break();
        }
        if (collisionLayers == 0)
        {
            Debug.LogWarning("Make sure to set the collision layers to the layers the camera should collide with!");
        }
    }

    /* This is where we do all our camera updates. This is where the camera
     * gets all of its functionality. From setting the position and rotation,
     * to adjusting the camera to avoid geometry clipping */
    void LateUpdate()
    {
        //float mouseHorDirection = Input.GetAxis("Mouse X");
        //var animator = viewTarget.GetComponentInChildren<Animator>();

        //float keysOrbitingSpeed = 120;

        // if player is on a ledge (Grabbing), we do not want to turn the character
        InteractConstraints mouseConstraints = new InteractConstraints();        
        var control = viewTarget.transform.root.GetComponent<CharacterControl>();
        var isOnLedge = mouseConstraints.IsPlayerOnLedge(control);
        var isCrouching = mouseConstraints.IsPlayerCrouching(control);

        if (!viewTarget)
            return;

        if (Input.GetMouseButton(0))
        {
            RotateCamera();
        }

        if (Input.GetMouseButton(1) /*|| Input.GetMouseButton(0)*/ && !isOnLedge && !isCrouching)
        {
                RotateCamera();           

            #region
            //if (mouseHorDirection > Mathf.Epsilon)
            //{
            //    //animate rotate right
            //    animator.SetBool("TurnRight", true);
            //    animator.SetLookAtPosition(newRotation.eulerAngles);                
            //}
            //else
            //{
            //    animator.SetBool("TurnRight", false);
            //}
            #endregion

            // rotate player with Camera
            viewTarget.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            //Debug.Log("Mouse down at " + Input.GetAxis("Mouse X"));
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        #region
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    h = transform.eulerAngles.y + 90;
        //    newRotation = Quaternion.Euler(transform.eulerAngles.x, h, transform.eulerAngles.z);

        //    h = new Vector3(0, 360 + Time.deltaTime * keysOrbitingSpeed, 0);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    h = transform.eulerAngles.y - 90;
        //    newRotation = Quaternion.Euler(transform.eulerAngles.x, h, transform.eulerAngles.z);
        //}
        #endregion

        /* We set the distance by moving the mouse wheel and use a custom
     * growth function as the time value for linear interpolation */
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 10, minDistance, maxDistance);
        smoothDistance = Mathf.Lerp(smoothDistance, distance, TimeSignature(distanceSpeed));

        /*We give the rotation some smoothing for a nicer effect */
        smoothRotation = Quaternion.Slerp(smoothRotation, newRotation, TimeSignature((1 / rotationDampening) * 100.0f));

        newPosition = viewTarget.position;
        //newPosition += smoothRotation * new Vector3(0.0f, height, -smoothDistance);
        newPosition += smoothRotation * new Vector3(horizontalOffset, height, -smoothDistance);

        /* Calls the function to adjust the camera position to avoid clipping */
        CheckSphere();

        smoothRotation.eulerAngles = new Vector3(smoothRotation.eulerAngles.x, smoothRotation.eulerAngles.y, 0.0f);

        cameraTransform.position = newPosition;
        cameraTransform.rotation = smoothRotation;
    }

    private void RotateCamera()
    {
        /* Check to make sure the game isn't paused and lock the mouse cursor */
        if (Time.timeScale > 0.0f)
            Cursor.lockState = CursorLockMode.Locked;

        h += Input.GetAxis("Mouse X") * horizontalRotationSpeed * Time.deltaTime;
        v -= Input.GetAxis("Mouse Y") * verticalRotationSpeed * Time.deltaTime;

        h = ClampAngle(h, -360.0f, 360.0f);
        v = ClampAngle(v, minVerticalAngle, maxVerticalAngle);
        //				Debug.Log ("value of h: "+h);
        newRotation = Quaternion.Euler(v, h, 0.0f);
    }

    /* This is where the camera checks for a collsion hit within a specified radius,
     * and then moves the camera above the location it hit with an offset value */
    void CheckSphere()
    {
        /* Add height to our spherecast origin */
        Vector3 tmpVect = viewTarget.position;
        tmpVect.y += height;

        RaycastHit hit;

        /* Get the direction from the camera position to the origin */
        Vector3 dir = (newPosition - tmpVect).normalized;

        /* Check a radius for collision hits and then set the new position for
         * the camera */
        if (Physics.SphereCast(tmpVect, 0.3f, dir, out hit, distance, collisionLayers))
        {
            newPosition = hit.point + (hit.normal * collisionOffset);
        }
    }

    /* Keeps the angles values within their specificed minimum and maximum
     * inputs while at the same time putting the values back to 0 if they
     * go outside of the 360 degree range */
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;

        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    /* This is our custom logistic growth time signature with speed as input */
    private float TimeSignature(float speed)
    {
        return 1.0f / (1.0f + 80.0f * Mathf.Exp(-speed * 0.02f));
    }
}
