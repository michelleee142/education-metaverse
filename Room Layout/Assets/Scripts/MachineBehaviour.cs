using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MachineBehaviour : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] LayerMask targetLayer;

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

    // DELETE LATER
    public InputActionReference toggleReference = null;
    public GameObject machine;
    private MeshRenderer meshRenderer = null;

    // Awake 
    private void Awake()
    {
        interactReference.action.started += OnUse;
        interactReference.action.canceled += OnUseCancel;
        oppositeInteractReference.action.started += OnOpposite;
        oppositeInteractReference.action.canceled += OnOppositeCancel;

        // DELETE LATER
        /*toggleReference.action.started += Toggle;
        toggleReference.action.canceled += ToggleCancel;*/
        meshRenderer = machine.GetComponent<MeshRenderer>(); 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    // DELETE LATER
    private void Toggle(InputAction.CallbackContext context)
    {
        //bool isActive = !machine.activeSelf;

        if (!hasTarget)
        {
            RaycastHit hit;

            // check if target has been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
            {
                //machine.SetActive(isActive);
                meshRenderer.material.color = Color.red;

                hasTarget = true;

            }
        }

    } */

    // DELETE LATER
    /*private void ToggleCancel(InputAction.CallbackContext context)
    {
        // if a target has been hit, release it
        if (hasTarget)
        {
            hasTarget = false;
            meshRenderer.material.color = Color.blue;
        }
    } */

    // When user interacts with elements of machine
    void OnUse(InputAction.CallbackContext context)
    {
        // if a target has not been hit yet
        if (!hasTarget)
        {
            RaycastHit hit;

            // check if target has been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
            {
                Debug.Log("Target hit!");

                // save hit target
                target = hit.transform;
                hasTarget = true;

                Debug.Log(target.name);

                // save component of hit target
                ButtonController btn = hit.transform.GetComponent<ButtonController>();
                HandleController th = hit.transform.GetComponent<HandleController>();
                SwitchController sw = hit.transform.GetComponent<SwitchController>();

                // check which target was hit and perform desired action
                if (btn != null)
                {
                    targetType = Interactables.Button;
                    //btn.PressButton();
                }
                else if (th != null)
                {
                    // DELETE LATER
                    meshRenderer.material.color = Color.red;

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
                else
                {

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
                    ButtonController btn = target.GetComponent<ButtonController>();

                    // unpress button
                    if (btn != null)
                    {
                        //btn.UnpressButton();
                    }
                    break;

                case Interactables.TurnHandle:
                    // DELETE LATER
                    meshRenderer.material.color = Color.blue;

                    // get turnhandle component of hit target
                    HandleController th = target.GetComponent<HandleController>();

                    // ungrab turnhandle
                    if (th != null)
                    {
                        th.UnGrab(); //FIXME - NOT NEEDED
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

            // check if target has been hit
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
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
                    th.TurnCounterClockwise();
                    grabbed = true;
                }
                else if (sw != null)
                {
                    targetType = Interactables.Switch;
                    //sw.MoveDown();
                    grabbed = true;
                }

            }
        }

    }

    void OnOppositeCancel(InputAction.CallbackContext context)
    {
        // if a target has been hit, release it
        if (hasTarget)
        {
            // DELETE LATER
            meshRenderer.material.color = Color.yellow;

            // check which target has been hit and perform exit action
            switch (targetType)
            {
                case Interactables.TurnHandle:
                    // get turnhandle component of hit target
                    HandleController th = target.GetComponent<HandleController>();

                    // ungrab turnhandle
                    if (th != null)
                    {
                        th.UnGrab(); //FIXME - NOT NEEDED
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
