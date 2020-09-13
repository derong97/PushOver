using Mirror;
using System.Collections;
using UnityEngine;

abstract public class PowerUp : MonoBehaviour
{
    public GameObject attached_block = null;
    protected float multiplier = 1f;
    protected float effectSeconds;
    protected PlayerStats.DelegateMethod method;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    protected virtual IEnumerator PickUp(Collider player)
    {
        BlockManager.Instance.FreeBlock(attached_block);
        
        method(multiplier); // Apply effects to the player

        // Cannot Destroy object yet, else script will disappear
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(effectSeconds);

        Destroy(gameObject);

        if (player != null) // Player might die during this period
        {
            method(1 / multiplier); // Reverse the effect on our player
        }
    }
}
