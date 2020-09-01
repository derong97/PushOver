using System.Collections;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private float duration = 10f;
    private float multiplier = 2f;

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
        stats.SetSpeed(multiplier);

        // Cannot Destroy object yet, else script will disappear
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Wait x amounts of seconds
        yield return new WaitForSeconds(duration);

        // Remove power up object
        Destroy(gameObject);

        // Reverse the effect on our player
        stats.SetSpeed(1/multiplier);
    }
}
