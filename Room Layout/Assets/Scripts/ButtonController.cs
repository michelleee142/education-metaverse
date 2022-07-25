using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public enum PushDirection
    {
        x, y, z
    }

    [SerializeField] Transform startPoint;
    [SerializeField] float depressMax;
    [SerializeField] PushDirection pushDirection;

    bool pressed;

    float minimum;          // min value for transition
    float maximum;          // max value for transition
    float startTime;
    float lastPos;
    float duration = 0.2f;  // time taken for transition

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
