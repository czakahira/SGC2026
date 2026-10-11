using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public float respawnHeightOffset = 1.0f; // ★ 好きな高さに調整できる
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Vector3 pos = transform.position;
			pos.y += respawnHeightOffset; // ★ 高さを少し上げる
			PlayerRespawn.instance.SetCheckpoint(pos);
			Debug.Log("チェックポイント更新: " + pos);
		}
	}
}
