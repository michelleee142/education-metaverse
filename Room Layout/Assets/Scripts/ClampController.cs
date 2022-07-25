using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampController : MonoBehaviour
{
    [SerializeField] float maxWidth = 0.5f;
    [SerializeField] float minWidth = 0.02f;
    [SerializeField] Collider center;
    [SerializeField] Collider leftClamp;
    [SerializeField] Collider rightClamp;

    public bool Widen(float amount)
    {
        // return false if desired width exceeds max
        if (center.transform.localScale.x + amount > maxWidth)
        {
            return false;
        }

        // return false if desired width is less than min
        if (center.transform.localScale.x + amount < minWidth)
        {
            return false;
        }

        Vector3 change = new Vector3(amount, 0, 0);

        // adjust new center
        center.transform.localScale += change;

        // adjust clamp position
        leftClamp.transform.localPosition += change / 2;
        rightClamp.transform.localPosition -= change / 2;

        return true;
    }
}
