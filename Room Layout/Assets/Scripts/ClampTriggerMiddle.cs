using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampTriggerMiddle : MonoBehaviour
{
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clamps")
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Clamps")
        {
            triggered = false;
        }
    }

    // check if clamps have hit trigger
    public bool checkTriggerMiddle()
    {
        return triggered;
    }
}
