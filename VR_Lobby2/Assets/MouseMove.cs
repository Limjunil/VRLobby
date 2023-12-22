using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sesitivity = 200f;
    private float rotationX;
    private float rotationY;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseMoveX = Input.GetAxis("Mouse X");

            float mouseMoveY = Input.GetAxis("Mouse Y");

            rotationY += mouseMoveX * sesitivity * Time.deltaTime;

            rotationX += mouseMoveY * sesitivity * Time.deltaTime;


            if (rotationX > 35f)
            {
                rotationX = 35f;
            }

            if (rotationX < -30f)
            {
                rotationX = -30f;
            }

            transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
        }
    }
}
