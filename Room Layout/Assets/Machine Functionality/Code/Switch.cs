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

    public void SetUp() 
    { 
        state = SwitchState.Up; 
        transform.localRotation = Quaternion.Euler(15, 0, 0);
    }

    public void SetDown() 
    { 
        state = SwitchState.Down; 
        transform.localRotation = Quaternion.Euler(-15, 0, 0);
    }

    public void SetNeutral() 
    { 
        state = SwitchState.Neutral; 
        transform.localRotation = Quaternion.Euler(0, 0, 0);
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
