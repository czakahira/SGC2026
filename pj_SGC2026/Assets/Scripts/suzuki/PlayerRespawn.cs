using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPos;
    //落下判定高度
    public float fallThreshold = -5f;
	public bool isFalling = false;
	void Start()
    {
        startPos = transform.position;
    }
	private void Update()
	{
		if (transform.position.y < fallThreshold)
        {
            PlayerLife.instance.ReduceLife();
            Respawn();
        }
	}
	public void Respawn()
    {
        transform.position = startPos;
        //速度リセット
        var rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            rb.linearVelocity = Vector3.zero;
        }
        ResetCollisions();
    }
	private void ResetCollisions()
	{
		Collider playerCol = GetComponent<Collider>();
        if (playerCol == null ) return;
		var obstacles = Object.FindObjectsByType<ColorObstacles>(FindObjectsSortMode.None);

		foreach (var obstacle in obstacles)
		{
			Collider obstacleCol = obstacle.GetComponent<Collider>();
			if (obstacleCol != null)
			{
				// IgnoreCollision を解除
				Physics.IgnoreCollision(playerCol, obstacleCol, false);
			}
		}

	}
}
