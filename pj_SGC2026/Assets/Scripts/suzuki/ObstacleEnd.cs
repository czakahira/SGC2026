using UnityEngine;

public class ColorObstacleEnd : MonoBehaviour
{
	public ColorType obstacleColor;
	public GameObject scoreEffectPrefab;
	private void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponent<PlayerColor>();
		if (player == null) return;

		// ★壁を通り抜けて終端まで来たときだけ加算
		if (player.currentColor == obstacleColor)
		{
			ScoreManager.instance.AddScore(100);
			Debug.Log("壁通過 +100");
			Instantiate(scoreEffectPrefab, transform.position, Quaternion.identity);
		}
	}
}
