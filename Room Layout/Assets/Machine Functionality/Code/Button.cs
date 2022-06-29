using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] float depressMax;

    bool pressed;

    // Minimum and maximum values for the transition.
    float minimum;
    float maximum;
    float startTime;
    float lastY;

    // Time taken for the transition.
    float duration = 0.2f;

    void Start() 
    {
        pressed = false;
        maximum = startPoint.position.y;
        minimum = startPoint.position.y - depressMax;
        lastY = transform.position.y;
        startTime = Time.time;
    }

    public void PressButton() {
        // button press code here
        Debug.Log("I am pressed.");
        lastY = transform.position.y;
        startTime = Time.time;
        pressed = true;
        // StartCoroutine(GoDown());
    }

    // public IEnumerator GoDown() {
    //     float startTime = Time.time;
    //     while (true) 
    //     {
    //         // Calculate the fraction of the total duration that has passed.
    //         float t = (Time.time - startTime) / duration;
    //         transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(minimum, maximum, t), transform.position.z);
    //     }
    //     yield return null;
    // }

    public void UnpressButton() {
        Debug.Log("I am no longer pressed.");
        lastY = transform.position.y;
        startTime = Time.time;
        pressed = false;
        // StartCoroutine(GoUp());
    }

    // public IEnumerator GoUp() {
    //     float startTime = Time.time;
    //     while (Time.time - startTime <= duration) 
    //     {
    //         // Calculate the fraction of the total duration that has passed.
    //         float t = (Time.time - startTime) / duration;
    //         transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(maximum, minimum, t), transform.position.z);
    //     }
    //     yield return null;
    // }

    void Update() 
    {
        if (!pressed) 
        {
            float t = (Time.time - startTime) / duration;
            // transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(minimum, maximum, t), transform.position.z);
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(lastY, maximum, t), transform.position.z);

        }
        else 
        {
            float t = (Time.time - startTime) / duration;
            // transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(maximum, minimum, t), transform.position.z);
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(lastY, minimum, t), transform.position.z);
        }
    }

}
