using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Player's speed
    private float moveSpeed = 6f;

    public void SetSize(float multiplier)
    { // Player becomes harder to push if heavier
        gameObject.transform.localScale *= multiplier;
        GetComponent<Rigidbody>().mass *= multiplier * 2;
    }

    public void SetSpeed(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}
