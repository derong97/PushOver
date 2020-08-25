using UnityEngine;

public class Controller : MonoBehaviour
{
	public Rigidbody player;
	public AudioSource scream;
	public Vector3 movementInput;
	public Animator animator;
	bool soundPlayed = false;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Update()
	{
		movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		transform.LookAt(transform.position + movementInput);

		if (movementInput != Vector3.zero)
		{
			animator.SetInteger("isMoving", 1);
		}
		else
		{
			animator.SetInteger("isMoving", 0);
		}

		if (player.position.y < -2)
		{
			if (!soundPlayed)
			{
				scream.Play();
				soundPlayed = true;
			}
		}
	}
}