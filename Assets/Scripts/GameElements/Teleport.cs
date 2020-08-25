using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    public Vector3 destination;

    private void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.position = destination;
        }
    }

    IEnumerator Waiting () {
        yield return new WaitForSeconds (3);
    }
}