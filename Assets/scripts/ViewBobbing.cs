using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PositionFollow))]
public class ViewBobbing : MonoBehaviour
{
    public float effectIntensity;
    public float effectIntensityX;
    public float effectSpeed;
    private PositionFollow followeInstance;
    private Vector3 OriginalOffset;
    private float sinTime;
    // Start is called before the first frame update
    void Start()
    {
        followeInstance = GetComponent<PositionFollow>();
        OriginalOffset = followeInstance.offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        if(inputVector.magnitude > 0f)
        {
            sinTime += Time.deltaTime * effectSpeed;
        }
        else
        {
            sinTime = 0f;
        }
        float sinAmountY = -Mathf.Abs(effectIntensity * Mathf.Sin(sinTime));
        Vector3 sinAmountX = followeInstance.transform.right * effectIntensity * Mathf.Cos(sinTime) * effectIntensityX;
        followeInstance.offset = new Vector3
        {
            x = OriginalOffset.x,
            y = OriginalOffset.y + sinAmountY,
            z = OriginalOffset.z
        };
        followeInstance.offset += sinAmountX;
    }
}
