using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandle : MonoBehaviour
{
    public float scale = 0.01f;
    [SerializeField] Clamp clamp;
    [SerializeField] Transform center;

    public void Grab() 
    {
        // Debug.Log("AAH! Ya Got Me!");
    }

    public void UnGrab() 
    {
        // Debug.Log("I'm free!");
    }

    public void Turn(float degrees) 
    {
        // transform.Rotate(0, degrees, 0);
        if (clamp.Widen(-degrees * scale * Time.deltaTime))
        {
            transform.RotateAround(center.position, Vector3.up, degrees * Time.deltaTime);
        }
        // if (clamp.Widen(degrees * scale * Time.deltaTime))
        // {
        //     transform.Rotate(0, degrees, 0);
        // }
    }
}
