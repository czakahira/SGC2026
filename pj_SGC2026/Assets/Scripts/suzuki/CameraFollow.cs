using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;

	private float fixedZ;

	void Start()
	{
		fixedZ = transform.position.z; // Zだけ固定
	}

	void LateUpdate()
	{
		transform.position = new Vector3(
		target.position.x + offset.x,
		offset.y, // Yは固定でもOK
		fixedZ
		);
	}
}
