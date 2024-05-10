using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookArPlayer : MonoBehaviour
{
    public Camera cam;
    private float Xrotation = 0f;
    public float Xsens = 30f;
    public float Ysens = 30f;
    
    public void processLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        Xrotation -= (mouseY * Time.deltaTime) * Ysens;
        Xrotation = Mathf.Clamp(Xrotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * Xsens);
    }
}
