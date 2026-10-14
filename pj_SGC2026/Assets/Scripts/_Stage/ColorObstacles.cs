
using UnityEngine;


/// <summary>
/// 色：障害物
/// </summary>
public class ColorObstacles : MonoBehaviour
{
	public EColorType obstacleColor;

	// ★ここは“壁本体”のコライダーだけ
	[SerializeField] private Collider obstacleCol;

	private void Awake()
	{
		// Inspector で入れるならここはなくてもOK
		if (obstacleCol == null)
			obstacleCol = GetComponent<Collider>();
	}

	private void Update()
	{
		var player = PlayerColor.instance;
		if (player == null) return;

		var playerCol = player.GetComponent<Collider>();
		if (playerCol == null) return;

		bool sameColor = (player.currentColor == obstacleColor);

		// 壁本体との衝突だけを無効化
		Physics.IgnoreCollision(playerCol, obstacleCol, sameColor);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var player = collision.gameObject.GetComponent<PlayerColor>();
		if (player == null) return;

		if (player.currentColor != obstacleColor)
		{
			PlayerLife.instance.ReduceLife();

			var respawn = collision.gameObject.GetComponent<PlayerRespawn>();
			if (respawn != null)
				respawn.Respawn();
		}
	}
}
