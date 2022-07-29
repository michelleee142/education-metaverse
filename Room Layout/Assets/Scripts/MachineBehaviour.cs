using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MachineBehaviour : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask bottomHandleLayer;
    [SerializeField] LayerMask topHandleLayer;
    //[SerializeField] Transform center;
    [SerializeField] GameObject bottomHandle;
    [SerializeField] GameObject topHandle;

    HandleController bottomHandleController;
    HandleController topHandleController;

    public enum Interactables
    {
        Button,
        TurnHandle,
        Switch
    }

    // for all interact actions
    public InputActionReference interactReference = null;

    // for interact actions requiring a reversal of movement (i.e. turning handle counterclockwise, moving switch downwards)
    public InputActionReference oppositeInteractReference = null;

    private Transform target;
    private Interactables targetType;
    private bool hasTarget = false;
    private bool grabbed = false;
    private float triggerLValue;        // for counterclockwise motion
    private float triggerRValue;        // for clockwise motion
    private bool turnClockwise = false;
    private bool turnCounterClockwise = false;
    private int handleLocation = 0;   // bottom = 0 and top = 1

    // DELETE LATER
    //public InputActionReference toggleReference = null;
    public GameObject machine;
    private MeshRenderer meshRenderer = null;


    // Awake 
    private void Awake()
    {
        // setup XR user interaction methods
        interactReference.action.started += OnUse;
        interactReference.action.canceled += OnUseCancel;
        oppositeInteractReference.action.started += OnOpposite;
        oppositeInteractReference.action.canceled += OnOppositeCancel;

        // get various components
        meshRenderer = machine.GetComponent<MeshRenderer>();
        bottomHandleController = bottomHandle.GetComponent<HandleController>();
        topHandleController = topHandle.GetComponent<HandleController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // read in trigger value
        triggerRValue = interactReference.action.ReadValue<float>();

        // read in grip value
        triggerLValue = oppositeInteractReference.action.ReadValue<float>();

        // turn handle clockwise if user has triggered handle
        if (turnClockwise)
        {
            if (handleLocation == 0)
            {
                bottomHandleController.TurnClockwise(triggerRValue, handleLocation);
            }
            else if (handleLocation == 1)
            {
                topHandleController.TurnClockwise(triggerRValue, handleLocation);
            }
        }
        // turn handle counterclockwise if user has triggered handle
        if (turnCounterClockwise)
        {
            if (handleLocation == 0)
            {
                bottomHandleController.TurnCounterClockwise(triggerLValue, handleLocation);
            }
            else if (handleLocation == 1)
            {
                topHandleController.TurnCounterClockwise(triggerLValue, handleLocation);
            }
        }

    }

    // When user interacts with elements of machine
    void OnUse(InputAction.CallbackContext context)
    {
        // if a target has not been hit yet
        if (!hasTarget)
        {
            RaycastHit hit;

            // check if bottom handles have been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, bottomHandleLayer))
            {
                // DEBUGING
                meshRenderer.material.color = Color.red;

                // store bottom handle hit
                handleLocation = 0;

                // save hit target
                target = hit.transform;
                hasTarget = true;

                // save target type
                targetType = Interactables.TurnHandle;

                // set bool turnClockwise to true
                turnClockwise = true;
            }

            // check if top handles have been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, topHandleLayer))
            {
                // DEBUGING
                meshRenderer.material.color = Color.blue;

                // store top handle hit
                handleLocation = 1;

                // save hit target
                target = hit.transform;
                hasTarget = true;

                // save target type
                targetType = Interactables.TurnHandle;

                // set bool turnClockwise to true
                turnClockwise = true;
            }

            /*
            // check if target has been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
            {
                Debug.LogError("Target hit!");

                // save hit target
                target = hit.transform;
                hasTarget = true;

                // save component of hit target
                ButtonController btn = hit.transform.gameObject.GetComponent<ButtonController>();
                HandleController th = hit.transform.gameObject.GetComponent<HandleController>();
                SwitchController sw = hit.transform.gameObject.GetComponent<SwitchController>();

                // check which target was hit and perform desired action
                if (btn != null)
                {
                    targetType = Interactables.Button;
                    //btn.PressButton();
                }
                else if (th != null)
                {

                    targetType = Interactables.TurnHandle;
                    //th.TurnClockwise(interactReference.action.ReadValue<float>());
                    th.TurnClockwise();
                    grabbed = true;
                }
                else if (sw != null)
                {
                    targetType = Interactables.Switch;
                    //sw.MoveUp();
                    grabbed = true;
                }

            } */
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
                    ButtonController btn = target.GetComponent<ButtonController>();

                    // unpress button
                    if (btn != null)
                    {
                        //btn.UnpressButton();
                    }
                    break;

                case Interactables.TurnHandle:
                    // get turnhandle component of hit target
                    HandleController th = target.GetComponent<HandleController>();

                    // ungrab turnhandle
                    if (th != null)
                    {
                        //th.UnGrab(); //FIXME - NOT NEEDED
                        turnClockwise = false;
                        grabbed = false;
                    }
                    break;

                case Interactables.Switch:
                    // get switch component of hit target 
                    SwitchController sw = target.GetComponent<SwitchController>();
                    grabbed = false;

                    // set switch to neutral
                    if (sw != null)
                    {
                        //sw.SetNeutral();
                    }
                    break;
            }

        }

        // reset values
        hasTarget = false;
        target = null;
    }

    void OnOpposite(InputAction.CallbackContext context)
    {
        // if a target has not been hit yet
        if (!hasTarget)
        {
            RaycastHit hit;

            // check if bottom handles have been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, bottomHandleLayer))
            {
                // DEBUGING
                meshRenderer.material.color = Color.green;

                // store bottom handle hit
                handleLocation = 0;

                // save hit target
                target = hit.transform;
                hasTarget = true;

                // save target type
                targetType = Interactables.TurnHandle;

                // set bool turnClockwise to true
                turnCounterClockwise = true;
            }

            // check if top handles have been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, topHandleLayer))
            {
                // DEBUGING
                meshRenderer.material.color = Color.yellow;

                // store top handle hit
                handleLocation = 1;

                // save hit target
                target = hit.transform;
                hasTarget = true;

                // save target type
                targetType = Interactables.TurnHandle;

                // set bool turnClockwise to true
                turnCounterClockwise = true;
            }
            /*
            // if a target has not been hit yet
            if (!hasTarget)
            {
                RaycastHit hit;

                // check if target has been hit
                if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, handleLayer))
                {
                    // save hit target
                    target = hit.transform;
                    hasTarget = true;

                    // DELETE LATER
                    meshRenderer.material.color = Color.green;

                    // save component of hit target
                    HandleController th = hit.transform.GetComponent<HandleController>();
                    SwitchController sw = hit.transform.GetComponent<SwitchController>();

                    // check which target was hit and perform desired action
                    if (th != null)
                    {
                        targetType = Interactables.TurnHandle;
                        //th.TurnCounterClockwise(oppositeInteractReference.action.ReadValue<float>());
                        //th.TurnCounterClockwise();
                        grabbed = true;
                    }
                    else if (sw != null)
                    {
                        targetType = Interactables.Switch;
                        //sw.MoveDown();
                        grabbed = true;
                    }

                }
            }*/

        }
    }

    void OnOppositeCancel(InputAction.CallbackContext context)
    {
        // if a target has been hit, release it
        if (hasTarget)
        {

            // check which target has been hit and perform exit action
            // check which target has been hit and perform exit action
            switch (targetType)
            {
                case Interactables.TurnHandle:
                    // get turnhandle component of hit target
                    HandleController th = target.GetComponent<HandleController>();

                    // ungrab turnhandle
                    if (th != null)
                    {
                        //th.UnGrab(); //FIXME - NOT NEEDED
                        turnCounterClockwise = false;
                        grabbed = false;
                    }
                    break;

                case Interactables.Switch:
                    // get switch component of hit target 
                    SwitchController sw = target.GetComponent<SwitchController>();
                    grabbed = false;

                    // set switch to neutral
                    if (sw != null)
                    {
                        //sw.SetNeutral();
                    }
                    break;
            }

        }

        // reset values
        hasTarget = false;
        target = null;
    }

}
