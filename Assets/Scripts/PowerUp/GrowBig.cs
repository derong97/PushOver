using System.Collections;
using UnityEngine;

public class GrowBig : MonoBehaviour
{
    private float duration = 10f;
    private float multiplier = 1.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider player)
    {
        // Apply effect to the player
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.SetSize(multiplier);

        // Cannot Destroy object yet, else script will disappear
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Wait x amounts of seconds
        yield return new WaitForSeconds(duration);

        // Remove power up object
        Destroy(gameObject);

        // Reverse the effect on our player
        stats.SetSize(1/multiplier);
    }
}
