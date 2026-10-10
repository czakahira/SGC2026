using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
	public float lifetime = 1.0f;
	public float moveUpSpeed = 1.0f;

	private void Update()
	{
		// 上にふわっと移動
		transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

		// 一定時間で消える
		lifetime -= Time.deltaTime;
		if (lifetime <= 0f)
		{
			Destroy(gameObject);
		}
	}
}
