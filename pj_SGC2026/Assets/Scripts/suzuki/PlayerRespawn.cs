using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
	public static PlayerRespawn instance;
	private Vector3 startPos;
	private Vector3 checkpointPosition;
	public float fallThreshold = -5f;

	private void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		if (transform.position.y < fallThreshold)
		{
			PlayerLife.instance.ReduceLife();
			Respawn();
		}
	}
	// ★チェックポイント更新
	public void SetCheckpoint(Vector3 pos)
	{
		checkpointPosition = pos;
		Debug.Log("チェックポイント更新: " + pos);
	}
	public void Respawn()
	{
		if (PlayerLife.instance.life <= 0) return; // ★ゲームオーバーなら復帰しない
		// ★チェックポイントへ復帰
		transform.position = checkpointPosition;

		var rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.linearVelocity = Vector3.zero;
		}
	}
	private void ResetCollisions()
	{
		Collider playerCol = GetComponent<Collider>();
		if (playerCol == null) return;

		var obstacles = Object.FindObjectsByType<ColorObstacles>(FindObjectsSortMode.None);

		foreach (var obstacle in obstacles)
		{
			Collider obstacleCol = obstacle.GetComponent<Collider>();
			if (obstacleCol != null)
			{
				Physics.IgnoreCollision(playerCol, obstacleCol, false);
			}
		}
	}
}
