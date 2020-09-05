using UnityEngine;

public class Teleport : MonoBehaviour {
    private Transform destination_pad;

    private void Awake()
    {
        destination_pad = transform.GetChild(0).transform;
    }

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            other.transform.position = destination_pad.position + new Vector3(0, destination_pad.lossyScale.y, 0);
        }
    }
}