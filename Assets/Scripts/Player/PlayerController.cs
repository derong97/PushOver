using Mirror;
using UnityEngine;
public class PlayerController : NetworkBehaviour
{
    private PlayerStats playerStats;
    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

    private void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.Find ("PlayerModel").GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    [Client]
    private void Update ()
    {
        if (!hasAuthority) { 
            return; 
        }
        if (!Input.anyKey)
        {
            animator.SetBool("Walking", false);
            return;
        }

        CmdMove();

        animator.SetBool("Walking", true);
    }

    [Command]
    private void CmdMove()
    {
        UpdatePlayerMovement();
    }

    [ClientRpc]
    private void UpdatePlayerMovement ()
    {
        float moveSpeed = playerStats.GetSpeed();

        if (Input.GetKey (KeyCode.UpArrow))
        { //Up movement
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 0, 0);
        }

        if (Input.GetKey (KeyCode.LeftArrow))
        { //Left movement
            rigidBody.velocity = new Vector3 (-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 270, 0);
        }

        if (Input.GetKey (KeyCode.DownArrow))
        { //Down movement
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 180, 0);
        }

        if (Input.GetKey (KeyCode.RightArrow))
        { //Right movement
            rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 90, 0);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
