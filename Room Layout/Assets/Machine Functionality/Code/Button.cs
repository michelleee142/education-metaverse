using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public enum PushDirection 
    {
        x, y, z
    }

    [SerializeField] Transform startPoint;
    [SerializeField] float depressMax;
    [SerializeField] PushDirection pushDirection;

    bool pressed;

    // Minimum and maximum values for the transition.
    float minimum;
    float maximum;
    float startTime;
    float lastPos;

    // Time taken for the transition.
    float duration = 0.2f;

    void Start() 
    {
        pressed = false;

        switch(pushDirection) 
        {
            case PushDirection.x:
                maximum = startPoint.localPosition.x;
                minimum = startPoint.localPosition.x - depressMax;
                lastPos = transform.localPosition.x;
                break;
            case PushDirection.y:
                maximum = startPoint.localPosition.y;
                minimum = startPoint.localPosition.y - depressMax;
                lastPos = transform.localPosition.y;
                break;
            default:
                maximum = startPoint.localPosition.z;
                minimum = startPoint.localPosition.z - depressMax;
                lastPos = transform.localPosition.z;
                break;
        }

        startTime = Time.time;
    }

    public void PressButton() {
        // button press code here
        Debug.Log("I am pressed.");
        UpdateLastPos();
        startTime = Time.time;
        pressed = true;
    }

    void UpdateLastPos()
    {
        switch(pushDirection) 
        {
            case PushDirection.x:
                lastPos = transform.localPosition.x;
                break;
            case PushDirection.y:
                lastPos = transform.localPosition.y;
                break;
            default:
                lastPos = transform.localPosition.z;
                break;
        }
    }

    public void UnpressButton() {
        Debug.Log("I am no longer pressed.");
        UpdateLastPos();
        startTime = Time.time;
        pressed = false;
        // StartCoroutine(GoUp());
    }

    void Update() 
    {
        if (!pressed) 
        {
            float t = (Time.time - startTime) / duration;
            // transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(minimum, maximum, t), transform.position.z);
            UpdatePosition(minimum, maximum);
        }
        else 
        {
            float t = (Time.time - startTime) / duration;
            // transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(maximum, minimum, t), transform.position.z);
            UpdatePosition(maximum, minimum);
        }
    }

    void UpdatePosition(float pos1, float pos2)
    {
        float t = (Time.time - startTime) / duration;
        float newPos = Mathf.SmoothStep(pos1, pos2, t);
        switch(pushDirection) 
        {
            case PushDirection.x:
                transform.localPosition = new Vector3(newPos, transform.localPosition.y, transform.localPosition.z);
                break;
            case PushDirection.y:
                transform.localPosition = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
                break;
            default:
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newPos);
                break;
        }
    }

}
