using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public enum SwitchState
    {
        Up, Down, Neutral
    }

    [SerializeField] GameObject topBlock;
    [SerializeField] float speed = 1f;

    private SwitchState state;

    // Start is called before the first frame update
    void Start()
    {
        state = SwitchState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            // user requests block to move up
            case SwitchState.Up:
                topBlock.position = new Vector3
                    (topblock.position.x, 
                    topBlock.position.y + (speed * Time.deltaTime), 
                    topBlock.position.z);

                break;

            // user requests block to move down
            case SwitchState.Down:
                topBlock.position = new Vector3
                   (topblock.position.x,
                   topBlock.position.y - (speed * Time.deltaTime),
                   topBlock.position.z);

                break;

            // block doesn't move
            default:
                break;
        }
    }

    public void MoveUp()
    {
        state = SwitchState.Up;

        // adjust switch
        transform.localPosition = upPosition;
        transform.localRotation = Quaternion.Euler(upRotation.x, upRotation.y, upRotation.z);

    }

    public void MoveDown()
    {
        state = SwitchState.Down;

        // adjust switch
        transform.localPosition = downPosition;
        transform.localRotation = Quaternion.Euler(downRotation.x, downRotation.y, downRotation.z);
    }

    public void MoveNeutral()
    {
        state = SwitchState.Neutral;

        // return switch to neutral
        transform.localPosition = neutralPosition;
        transform.localRotation = Quaternion.Euler(neutralRotation.x, neutralRotation.y, neutralRotation.z);
    }

}
