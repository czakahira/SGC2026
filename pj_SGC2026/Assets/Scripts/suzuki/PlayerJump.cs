using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
	public float jumpForce = 7f;
	public int maxJumpCount = 2;

	private int jumpCount;
	private bool isGrounded;

	private Rigidbody rb;
	private InputAction jumpAction;

	void Awake()
	{
		var playerInput = GetComponent<PlayerInput>();
		jumpAction = playerInput.actions["Jump"];
	}

	void OnEnable()
	{
		jumpAction.performed += OnJump;
	}

	void OnDisable()
	{
		jumpAction.performed -= OnJump;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnJump(InputAction.CallbackContext ctx)
	{
		TryJump();
	}

	public void TryJump()
	{
		if (jumpCount < maxJumpCount)
		{
			Vector3 lv = rb.linearVelocity;
			lv.y = jumpForce;
			rb.linearVelocity = lv;

			jumpCount++;
		}
	}

	// 接地判定
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ground"))
		{
			isGrounded = true;
			jumpCount = 0;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("ground"))
		{
			isGrounded = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("ground"))
		{
			isGrounded = false;
		}
	}
}
