using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float maxSpeed = 5f;
	public float acceleration = 2f;

	private Rigidbody rb;
	private float currentSpeed = 0f;

	private bool canMove = true;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (!canMove) return;

		// 徐々にスピードを増やす
		currentSpeed += acceleration * Time.fixedDeltaTime;

		// 最大速度制限
		currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

		// 移動
		Vector3 vel = rb.linearVelocity;
		vel.x = currentSpeed;
		rb.linearVelocity = vel;
	}

	public void StopMove()
	{
		canMove = false;

		currentSpeed = 0f;

		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	public void ResetSpeed()
	{
		currentSpeed = 0f;
	}
}
