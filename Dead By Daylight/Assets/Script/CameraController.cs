using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public Camera survCamera;
    public float cameraSpeed;

    public float minYRotation;
    public float maxYRotation;

    float mouseX = 0;
    float mouseY = 0;

    public Transform playerPos;
    public Vector3 cameraPos;


    private void Start()
    {
        survCamera = Camera.main;
        playerPos = transform.parent;
    }

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * cameraSpeed;
        mouseY += Input.GetAxis("Mouse Y") * cameraSpeed;

        mouseY = Mathf.Clamp(mouseY, minYRotation, maxYRotation);
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);

        transform.position = (playerPos.position + cameraPos);
    }
}
