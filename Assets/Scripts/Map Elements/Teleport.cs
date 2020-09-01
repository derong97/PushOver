using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    private Vector3 destination;

    private void Start()
    {
        destination = transform.GetChild(0).transform.position;
    }

    private void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.position = destination;
        }
    }

    IEnumerator Waiting () {
        yield return new WaitForSeconds (3);
    }
}