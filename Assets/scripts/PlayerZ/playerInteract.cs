using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class playerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUi playerUI;
    private InputMang inputmanager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<lookArPlayer>().cam;
        playerUI = GetComponent<PlayerUi>();
        inputmanager = GetComponent<InputMang>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.updateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Door>() != null)
            {
                Door interactable = hitInfo.collider.GetComponent<Door>();
                playerUI.updateText(interactable.promptMessage);
                if (inputmanager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
