using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    public float scale = 1f;

    [SerializeField] GameObject clamp;
    [SerializeField] Transform center;

    ClampController clampController;

    public void Awake()
    {
        // get controller components
        clampController = clamp.GetComponent<ClampController>();
    }

    public void Grab()
    {

    }

    public void UnGrab()
    {

    }

    // turn handles clockwise
    public void TurnClockwise(float degrees, int handleLoc)
    {
        // if clamps can be moved, turn handle clockwise
        if (clampController.Tighten(degrees, handleLoc))
        {
            transform.RotateAround(center.position, Vector3.up, degrees);
        }
    }

    // turn handles counterclockwise
    public void TurnCounterClockwise(float degrees, int handleLoc)
    {
        // if clamps can be moved, turn handle counterclockwise
        if (clampController.Widen(degrees, handleLoc))
        {
            transform.RotateAround(center.position, Vector3.up, -degrees);
        }
    }
}
