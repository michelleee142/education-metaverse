using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public enum SwitchState 
    {
        Up, Down, Neutral
    }

    [SerializeField] Transform block;
    [SerializeField] float speed = 1f;
    private SwitchState state;
    [SerializeField] Vector3 upPosition;
    [SerializeField] Vector3 upRotation;
    [SerializeField] Vector3 downPosition;
    [SerializeField] Vector3 downRotation;
    [SerializeField] Vector3 neutralPosition;
    [SerializeField] Vector3 neutralRotation;


    public void SetUp() 
    { 
        state = SwitchState.Up; 
        // transform.localRotation = Quaternion.Euler(15, 0, 0);
        transform.localPosition = upPosition;
        transform.localRotation = Quaternion.Euler(upRotation.x, upRotation.y, upRotation.z);

    }

    public void SetDown() 
    { 
        state = SwitchState.Down; 
        // transform.localRotation = Quaternion.Euler(-15, 0, 0);
        transform.localPosition = downPosition;
        transform.localRotation = Quaternion.Euler(downRotation.x, downRotation.y, downRotation.z);
    }

    public void SetNeutral() 
    { 
        state = SwitchState.Neutral; 
        // transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localPosition = neutralPosition;
        transform.localRotation = Quaternion.Euler(neutralRotation.x, neutralRotation.y, neutralRotation.z);
    }

    // moves an object up and down

    // Start is called before the first frame update
    void Start()
    {
        state = SwitchState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case SwitchState.Up:
                // block moves up
                block.position = new Vector3(
                    block.position.x, 
                    block.position.y + (speed * Time.deltaTime), 
                    block.position.z
                    );

                break;
            case SwitchState.Down:
                // block moves down
                block.position = new Vector3(
                    block.position.x, 
                    block.position.y - (speed * Time.deltaTime), 
                    block.position.z
                    );
                break;
            default: // block doesn't move
                break;
        }
    }
}
