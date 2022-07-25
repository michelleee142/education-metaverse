using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    public float scale = 0.01f;
    [SerializeField] ClampController clamp;
    [SerializeField] Transform center;

    public void Grab()
    {

    }

    public void UnGrab()
    {

    }

    public void TurnClockwise()
    {
        /*if (clamp.Widen(-degrees * scale * Time.deltaTime))
        {
            transform.RotateAround(center.position, Vector3.up, degrees * Time.deltaTime);
        }*/
        transform.RotateAround(center.position, Vector3.up, 90 * Time.deltaTime);
    }

    public void TurnCounterClockwise()
    {
        /*if (clamp.Widen(-degrees * scale * Time.deltaTime))
        {
            transform.RotateAround(center.position, Vector3.up, degrees * Time.deltaTime);
        }*/
        transform.RotateAround(center.position, Vector3.up, 90 * Time.deltaTime);

    }
}
