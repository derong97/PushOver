using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float multiplier = 1f;
    public delegate void DelegateMethod(float multiplier);

    public void CallMethod(DelegateMethod method)
    {
        method(multiplier);
    }

    public void SetSize(float multiplier)
    { // Player becomes harder to push if heavier
        this.multiplier = multiplier;
        gameObject.transform.localScale *= multiplier;
        GetComponent<Rigidbody>().mass *= multiplier * 2;
    }

    public void SetSpeed(float multiplier)
    {
        this.multiplier = multiplier;
        moveSpeed *= multiplier;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}
