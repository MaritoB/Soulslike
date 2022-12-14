using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MB
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal, vertical, moveAmount, mouseX, mouseY;
        public bool b_input, rollFlag, isInteracting;


        PlayerControls inputActions;
        CameraHandler cameraHandler;
        Vector2 movementInput, cameraInput;


        private void Awake()
        {
            cameraHandler = CameraHandler.instance;
        }
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
            else
            {
                cameraHandler = CameraHandler.instance;
            }
        }
        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();  
            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
        }
        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollInput(float delta)
        {
            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            if (b_input)
            {
                rollFlag = true;
            }
        }
    }
}
