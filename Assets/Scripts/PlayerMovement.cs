using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject camera;
    float Speed = 0.1f;

    public float lookSensitivity = 3.6f, lookSmoothDamp = 0.15f;
    public float yRot, xRot;
    public float currentY, currentX;
    public float yRotationV, xRotationV;    
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
    }
    float GetHorizontal()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            return -Speed;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            return Speed;

        return 0;
    }

    float GetVertical()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            return -Speed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            return Speed;

        return 0;
    }
    private void LateUpdate()
    {
        transform.position += new Vector3(GetHorizontal(), 0, GetVertical()) * Time.deltaTime;

        camera.transform.position = transform.position + new Vector3(0, 0.1f, 0);
        yRot += Input.GetAxis("Mouse X") * lookSensitivity; //its weird, x and y mouse values are swapped
        xRot += Input.GetAxis("Mouse Y") * lookSensitivity;
        currentY = Mathf.SmoothDamp(currentY, yRot, ref xRotationV, lookSmoothDamp);
        currentX = Mathf.SmoothDamp(currentX, xRot, ref yRotationV, lookSmoothDamp);

        xRot = Mathf.Clamp(xRot, -80, 80);

        camera.transform.rotation = Quaternion.Euler(-currentX, currentY, 0);
    }
}
