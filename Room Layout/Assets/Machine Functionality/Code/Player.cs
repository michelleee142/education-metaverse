using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float mass = 2f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] Transform cameraTransform;

    public enum Interactables 
    {
        Button,
        TurnHandle,
        Switch
    }

    private Transform target;
    private Interactables targetType;

    private bool hasTarget = false;
    private bool grabbed = false;

    //public event Action OnFire;
    //public event Action OnUse;

    Vector3 velocity;
    CharacterController controller;
    Vector2 look;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction sprintAction;
    InputAction fireAction;
    InputAction useAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        sprintAction = playerInput.actions["sprint"];
        fireAction = playerInput.actions["fire"];
        useAction = playerInput.actions["use"];

        useAction.started += _ => OnUse();
        
        useAction.canceled += _ => OnUseCancel();
    }

    void OnUse() 
    {
        if (!hasTarget) 
        {
            // interact with target
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit) && hit.transform.CompareTag("Interactable"))
            {
                Debug.Log("Hit interactable");
                target = hit.transform;
                hasTarget = true;

                Button btn = hit.transform.GetComponent<Button>();
                TurnHandle th = hit.transform.GetComponent<TurnHandle>();
                Switch sw = hit.transform.GetComponent<Switch>();
                if (btn != null) 
                {
                    targetType = Interactables.Button;
                    btn.PressButton();
                }
                else if (th != null)
                {
                    targetType = Interactables.TurnHandle;
                    th.Grab();
                    grabbed = true;
                }
                else if (sw != null) 
                {
                    targetType = Interactables.Switch;
                    grabbed = true;
                }
            }
        }
    }

    void OnUseCancel()
    {
        if (hasTarget) 
        {
            // release target
            switch(targetType) 
            {
                case Interactables.Button:
                    Button btn = target.GetComponent<Button>();
                    if (btn != null) { btn.UnpressButton(); }
                    break;
                case Interactables.TurnHandle:
                    TurnHandle th = target.GetComponent<TurnHandle>();
                    if (th != null) {
                        th.UnGrab();
                        grabbed = false;
                    }
                    break;
                case Interactables.Switch:
                    grabbed = false;
                    Switch sw = target.GetComponent<Switch>();
                    if (sw != null) {
                        sw.SetNeutral();
                    }
                    break;
            }
        }
        hasTarget = false;
        target = null;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (transform.position.y < -10f) { Die(); }

        UpdateGravity();
        if (grabbed) 
        {
            var moveInput = moveAction.ReadValue<Vector2>();
            switch(targetType) 
            {   
                case Interactables.TurnHandle:
                    if (moveInput.x != 0) 
                    {
                        target.GetComponent<TurnHandle>().Turn(moveInput.x * -360f);
                    }
                    break;
                case Interactables.Switch:
                    Switch sw = target.GetComponent<Switch>();
                    if (sw != null) 
                    {
                        if (moveInput.y > 0) { sw.SetUp(); }
                        else if (moveInput.y < 0) { sw.SetDown(); }
                        else { sw.SetNeutral(); }
                    }
                    break;
            }
        } 
        else
        {
            UpdateMovement();
        }
        UpdateLook();
        UpdateFire();
        // UpdateUse();
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    Vector3 GetMovementInput()
    {
        var moveInput = moveAction.ReadValue<Vector2>();
        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        var sprintInput = sprintAction.ReadValue<float>();
        var multiplier = sprintInput > 0 ? 1.5f : 1f;
        input *= movementSpeed * multiplier;
        return input;
    }

    void UpdateMovement()
    {
        Vector3 input = GetMovementInput();

        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        var jumpinput = jumpAction.ReadValue<float>();
        if (jumpinput > 0 && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }

    void UpdateFire()
    {
        var fireInput = fireAction.ReadValue<float>();
        if (fireInput > 0)
        {
            // Debug.Log("Fire");
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
            {
                // Debug.Log("Hit");
                
                // Enemy enemy = hit.transform.GetComponent<Enemy>();
                // if (enemy != null)
                // {
                //     enemy.TakeDamage(17); // TakeDamage(damage)
                // }
            }
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ran into");
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }


}
