using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineBehaviour : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;

    public enum Interactables
    {
        Button,
        TurnHandle,
        Switch
    }

    public InputActionReference interactReference = null;

    private Transform target;
    private Interactables targetType;

    private bool hasTarget = false;
    private bool grabbed = false;

    // Awake 
    private void Awake()
    {
        interactReference.action.started += OnUse;
        interactReference.action.canceled += OnUseCancel;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // When user interacts with elements of machine
    void OnUse(InputAction.CallbackContext context)
    {
        // if a target has not been hit yet
        if (!hasTarget)
        {
            RaycastHit hit;

            // check if target has been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit) && hit.transform.CompareTag("Interactable"))
            {
                // save hit target
                target = hit.transform;
                hasTarget = true;

                // save component of hit target
                Button btn = hit.transform.GetComponent<Button>();
                TurnHandle th = hit.transform.GetComponent<TurnHandle>();
                Switch sw = hit.transform.GetComponent<Switch>();

                // check which target was hit and perform desired action
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

    void OnUseCancel(InputAction.CallbackContext context)
    {
        // if a target has been hit, release it
        if (hasTarget)
        {
            // check which target has been hit and perform exit action
            switch (targetType)
            {
                case Interactables.Button:
                    // get button component of hit target
                    Button btn = target.GetComponent<Button>();

                    // unpress button
                    if (btn != null)
                    {
                        btn.UnpressButton();
                    }
                    break;

                case Interactables.TurnHandle:
                    // get turnhandle component of hit target
                    TurnHandle th = target.GetComponent<TurnHandle>();

                    // ungrab turnhandle
                    if (th != null)
                    {
                        th.UnGrab();
                        grabbed = false;
                    }
                    break;

                case Interactables.Switch:
                    // get switch component of hit target 
                    Switch sw = target.GetComponent<Switch>();
                    grabbed = false;

                    // set switch to neutral
                    if (sw != null)
                    {
                        sw.SetNeutral();
                    }
                    break;
            }

        }
        // reset values
        hasTarget = false;
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    // Up switch pressed - move block up
    public void Switch_press_up()
    {

    }

    // Down switch pressed - move block down
    public void Switch_press_down()
    {

    }

    // Right turn clamp pressed - twist clockwise
    public void Clamp_clockwise()
    {

    }

    // Left turn clamp pressed - twist counterclockwise
    public void Clamp_counterclockwise()
    {

    }

    // Emergency button pressed - animate button and stop test
    public void Button_press()
    {

    }
}
