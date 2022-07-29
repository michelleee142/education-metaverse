using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampController : MonoBehaviour
{
    [SerializeField] Collider leftClamp;
    [SerializeField] Collider rightClamp;
    [SerializeField] GameObject myTrigger;
    [SerializeField] GameObject myTriggerMiddle;

    private MeshRenderer myMesh;
    private bool triggered = false;
    private bool closedMax = false;
    private bool openedMax = false;

    ClampTrigger triggerController;
    ClampTriggerMiddle triggerMiddleController;
    
    public void Awake()
    {
        // get trigger components
        triggerController = myTrigger.GetComponent<ClampTrigger>();
        triggerMiddleController = myTriggerMiddle.GetComponent<ClampTriggerMiddle>();
    }

    // tighten the clamps
    public bool Tighten(float amount, int handleLoc)
    {
        Vector3 change = new Vector3(amount, 0, 0);

        switch (handleLoc)
        {
            // bottom handle selected
            case 0:
                // check if max closed position has been reached
                if (closedMax == false)
                {
                    // check if trigger has been hit
                    if (triggerMiddleController.checkTriggerMiddle() == false)
                    {
                        // move clamps
                        leftClamp.transform.localPosition -= change / 10;
                        rightClamp.transform.localPosition += change / 10;
                        openedMax = false;

                        return true;
                    }
                    // don't move clamps if trigger already hit
                    else
                    {
                        closedMax = true;
                        return false;
                    }
                }
                // don't move clamps if max already reached
                else
                {
                    openedMax = false;
                    return false;
                }

                break;
 
            // top handle selected
            case 1:
                // check if max closed position has been reached
                if (closedMax == false)
                {
                    // check if trigger has been hit
                    if (triggerMiddleController.checkTriggerMiddle() == false)
                    {
                        // move clamps
                        leftClamp.transform.localPosition += change / 10;
                        rightClamp.transform.localPosition -= change / 10;
                        openedMax = false;

                        return true;
                    }
                    // don't move clamps if trigger already hit
                    else
                    {
                        closedMax = true;
                        return false;
                    }
                }
                // don't move clamps if max already reached
                else
                {
                    openedMax = false;
                    return false;
                }
                break;

            default:
                return false;
        }
       
    }

    // widen the clamps
    public bool Widen(float amount, int handleLoc)
    {
        Vector3 change = new Vector3(amount, 0, 0);

        switch (handleLoc)
        {
            // bottom handle selected
            case 0:
                // check if max open position has been reached
                if (openedMax == false)
                {
                    // check if trigger has been hit
                    if (triggerController.checkTrigger() == false)
                    {
                        // move clamps
                        leftClamp.transform.localPosition += change / 10;
                        rightClamp.transform.localPosition -= change / 10;
                        closedMax = false;

                        return true;
                    }
                    // don't move clamps if trigger already hit
                    else
                    {
                        openedMax = true;
                        return false;
                    }
                }
                // don't move clamps if max already reached
                else
                {
                    closedMax = false;
                    return false;
                }

                break;

            // top handle selected
            case 1:
                // check if max open position has been reached
                if (openedMax == false)
                {
                    // check if trigger has been hit
                    if (triggerController.checkTrigger() == false)
                    {
                        // move clamps
                        leftClamp.transform.localPosition -= change / 10;
                        rightClamp.transform.localPosition += change / 10;
                        closedMax = false;

                        return true;
                    }
                    // don't move clamps if trigger already hit
                    else
                    {
                        openedMax = true;
                        return false;
                    }
                }
                // don't move clamps if max already reached
                else
                {
                    closedMax = false;
                    return false;
                }

                break;

            default:
                return false;
        }
    }
}
