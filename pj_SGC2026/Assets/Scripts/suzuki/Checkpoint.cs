using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerRespawn.instance.SetCheckpoint(transform.position);
			Debug.Log("チェックポイント更新: " + transform.position);
		}
	}
}
