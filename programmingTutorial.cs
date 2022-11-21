using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class programmingTutorial : MonoBehaviour
{

    public Transform player;

    public float mouseSens = 2f;

    private float cameraRotationVertical = 0f;

    private Vector3 startingPos;

    private float defaultFOV = 40f;

    private float maxFOV = 50f;


    [SerializeField] private float fovLerpSpeedRunning, bobHeight, fovLerpSpeed, bobSpeed;

    [SerializeField] private Camera cam;

    private Vector3 targetCamPosition;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        startingPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis ("Mouse X") * mouseSens;
        float inputY = Input.GetAxis ("Mouse Y") * mouseSens;

        cameraRotationVertical -= inputY;
        cameraRotationVertical = Mathf.Clamp( value: cameraRotationVertical, min: -90f,
            max: 90f);

        headBobbing();
        changeFOV();
    }

    void headBobbing()
    {
        Vector3 targetCamPosition;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetCamPosition = new Vector3(0f, MathF.Sin(Time.time * fovLerpSpeedRunning) * bobHeight) + startingPos;
            }
            else
            {
                targetCamPosition = new Vector3(0f, MathF.Sin(Time.time * fovLerpSpeed) * bobHeight) + startingPos;
            }
        }
        else
        {
            targetCamPosition = startingPos;
        }
        
        transform.localPosition = Vector3.Slerp(transform.localPosition, targetCamPosition, Time.deltaTime * bobSpeed);
    }

    void changeFOV()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxFOV, 0.01f);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, 0.01f);
        }
    }
    
    
}
