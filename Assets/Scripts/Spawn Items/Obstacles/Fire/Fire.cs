using UnityEngine;

public class Fire : MonoBehaviour {

    // TODO: consider changing to onTriggerEnter if player is invincible
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}