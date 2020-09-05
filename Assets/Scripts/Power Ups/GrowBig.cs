using UnityEngine;

public class GrowBig : PowerUp
{
    private void Awake()
    {
        multiplier = 1.2f;
        effectSeconds = 10f;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        method = other.GetComponent<PlayerStats>().SetSize;
        base.OnTriggerEnter(other);
    }
}
