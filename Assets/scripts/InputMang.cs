using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMang : MonoBehaviour
{
    private PlayerInput playerinput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerRun run;
    private lookArPlayer look;
    // Start is called before the first frame update
    void Awake()
    {
        playerinput = new PlayerInput();
        onFoot = playerinput.OnFoot;
        run = GetComponent<PlayerRun>();
        look = GetComponent<lookArPlayer>();
        onFoot.jump.performed += ctx => run.Jump();
        onFoot.Crouch.performed += ctx => run.Crouch();
        onFoot.Sprint.performed += ctx => run.Sprint();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        run.ProcessMove(onFoot.movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.processLook(onFoot.lookAround.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
