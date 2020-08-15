using UnityEngine;

public class OldPlayerController : MonoBehaviour
{
	private Animator animator;
	private Vector3 movementInput;
	
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

		if (gameObject.transform.position.y < -2)
		{
			Debug.Log("Falling!");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collided!");
	}
}