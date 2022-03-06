using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Vector3 mouseMovement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		if (GetComponent<NetworkView>().isMine) {
			float horizontal = Input.GetAxisRaw ("Horizontal");
			float vertical = Input.GetAxisRaw ("Vertical");
			bool mouseMoveDown = Input.GetMouseButton (1);
			Move (horizontal, vertical);
			MouseMove (mouseMoveDown);
			Turning ();
			Animating (horizontal, vertical, mouseMoveDown);
		}
	}

	void Move(float horizontal, float vertical)
	{
		movement.Set (horizontal, 0f, vertical);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void MouseMove(bool mouseMoveDown)
	{

		mouseMovement.Set (0f, 0f, 1f);
		mouseMovement = mouseMovement * speed * Time.deltaTime;
		if (mouseMoveDown) 
		{
			playerRigidbody.MovePosition(transform.position + transform.rotation * mouseMovement);
		}
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float horizontal, float vertical, bool mouseMoveDown)
	{
		bool walking = horizontal != 0f || vertical != 0f || mouseMoveDown;
		anim.SetBool ("IsWalking", walking);
	}
}
